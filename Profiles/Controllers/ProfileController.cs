using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Profiles.Models;

namespace Profiles.Controllers
{
    public class ProfileController : Controller
    {


        private ProfilesContext mydb = null;
        private IHostingEnvironment _env = null;


        public ProfileController(ProfilesContext abc, IHostingEnvironment xyz)
        {

            this._env = xyz;
            mydb = abc;

        }

        [HttpGet]

        public IActionResult Student()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Student(Student S)



        {

            bool email = mydb.Student.Where(a => a.Email == S.Email).Any();
            if (email)
            {
                ViewBag.Message = "You Have Already Registered";
                return View();

            }


            mydb.Student.Add(S);
            mydb.SaveChanges();
            var msg = new MailMessage();
            msg.From = new MailAddress("umairbabar1996@gmail.com", "RegistrationMail");
            msg.To.Add(new MailAddress(S.Email));
            msg.Subject = "ragistration Mail";
            msg.Body = "You have Successfully Registered";
            SmtpClient sc = new SmtpClient();
            sc.Credentials = new System.Net.NetworkCredential("umairbabar1996@gmail.com", "88669966@@");
            sc.Host = "smtp.gmail.com";
            sc.Port = 587;
            sc.EnableSsl = true;
            sc.Send(msg);
            ViewBag.Message = "You have successfully registered";
            return RedirectToAction("Umair");
        }


        [HttpGet]
        public IActionResult EditStudents(int Id)
        {
            Student S = mydb.Student.Where(a => a.Id == Id).SingleOrDefault<Student>();

            return View(S);
        }
        [HttpPost]
        public IActionResult EditStudents(Student S)
        {
            mydb.Student.Update(S);
            mydb.SaveChanges();
            return RedirectToAction("AllStudents");
        }
        public IActionResult DetailsStudents(int Id)
        {
            Student S = mydb.Student.Where(a => a.Id == Id).SingleOrDefault<Student>();
            return View(S);
        }
        [HttpGet]
        public IActionResult DeleteStudents(int Id)
        {
            Student S = mydb.Student.Where(a => a.Id == Id).SingleOrDefault<Student>();

            return View(S);
        }
        [HttpPost]
        public IActionResult DeleteStudents(Student S)
        {
            mydb.Student.Remove(S);
            mydb.SaveChanges();
            return RedirectToAction("AllStudents");
        }
        public IActionResult AllStudents()
        {
            IList<Student> S = new List<Student>();
            S = mydb.Student.ToList<Student>();
            return View(S);
        }
        [HttpGet]
        public IActionResult AllTeachers()
        {
            IList<Teacher> abc = mydb.Teacher.ToList<Teacher>();
            return View(abc);
        }
        [HttpPost]
        public IActionResult AllTeachers(string SearchByName, string SearchByEmail)

        {
           IList<Teacher> o = mydb.Teacher.Where(a => a.Name.Contains(SearchByName)||a.Email.Contains(SearchByEmail)).ToList<Teacher>();
            return View(o);
        } 
        [HttpGet]
        public IActionResult Teacher()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Teacher(Teacher T,IFormFile Image)
        {
            bool abc = mydb.Teacher.Where(a => a.Email == T.Email).Any();
            if (abc)
            {
                ViewBag.Message = "Teacher with same Email already exist.";
                return View();
            }
            string path = _env.WebRootPath +"/images/";
            string FileExt = Path.GetExtension(Image.FileName);
            string Name = DateTime.Now.ToString("yymmddhhmmss") + FileExt;
            FileStream fs = new FileStream(path + Name, FileMode.Create);
            Image.CopyTo(fs);
            fs.Close();
            T.Image = "/images/" + Name + FileExt;

            mydb.Teacher.Add(T);
            mydb.SaveChanges();
            try {
                var msg = new MailMessage();
                msg.From = new MailAddress("umairbabar1996@gmail.com", "Registration Mail");
                msg.To.Add(new MailAddress(T.Email));
                msg.Subject = "Registration Mail";
                msg.Body = "You Have Successfully Registered";
                SmtpClient sc = new SmtpClient();
                sc.Credentials = new System.Net.NetworkCredential("umairbabar1996@gmail.com", "88669966@@");
                sc.Host = "smtp.gmail.com";
                sc.Port = 587;
                sc.EnableSsl = true;
                sc.Send(msg); }
            catch (Exception e)
            {
            }
            ViewBag.Message = "You have successfully registered";
            return RedirectToAction("TeacherProfile");
        }

        public IActionResult TeacherProfile()
        {
            return View();


        }
        [HttpGet]
        public IActionResult EditTeacher(int Id)
        {
            Teacher T = mydb.Teacher.Where(a => a.Id == Id).SingleOrDefault<Teacher>();
            return View(T);

        }
        [HttpPost]
        public IActionResult EditTeacher(Teacher T)
        {
            mydb.Teacher.Update(T);
            mydb.SaveChanges();
            return RedirectToAction("AllTeachers");
        } 
      

        public IActionResult DeleteTeacher(Teacher T)
        {
            mydb.Teacher.Remove(T);
            mydb.SaveChanges();
            return RedirectToAction("AllTeachers");
        }

        public IActionResult DetailsTeacher(int Id)
        {
            Teacher p = mydb.Teacher.Where(a => a.Id ==Id).SingleOrDefault<Teacher>();
            return View(p);
        }



        public IActionResult Profile()
        {

            return RedirectToAction("Umair");
        }
        public IActionResult Umair()
        {
            return View();

        }

        public IActionResult Download(string path)
        {
            return View();
        }
    }
}