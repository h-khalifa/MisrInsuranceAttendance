using DataAceess.Interfaces;
using DataAceess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Portal.Controllers
{
    public class AttendanceController : Controller
    {

        //private IUnitOfWork _db;

        //public AttendanceController(IUnitOfWork db)
        //{
        //    _db = db;
        //}
        // GET: Attendance
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAllEmployeesCode()
        {
            AttendanceEntities db = new AttendanceEntities();
            dynamic res = db.Employes.Select(e => new
            {
                Id = e.ID,
                Code = e.Code
            }).ToList();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DownloadLogs(DateTime? from = null, DateTime? to = null, int? empId = null)
        {
            try
            {
                AttendanceEntities db = new AttendanceEntities();

                var logsq = db.AttendanceLogs.Select(a => a);
                if (empId.HasValue)
                    logsq = logsq.Where(a => a.EmployeID == empId.Value);
                if (from.HasValue)
                    logsq = logsq.Where(a => a.LogTime > from.Value);
                if (to.HasValue)
                    logsq = logsq.Where(a => a.LogTime < to.Value);

                dynamic res = logsq.AsEnumerable().Select(a => new
                {
                    EmpCode = a.Employe.Code,
                    LogTime = a.LogTime.ToShortTimeString(),
                    LogDate = a.LogTime.ToShortDateString(),
                    LogTypt = a.LogType
                }).ToList();

                return Json(res, JsonRequestBehavior.AllowGet);
                //var jsonResult = Json(res, JsonRequestBehavior.AllowGet);
                //jsonResult.MaxJsonLength = int.MaxValue;
                //return jsonResult;
            }
            catch(Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, ex.Message);
            }
           
        }
    }
}