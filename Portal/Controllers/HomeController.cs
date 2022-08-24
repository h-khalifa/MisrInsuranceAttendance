using DataAceess;
using DataAceess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        IUnitOfWork _db;

        public HomeController(IUnitOfWork db)
        {
            _db = db;
        }
        public ActionResult Index()
        {
            var branches = _db.BranchesRepo.GetAll();
            var onst = branches.First();
            int x = 9;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}