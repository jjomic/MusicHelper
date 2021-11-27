using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelp.Models
{
    public class LessonCreate
    {
        public int LessonID { get; set; }
        [Display(Name ="What instrument is this lesson for?")]
        public int InstrumentID { get; set; }
        [Display(Name ="What is the title of this lesson?")]
        [MinLength(1, ErrorMessage = "You didn't enter anything...")]
        [MaxLength(100, ErrorMessage ="That title is too long!")]
        public string LessonName { get; set; }
        [Display(Name ="Please enter a brief description for this lesson.")]
        [MinLength(10, ErrorMessage ="Please be a bit, more expansive.")]
        [MaxLength(1000, ErrorMessage ="Description is too long!")]
        public string LessonDescription { get; set; }
        [Display(Name ="On a scale of 1-10, how difficult is this lesson?")]
        [Range(1, 10, ErrorMessage ="Please pick a number between 1 and 10...")]
        public int LessonDifficulty { get; set; }
        [Display(Name ="Where did you originally find this lesson? (i.e. name of YouTube channel, website, etc.)")]
        [MinLength(1, ErrorMessage ="Please site your sources.")]
        public string LessonSource { get; set; }
        [Display(Name = "Please enter the link for this lesson!")]
        public string LessonLink { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
