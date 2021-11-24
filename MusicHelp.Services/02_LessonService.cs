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

        //Convert to link method
        private static readonly Regex regex = new Regex("((http://|www\\.)([A-Z0-9.-:]{1,})\\.[0-9A-Z?;~&#=\\-_\\./]{2,})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly string link = "<a href=\"{0}{1}\">{2}</a>";

        public static string ResolveLinks(string body)
        {
            if (string.IsNullOrEmpty(body))
                return body;

            foreach (Match match in regex.Matches(body))
            {
                if (!match.Value.Contains("://"))
                {
                    body = body.Replace(match.Value, string.Format(link, "http://", match.Value, ShortenUrl(match.Value, 50)));
                }
                else
                {
                    body = body.Replace(match.Value, string.Format(link, string.Empty, match.Value, ShortenUrl(match.Value, 50)));
                }
            }

            return body;
        }

        private static string ShortenUrl(string url, int max)
        {
            if (url.Length <= max)
                return url;

            // Remove the protocal
            int startIndex = url.IndexOf("://");
            if (startIndex > -1)
                url = url.Substring(startIndex + 3);

            if (url.Length <= max)
                return url;

            // Remove the folder structure
            int firstIndex = url.IndexOf("/") + 1;
            int lastIndex = url.LastIndexOf("/");
            if (firstIndex < lastIndex)
                url = url.Replace(url.Substring(firstIndex, lastIndex - firstIndex), "...");

            if (url.Length <= max)
                return url;

            // Remove URL parameters
            int queryIndex = url.IndexOf("?");
            if (queryIndex > -1)
                url = url.Substring(0, queryIndex);

            if (url.Length <= max)
                return url;

            // Remove URL fragment
            int fragmentIndex = url.IndexOf("#");
            if (fragmentIndex > -1)
                url = url.Substring(0, fragmentIndex);

            if (url.Length <= max)
                return url;

            // Shorten page
            firstIndex = url.LastIndexOf("/") + 1;
            lastIndex = url.LastIndexOf(".");
            if (lastIndex - firstIndex > 10)
            {
                string page = url.Substring(firstIndex, lastIndex - firstIndex);
                int length = url.Length - max + 3;
                url = url.Replace(page, "..." + page.Substring(length));
            }

            return url;
        }
    }
}
