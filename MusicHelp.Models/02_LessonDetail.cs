using MusicHelp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelp.Models
{
    public class LessonDetail
    {
        [Display(Name = "Lesson ID")]
        public int LessonID { get; set; }
        public int InstrumentID { get; set; }
        [Display(Name = "Lesson is for...")]
        public Instrument Instrument { get; set; }
        [Display(Name = "Title")]
        public string LessonName { get; set; }
        [Display(Name = "Description")]
        public string LessonDescription { get; set; }
        [Display(Name = "Difficulty (Out of 10)")]
        public int LessonDifficulty { get; set; }
        [Display(Name = "Source")]
        public string LessonSource { get; set; }
        public string LessonLink { get; set; }
        [Display(Name = "Date Added")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
