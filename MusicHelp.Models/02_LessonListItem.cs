using MusicHelp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelp.Models
{
    public class LessonListItem
    {
        [Display(Name = "ID")]
        public int LessonID { get; set; }
        [Display(Name = "Lesson is for...")]
        public int InstrumentID { get; set; }
        public Instrument Instrument { get; set; }
        [Display(Name = "Lesson Title")]
        public string LessonName { get; set; }
        [Display(Name = "Description")]
        public string LessonDescription { get; set; }
        [Display(Name = "Diffifulty")]
        public int LessonDifficulty { get; set; }
        [Display(Name = "Source")]
        public string LessonSource { get; set; }
        [Display(Name = "Link")]
        public string LessonLink { get; set; }
        [Display(Name = "Date added")]
        public DateTimeOffset CreatedUtc { get; set; }
        [UIHint("Starred")]
        public bool IsStarred { get; set; }
    }
}
