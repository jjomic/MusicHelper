using Microsoft.AspNet.Identity;
using MusicHelp.Models;
using MusicHelp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MusicHelper.Controllers.WebAPI
{
    [Authorize]
    [RoutePrefix("api/Lesson")]
    public class LessonController : ApiController
    {
        private bool SetStarState(int lessonId, bool newState)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new LessonService(userId);

            var detail = service.GetLessonByID(lessonId);

            var updatedLesson =
                new LessonEdit
                {
                    LessonID = detail.LessonID,
                    InstrumentID = detail.InstrumentID,
                    LessonName = detail.LessonName,
                    LessonDescription = detail.LessonDescription,
                    LessonDifficulty = detail.LessonDifficulty,
                    LessonSource = detail.LessonSource,
                    LessonLink = detail.LessonLink,
                    IsStarred = newState
                };

            return service.UpdateLesson(updatedLesson);
        }

        [Route("{id}/Star")]
        [HttpPut]
        public bool ToggleStarOn(int id) => SetStarState(id, true);

        [Route("{id}/Star")]
        [HttpDelete]
        public bool ToggleStarOff(int id) => SetStarState(id, false);
    }
}
