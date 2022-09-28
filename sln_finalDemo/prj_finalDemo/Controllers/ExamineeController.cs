using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using prj_finalDemo.Models;
using System.Web.Security;

namespace prj_finalDemo.Controllers
{
    public class ExamineeController : Controller
    {
        private finalDemoEntities db = new finalDemoEntities();

        //登入
        public ActionResult Enter()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Enter(string acc, string pw)
        {
            var temp = db.TableExaminees1091704
                .Where(m => m.account == acc && m.password == pw)
                .FirstOrDefault();
            if (temp == null)
            {
                ViewBag.Message = "帳號或密碼錯誤，登入失敗";
                return View();
            }
            FormsAuthentication.RedirectFromLoginPage(acc, true);
            Session["LoginOK"] = "Yes";
            Session["name"] = temp.name;
            return RedirectToAction("Index","Application");
        }

        //註冊0
        // GET: Examinee/Create
        public ActionResult Create()
        {
            return View();
        }
        // POST: Examinee/Create
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "account,password,email,phone,name")] TableExaminees1091704 Register)
        {
            if (ModelState.IsValid == false)
            {
                return View();
            }
            var check = db.TableExaminees1091704
                .Where(m => m.account == Register.account && m.password == Register.password)
                .FirstOrDefault();
            if (check == null)
            {
                db.TableExaminees1091704.Add(Register);
                db.SaveChanges();
                return RedirectToAction("Enter");
            }
            ViewBag.Err = "此帳號密碼已有人註冊，請重新註冊";
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
