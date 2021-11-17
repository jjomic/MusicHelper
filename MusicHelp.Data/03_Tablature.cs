using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelp.Data
{
    public class Tablature
    {
        [Key]
        [ForeignKey(nameof(Instrument))]
        public int InstrumentID { get; set; }
        public Instrument Instrument { get; set; }
        [ForeignKey(nameof(Lesson))]
        public int LessonID { get; set; }
        public Lesson Lesson { get; set; }
        [Required]
        [Display(Name = "Song Name")]
        public string TabName { get; set; }
        [Required]
        [Display(Name ="Artist")]
        public string TabArtist { get; set; }
        [Display(Name = "Album")]
        public string TabAlbum { get; set; }
        [Required]
        [Display(Name = "Please enter the difficulty of this song on a scale of 1-10")]
        [Range (1, 10, ErrorMessage = "Please enter a number between 1 and 10")]
        public int TabDifficulty { get; set; }
        [Required]
        [Display(Name = "Please enter the original source of this tab.")]
        public string OriginalSource { get; set; }
        [Required]
        [Display(Name = "Please enter the link for this tab.")]
        public string TabLink { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }

    }
}
