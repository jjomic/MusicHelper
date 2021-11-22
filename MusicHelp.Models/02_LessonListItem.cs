using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelp.Models
{
    public class LessonListItem
    {
        public int LessonID { get; set; }
        public int InstrumentID { get; set; }
        public string LessonName { get; set; }
        public string LessonDescription { get; set; }
        public int LessonDifficulty { get; set; }
        public string LessonSource { get; set; }
        public string LessonLink { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
