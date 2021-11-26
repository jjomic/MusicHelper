using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MusicHelp.Data
{
    public class Lesson
    {
        private static readonly Regex regex = new Regex("((http://|www\\.)([A-Z0-9.-:]{1,})\\.[0-9A-Z?;~&#=\\-_\\./]{2,})", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        private static readonly string link = "<a href=\"{0}{1}\">{2}</a>";

        [Key]
        public int LessonID { get; set; }
        public Guid OwnerID { get; set; }
        [ForeignKey(nameof(Instrument))]
        public int InstrumentID { get; set; }
        public virtual Instrument Instrument { get; set; }
        public virtual List<Tablature> _tabs { get; set; }
        [Required]
        [Display(Name="Please enter the name of the lesson.")]
        [MinLength(5, ErrorMessage = "Please provide a more complete title.")]
        public string LessonName { get; set; }
        [Required]
        [Display(Name="Please provide a brief description of this lesson.")]
        [MinLength(1, ErrorMessage = "Please describe your lesson before proceeding.")]
        public string LessonDescription { get; set; }
        [Required]
        [Display(Name = "On a scale of 1-10, please enter the difficulty of this lesson.")]
        [Range(1,10, ErrorMessage ="Please enter a number between 1-10")]
        public int LessonDifficulty { get; set; }
        [Required]
        [Display(Name ="Please provide the name of the original source where you found this lesson.")]
        public string LessonSource { get; set; }
        [Required]
        [Display(Name ="Please enter the URL for your lesson.")]
        public string LessonLink { get; set; }
        [Display(Name = "Date Published")]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
