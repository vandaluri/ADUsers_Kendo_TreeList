using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ADUser_TreeList.Models;
namespace ADUser_TreeList.Controllers
{
    public class HomeController : Controller
    {
        public JsonResult All([DataSourceRequest] DataSourceRequest request)
        {
            ADUserDummyData ad = new ADUserDummyData();
            //var result = ad.GetDummyADUsers().ToTreeDataSourceResult(request,
            //    e => e.Id,
            //    e => e.ManagerId,
            //    e => e
            //);

            var result = ad.GetDummyADUsers_Linq().ToTreeDataSourceResult(request,
                e => e.Id,
                e => e.ManagerId,
                e => e
            );


            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
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