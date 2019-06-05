using DiscussionApp.Data;
using DiscussionApp.Models;
using DiscussionApp.Services;
using DiscussionApp.WebMVC.Data;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiscussionApp.WebMVC.Controllers
{
    [Authorize]
    public class DiscussionController : Controller
    {
        FilmService filmService = new FilmService();

        // GET: Discussion
        [Route("")]
        public ActionResult Index()
        {
            var service = NewDiscussionService();
            var model = service.GetDiscussions();

            ViewBag.UserId = Guid.Parse(User.Identity.GetUserId());

            return View(model);
        }

        //// GET: FilmDiscussions
        //[Route("")]
        //public ActionResult IndexFilm()
        //{
        //    var service = NewDiscussionService();
        //    var model = service.GetDiscussionsByType(MediaType.Film);

        //    ViewBag.UserId = Guid.Parse(User.Identity.GetUserId());

        //    return View(model);
        //}

        // GET: Discussion/Create
        public ActionResult CreateFilmDiscussion()
        {
            ViewBag.FilmId = new SelectList(filmService.GetFilms(), "FilmId", "Title");

            return View();
        }

        // POST: FilmDiscussion/Create
        [HttpPost]
        [ActionName("CreateFilmDiscussion")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFilmDiscussion(FilmDiscussionCreate model)
        {
            model.DiscussionId = Guid.NewGuid();

            var service = NewDiscussionService();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (service.CreateDiscussion(model))
            {
                TempData["ResultSaved"] = "The discussion was created.";
                return RedirectToAction("Index", "Post", new { discussionId = model.DiscussionId });
            };

            ModelState.AddModelError("", "The discussion could not be created.");

            return View(model);
        }

        // GET: Discussion/Detail/{id}
        public ActionResult Details(Guid id)
        {
            var service = NewDiscussionService();
            var model = service.GetDiscussionById(id);

            ViewBag.Discussion = service.GetDiscussionTitle(id);

            return View(model);
        }

        // GET: Discussion/Edit/{id}
        public ActionResult EditFilmDiscussion(Guid id)
        {
            var service = NewDiscussionService();

            var detail = service.GetDiscussionById(id);

            var model = new DiscussionEdit
            {
                DiscussionId = detail.DiscussionId,
                FilmId = detail.FilmId,
                DiscussionTitle = detail.DiscussionTitle,
            };

            ViewBag.Discussion = service.GetDiscussionTitle(id);
            ViewBag.FilmId = new SelectList(filmService.GetFilms(), "FilmId", "Title", detail.FilmId);
            return View(model);
        }

        // PUT: FilmDiscussion/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditFilmDiscussion(Guid id, DiscussionEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.DiscussionId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = NewDiscussionService();

            if (service.UpdateFilmDiscussion(model))
            {
                TempData["ResultSaved"] = "The discussion was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The discussion could not be updated.");
            return View(model);
        }

        // DELETE: Discussion/Delete/{id}
        public ActionResult Delete(Guid id)
        {
            var service = NewDiscussionService();
            var model = service.GetDiscussionById(id);

            ViewBag.Discussion = service.GetDiscussionTitle(id);

            return View(model);
        }

        // POST: Discussion/Delete/{id}
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDiscussion(Guid id)
        {
            var service = NewDiscussionService();

            service.DeleteDiscussion(id);

            TempData["SaveResult"] = "The discussion was deleted.";

            return RedirectToAction("Index");
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