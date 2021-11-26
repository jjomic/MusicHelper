using MusicHelp.Data;
using MusicHelp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MusicHelp.Services
{
    public class LessonService
    {
        private readonly Guid _userID;

        public LessonService(Guid userID)
        {
            _userID = userID;
        }

        public LessonService()
        {

        }

        public bool CreateLesson(LessonCreate model)
        {
            var entity =
                new Lesson()
                {
                    OwnerID = _userID,
                    LessonName = model.LessonName,
                    InstrumentID = model.InstrumentID,
                    LessonDescription = model.LessonDescription,
                    LessonDifficulty = model.LessonDifficulty,
                    LessonSource = model.LessonSource,
                    LessonLink = model.LessonLink,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Lessons.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<LessonListItem> GetLessons()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Lessons
                        .Select(
                            e =>
                                new LessonListItem
                                {
                                    LessonID = e.LessonID,
                                    InstrumentID = e.InstrumentID,
                                    Instrument = e.Instrument,
                                    LessonName = e.LessonName,
                                    LessonDescription = e.LessonDescription,
                                    LessonDifficulty = e.LessonDifficulty,
                                    LessonSource = e.LessonSource,
                                    LessonLink = e.LessonLink,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );
                return query.ToArray();
            }
        }

        public LessonDetail GetLessonByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Lessons
                        .Single(e => e.LessonID == id);
                return
                    new LessonDetail
                    {
                        LessonID = entity.LessonID,
                        LessonName = entity.LessonName,
                        InstrumentID = entity.InstrumentID,
                        Instrument = entity.Instrument,
                        LessonDescription = entity.LessonDescription,
                        LessonDifficulty = entity.LessonDifficulty,
                        LessonSource = entity.LessonSource,
                        LessonLink = entity.LessonLink,
                        CreatedUtc = entity.CreatedUtc
                    };
            }
        }

        public bool UpdateLesson(LessonEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Lessons
                        .Single(e => e.LessonID == model.LessonID);

                entity.LessonName = model.LessonName;
                entity.InstrumentID = model.InstrumentID;
                entity.LessonDescription = model.LessonDescription;
                entity.LessonDifficulty = model.LessonDifficulty;
                entity.LessonSource = model.LessonSource;
                entity.LessonLink = model.LessonLink;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteLesson(int lessonID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Lessons
                        .Single(e => e.LessonID == lessonID);

                ctx.Lessons.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
