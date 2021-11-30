using Microsoft.AspNet.Identity;
using MusicHelp.Data;
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
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: Tablature
        public ActionResult Index()
        {
            var service = CreateUnauthenticatedTablatureService();
            var model = service.GetTabs();
            PopulateInstrumentDropDownList();
            return View(model);
        }
        [Authorize]
        public ActionResult Create()
        {
            PopulateInstrumentDropDownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TablatureCreate model)
        {
            if (!ModelState.IsValid) 
            {
                PopulateInstrumentDropDownList();
                return View(model); 
            }

            var service = CreateTablatureService();

            if (service.CreateTab(model))
            {
                PopulateInstrumentDropDownList();
                TempData["SaveResult"] = "Song added to the database.";
                return RedirectToAction("Tablature Index");
            };

            ModelState.AddModelError("", "This song could not be added.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateTablatureService();
            var model = svc.GetTabByID(id);
            return View(model);
        }

        [Authorize]
        public ActionResult Edit(int id)
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
                    TabLink = detail.TabLink,
                    IsStarred = detail.IsStarred
                };

            PopulateInstrumentDropDownList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, TablatureEdit model)
        {
            if (!ModelState.IsValid) 
            {
                PopulateInstrumentDropDownList();
                return View(model); 
            }

            if (model.TabID != id)
            {
                PopulateInstrumentDropDownList();
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateTablatureService();

            if (service.UpdateTab(model))
            {
                PopulateInstrumentDropDownList();
                TempData["SaveResult"] = "Song information has been updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "This song could not be updated.");
            return View(model);
        }

        [Authorize]
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

            return RedirectToAction("Index");
        }

        //Helper methods

        public void PopulateInstrumentDropDownList()
        {
            ViewBag.Instruments = new SelectList(new InstrumentService().GetInstruments(), "InstrumentID", "InstrumentName");
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