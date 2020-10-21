using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UploadingAndRetrivingImages.Models;

namespace UploadingAndRetrivingImages.Controllers
{
    public class HomeController : Controller
    {
        ImageDBEntities1 db=new ImageDBEntities1();

        public ActionResult Index()
        {
           var data= db.Students.ToList();
            return View(data);
        }
        // GET: Home
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Student s)
        {
            string fileName = Path.GetFileNameWithoutExtension(s.ImageFile.FileName);
            string extention = Path.GetExtension(s.ImageFile.FileName);
            HttpPostedFileBase postedFile = s.ImageFile;
            int length = postedFile.ContentLength;

            if (extention.ToLower() == ".jpg" || extention.ToLower() == ".png" || extention.ToLower() == ".jpeg")
            {
                if (length <= 1000000)
                {
                    fileName = fileName + extention;
                    s.ImagePath = "~/images/" + fileName;
                    fileName = Path.Combine(Server.MapPath("~/images/"), fileName);
                    s.ImageFile.SaveAs(fileName);
                    db.Students.Add(s);

                    int a = db.SaveChanges();
                    if (a > 0)
                    {
                        ViewBag.Message = "<script> alert ('Record inserted !!')</script>";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.SizeMessage = "<script> alert ('Image Size NOT inserted !!')</script>";
                    }
                    
                }
            }
            else
            {
                ViewBag.ExtentionMessage = "<script> alert ('Image Not Supported !!')</script>";
            }
            return View();
        }
    }
}