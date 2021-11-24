using Microsoft.AspNet.Identity;
using MusicHelp.Models;
using MusicHelp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicHelper.Controllers
{
    public class TablatureController : Controller
    {
        // GET: Tablature
        public ActionResult Index()
        {
            var service = CreateUnauthenticatedTablatureService();
            var model = service.GetTabs();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TablatureCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateTablatureService();

            if (service.CreateTab(model))
            {
                TempData["SaveResult"] = "Song added to the database.";
                return RedirectToAction("Tablature Index");
            };

            ModelState.AddModelError("", "This song could not be added.");

            return View(model);
        }

        public ActionResult TabDetails(int id)
        {
            var svc = CreateTablatureService();
            var model = svc.GetTabByID(id);
            return View(model);
        }

        public ActionResult TabEdit(int id)
        {
            var service = CreateTablatureService();
            var detail = service.GetTabByID(id);
            var model =
                new TablatureEdit
                {
                    TabID = detail.TabID,
                    TabName = detail.TabName,
                    InstrumentID = detail.InstrumentID,
                    TabArtist = detail.TabArtist,
                    TabAlbum = detail.TabAlbum,
                    TabDifficulty = detail.TabDifficulty,
                    TabSource = detail.TabSource,
                    TabLink = detail.TabLink
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TabEdit(int id, TablatureEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if (model.TabID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateTablatureService();

            if (service.UpdateTab(model))
            {
                TempData["SaveResult"] = "Song information has been updated.";
                return RedirectToAction("Lesson Index");
            }

            ModelState.AddModelError("", "This song could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateTablatureService();
            var model = svc.GetTabByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTab(int id)
        {
            var service = CreateTablatureService();

            service.DeleteTab(id);

            TempData["SaveResult"] = "This song's tab has now been deleted";

            return RedirectToAction("Tablature Index");
        }

        private TablatureService CreateTablatureService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new TablatureService(userID);
            return service;
        }

        private TablatureService CreateUnauthenticatedTablatureService()
        {
            var service = new TablatureService();
            return service;
        }
    }
}