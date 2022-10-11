using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAceess.Models;

namespace Portal.Controllers
{
    public class DevicesController : Controller
    {
        private AttendanceEntities db = new AttendanceEntities();

        // GET: Devices
        public ActionResult Index()
        {
            var fingerPrintDevices = db.FingerPrintDevices.Include(f => f.Branch);
            return View(fingerPrintDevices.ToList());
        }

        // GET: Devices/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FingerPrintDevice fingerPrintDevice = db.FingerPrintDevices.Find(id);
            if (fingerPrintDevice == null)
            {
                return HttpNotFound();
            }
            return View(fingerPrintDevice);
        }

        // GET: Devices/Create
        public ActionResult Create()
        {
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name");
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Desc,LocationDesc,IP,Port,Mac,Model,BranchID,Vendor,SerialNumber,FirmwareVer,Platform,ProductionTime,LatestLogTime")] FingerPrintDevice fingerPrintDevice)
        {
            if (ModelState.IsValid)
            {
                db.FingerPrintDevices.Add(fingerPrintDevice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", fingerPrintDevice.BranchID);
            return View(fingerPrintDevice);
        }

        // GET: Devices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FingerPrintDevice fingerPrintDevice = db.FingerPrintDevices.Find(id);
            if (fingerPrintDevice == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", fingerPrintDevice.BranchID);
            return View(fingerPrintDevice);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Desc,LocationDesc,IP,Port,Mac,Model,BranchID,Vendor,SerialNumber,FirmwareVer,Platform,ProductionTime,LatestLogTime")] FingerPrintDevice fingerPrintDevice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fingerPrintDevice).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchID = new SelectList(db.Branches, "ID", "Name", fingerPrintDevice.BranchID);
            return View(fingerPrintDevice);
        }

        // GET: Devices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FingerPrintDevice fingerPrintDevice = db.FingerPrintDevices.Find(id);
            if (fingerPrintDevice == null)
            {
                return HttpNotFound();
            }
            return View(fingerPrintDevice);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FingerPrintDevice fingerPrintDevice = db.FingerPrintDevices.Find(id);
            db.FingerPrintDevices.Remove(fingerPrintDevice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Connect(string ip, string port)
        {
            try
            {
                string link = "api/Devices/ConnectAndSave?ip=" + ip + "&port=" + port;
                WebClient client = new WebClient();
                client.BaseAddress = "http://localhost:4370/";
                string id = client.DownloadString(link);
                return new HttpStatusCodeResult(HttpStatusCode.OK, id);
            }
            catch(WebException exWeb)
            {
                HttpWebResponse response = (System.Net.HttpWebResponse)exWeb.Response;
                //switch (response.StatusCode)
                //{
                //    case (HttpStatusCode.Conflict):
                //        break;
                //    case (HttpStatusCode.BadGateway):
                //        break;
                //    case (HttpStatusCode.InternalServerError):
                //        break;
                //}

                var temp = new System.IO.StreamReader(response.GetResponseStream()).ReadToEnd();
                dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(temp);

                return new HttpStatusCodeResult(response.StatusCode, obj);
            }
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
