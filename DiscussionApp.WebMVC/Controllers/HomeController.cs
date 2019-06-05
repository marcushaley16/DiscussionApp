using DiscussionApp.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiscussionApp.WebMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: HomeTrending
        [Route("")]
        public ActionResult Index()
        {
            var service = new HomeService();
            var model = service.GetTrendingDiscussions();

            return View(model);
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

        // ------------- Helper Method ------------------------
        private DiscussionService NewDiscussionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new DiscussionService(userId);
            return service;
        }
    }
}