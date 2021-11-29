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
    public class LessonController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Lesson
        public ActionResult Index()
        {
            var service = CreateUnauthenticatedLessonService();
            var model = service.GetLessons();
            PopulateInstrumentDropdownList();
            return View(model);
        }

        [Authorize]
        public ActionResult Create()
        {
            PopulateInstrumentDropdownList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LessonCreate model)
        {
            if (!ModelState.IsValid)
            {
                PopulateInstrumentDropdownList();
                return View(model);
            }
            var service = CreateLessonService();

            if (service.CreateLesson(model))
            {
                PopulateInstrumentDropdownList();
                TempData["SaveResult"] = "Lesson added to the database.";
                return RedirectToAction("Index");
            };

            ModelState.AddModelError("", "This lesson could not be added.");

            return View(model);
        }

        public ActionResult Details(int id)
        {
            var svc = CreateUnauthenticatedLessonService();
            var model = svc.GetLessonByID(id);
            return View(model);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var service = CreateLessonService();
            var detail = service.GetLessonByID(id);
            var model =
                new LessonEdit
                {
                    LessonID = detail.LessonID,
                    LessonName = detail.LessonName,
                    InstrumentID = detail.InstrumentID,
                    LessonDescription = detail.LessonDescription,
                    LessonDifficulty = detail.LessonDifficulty,
                    LessonSource = detail.LessonSource,
                    LessonLink = detail.LessonLink,
                    IsStarred = detail.IsStarred
                };

            PopulateInstrumentDropdownList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, LessonEdit model)
        {
            if (!ModelState.IsValid)
            {
                PopulateInstrumentDropdownList();
                return View(model);
            }
            if (model.LessonID != id)
            {
                PopulateInstrumentDropdownList();
                ModelState.AddModelError("", "ID Mismatch");
                return View(model);
            }

            var service = CreateLessonService();

            if (service.UpdateLesson(model))
            {
                PopulateInstrumentDropdownList();
                TempData["SaveResult"] = "Lesson information has been updated.";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "This lesson could not be updated.");
            return View(model);
        }

        [Authorize]
        [ActionName("Delete")]
        public ActionResult Delete(int id)
        {
            var svc = CreateLessonService();
            var model = svc.GetLessonByID(id);

            return View(model);
        }

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteLesson(int id)
        {
            var service = CreateLessonService();

            service.DeleteLesson(id);

            TempData["SaveResult"] = "This lesson has now been deleted";

            return RedirectToAction("Lesson Index");
        }

        //Helper Methods

        private void PopulateInstrumentDropdownList()
        {
            ViewBag.Instruments = new SelectList(new InstrumentService().GetInstruments(), "InstrumentID", "InstrumentName");
        }

        private LessonService CreateLessonService()
        {
            var userID = Guid.Parse(User.Identity.GetUserId());
            var service = new LessonService(userID);
            return service;
        }

        private LessonService CreateUnauthenticatedLessonService()
        {
            var service = new LessonService();
            return service;
        }
    }
}