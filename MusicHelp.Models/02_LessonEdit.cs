using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelp.Models
{
    public class LessonEdit
    {
        public int LessonID { get; set; }
        [Display(Name = "Please enter the corresponding Instrument ID that this lesson is for...")]
        public int InstrumentID { get; set; }
        [Display(Name = "Please enter the updated title for this lesson...")]
        [MinLength(1, ErrorMessage ="You didn't enter anything...")]
        public string LessonName { get; set; }
        [Display(Name = "Please enter the update description for this lesson...")]
        [MinLength(10, ErrorMessage ="Please be a bit, more expansive with your description...")]
        [MaxLength(1000, ErrorMessage ="Description is too long!")]
        public string LessonDescription { get; set; }
        [Display(Name = "On a scale of 1-10, how would you rate the difficulty of this lesson?")]
        [Range(1,10,ErrorMessage ="Please enter a number between 1 & 10")]
        public int LessonDifficulty { get; set; }
        [Display(Name ="Please enter the source of this lesson. (i.e. YouTube channel name, website, etc.")]
        [MinLength(1, ErrorMessage ="Please site your sources.")]
        public string LessonSource { get; set; }
        [Display(Name ="Please enter the link for this lesson.")]
        public string LessonLink { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
