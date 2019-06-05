using DiscussionApp.Models;
using DiscussionApp.Services;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiscussionApp.WebMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        // GET: Post
        public ActionResult Index(string discussionId)
        {
            Guid CurrentDiscussionId = Guid.Parse(discussionId);

            var service = CreatePostService();
            var discussionService = CreateDiscussionService();
            var model = service.GetPostsByDiscussion(CurrentDiscussionId);

            this.Session["currentDiscussion"] = CurrentDiscussionId.ToString();

            ViewBag.DiscussionTitle = discussionService.GetDiscussionTitle(CurrentDiscussionId);
            ViewBag.Op = discussionService.GetDiscussionCreatorId(CurrentDiscussionId);
            ViewBag.UserId = Guid.Parse(User.Identity.GetUserId());

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string body, object nullObject)
        {
            var newPost = new PostCreate()
            {
                Body = body,
            };
            Create(newPost);
            return Redirect(Url.RouteUrl(new { controller = "Post", action = "Index", discussionId = Session["currentDiscussion"] }) + "#new_reply");
        }

        public ActionResult Create()
        {
            var discussionService = CreateDiscussionService();
            ViewBag.Discussion = discussionService.GetDiscussionTitle(Guid.Parse(Session["currentDiscussion"].ToString()));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostCreate model)
        {
            model.DiscussionId = Guid.Parse(this.Session["currentDiscussion"].ToString());

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var service = CreatePostService();

            if (service.CreatePost(model))
            {
                TempData["ResultSaved"] = "Post was created.";
                return Redirect(Url.RouteUrl(new { controller = "Post", action = "Index", discussionId = Session["currentDiscussion"] }) + "#new_reply");
            }

            ModelState.AddModelError("", "Post could not be created.");

            return View(model);
        }

        public ActionResult Edit(Guid id)
        {
            var service = CreatePostService();
            var model = service.GetPostById(id);
            ViewBag.Discussion = Session["currentDiscussion"];
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guid id, PostEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.PostId != id)
            {
                ModelState.AddModelError("", "Id Mismatch");
                return View(model);
            }

            var service = CreatePostService();

            if (service.UpdatePost(model))
            {
                TempData["ResultSaved"] = "Post was updated.";
                return RedirectToAction("Index", new { discussionId = Session["currentDiscussion"] });
            }

            ModelState.AddModelError("", "Post could not be updated.");
            return View(model);
        }

        public ActionResult Delete(Guid id)
        {
            var service = CreatePostService();
            var model = service.GetPostById(id);

            ViewBag.Discussion = Session["currentDiscussion"];
            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteSection(Guid id)
        {
            var service = CreatePostService();
            var model = service.GetPostById(id);

            service.DeletePost(id);

            TempData["SaveResult"] = "Post was deleted.";

            return RedirectToAction("Index", new { discussionId = Session["currentDiscussion"] });
        }

        // ---------- Helper Methods --------------------
        private PostService CreatePostService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new PostService(userId);
            return service;
        }

        private DiscussionService CreateDiscussionService()
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new DiscussionService(userId);
            return service;
        }
    }
}