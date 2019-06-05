using DiscussionApp.Data;
using DiscussionApp.Models;
using DiscussionApp.Services;
using DiscussionApp.WebMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiscussionApp.WebMVC.Controllers
{
    [Authorize]
    public class FilmController : Controller
    {
        // GET: Film
        public ActionResult Index()
        {
            var service = new FilmService();
            var model = service.GetFilms().OrderBy(x => x.Title);

            return View(model);
        }

        // GET: Film/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Film/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FilmCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = new FilmService();

            if (service.CreateFilm(model))
            {
                TempData["SaveResult"] = "The film was created.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "The film could not be created.");

            return View(model);
        }

        // GET: Film/Detail/{id}
        public ActionResult Details(int id)
        {
            var service = new FilmService();
            var model = service.GetFilmById(id);

            ViewBag.FilmTitle = service.GetFilmTitle(id);

            return View(model);
        }

        // GET: Film/Edit/{id}
        public ActionResult Edit(int id)
        {
            var service = new FilmService();

            var detail = service.GetFilmById(id);

            var model = new FilmEdit
            {
                FilmId = detail.FilmId,
                Title = detail.Title,
                Director = detail.Director,
                Writer = detail.Writer,
                Stars = detail.Stars,
                Cinematographer = detail.Cinematographer,
                Editor = detail.Editor,
                Synopsis = detail.Synopsis,
                Genre1 = detail.Genre1,
                Genre2 = detail.Genre2,
                //Released = detail.Released,
                Year = detail.Year,
                Runtime = detail.Runtime,
                Rating = detail.Rating,
            };

            ViewBag.Title = service.GetFilmTitle(id);
            return View(model);
        }

        // POST: Film/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, FilmEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.FilmId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = new FilmService();

            if (service.UpdateFilm(model))
            {
                TempData["SaveResult"] = "The film was updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "The film could not be updated.");
            return View();
        }

        public ActionResult Delete(int id)
        {
            var service = new FilmService();
            var model = service.GetFilmById(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteFilm(int id)
        {
            var service = new FilmService();

            if (service.DeleteFilm(id))
            {
                return RedirectToAction("Index");
            }

            TempData["SaveResult"] = "The film was deleted.";

            return RedirectToAction("Delete", new { id });
        }
    }
}