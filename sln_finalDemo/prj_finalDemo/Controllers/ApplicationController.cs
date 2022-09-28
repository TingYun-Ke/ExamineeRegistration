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
using PagedList;

namespace prj_finalDemo.Controllers
{
    public class ApplicationController : Controller
    {
        private finalDemoEntities db = new finalDemoEntities();

        //考生分布圖
        // GET: Application
        int pageSize = 10;
        public ActionResult Index(int page = 1)
        {
            if (Session["LoginOK"] != null && Session["LoginOK"].ToString() == "Yes") {
                int currentPage = page < 1 ? 1 : page;
                var children = db.TableApplications1091704.OrderBy(m => m.identity).ToList();
                var result = children.ToPagedList(currentPage, pageSize);
                return View(result);
            }
            else
            {
                return RedirectToAction("Enter", "Examinee");
            }
        }

        // GET: Application/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableApplications1091704 tableApplications1091704 = db.TableApplications1091704.Find(id);
            if (tableApplications1091704 == null)
            {
                return HttpNotFound();
            }
            return View(tableApplications1091704);
        }

        //報名考試
        // GET: Application/Create
        public ActionResult Create()
        {
            if (Session["LoginOK"] != null && Session["LoginOK"].ToString() == "Yes")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Create", "Examinee");
            }
        }
        // POST: Application/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "identity,county,schoolName,suject")] TableApplications1091704 tableApplications1091704)
        {
            if (ModelState.IsValid)
            {
                db.TableApplications1091704.Add(tableApplications1091704);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tableApplications1091704);
        }

        // GET: Application/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableApplications1091704 tableApplications1091704 = db.TableApplications1091704.Find(id);
            if (tableApplications1091704 == null)
            {
                return HttpNotFound();
            }
            return View(tableApplications1091704);
        }
        // POST: Application/Edit/5
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "identity,county,schoolName,suject")] TableApplications1091704 tableApplications1091704)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tableApplications1091704).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tableApplications1091704);
        }

        // GET: Application/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TableApplications1091704 tableApplications1091704 = db.TableApplications1091704.Find(id);
            if (tableApplications1091704 == null)
            {
                return HttpNotFound();
            }
            return View(tableApplications1091704);
        }
        // POST: Application/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TableApplications1091704 tableApplications1091704 = db.TableApplications1091704.Find(id);
            db.TableApplications1091704.Remove(tableApplications1091704);
            db.SaveChanges();
            return RedirectToAction("Index");
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
