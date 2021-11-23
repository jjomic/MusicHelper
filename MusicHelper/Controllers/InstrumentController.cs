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
    public class InstrumentController : Controller
    {
        // GET: Instrument
        public ActionResult Index()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new InstrumentService(userID);
            var model = service.GetInstruments();

            return View(model);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(InstrumentCreate model)
        {
            if (!ModelState.IsValid) return View(model);

            var service = CreateInstrumentService();

            if (service.CreateInstrument(model))
            {
                TempData["SaveResult"] = "Instrument added to the database.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "Instrument could not be added.");

            return View(model);
        }

        public ActionResult InstrumentDetails(int id)
        {
            var svc = CreateInstrumentService();
            var model = svc.GetInstrumentByID(id);
            return View(model);
        }

        public ActionResult InstrumentEdit(int id)
        {
            var service = CreateInstrumentService();
            var detail = service.GetInstrumentByID(id);
            var model =
                new InstrumentEdit
                {
                    InstrumentID = detail.InstrumentID,
                    InstrumentName = detail.InstrumentName
                };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult InstrumentEdit(int id, InstrumentEdit model)
        {
            if (!ModelState.IsValid) return View(model);

            if(model.InstrumentID != id)
            {
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateInstrumentService();

            if (service.UpdateInstrument(model))
            {
                TempData["SaveResult"] = "Instrument information has been updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "This instrument could not be updated.");
            return View(model);
        }

        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateInstrumentService();
            var model = svc.GetInstrumentByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteInstrument(int id)
        {
            var service = CreateInstrumentService();

            service.DeleteInstrument(id);

            TempData["SaveResult"] = "This instrument has now been deleted";

            return RedirectToAction("Index");
        }

        private InstrumentService CreateInstrumentService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new InstrumentService(userID);
            return service;
        }
    }
}