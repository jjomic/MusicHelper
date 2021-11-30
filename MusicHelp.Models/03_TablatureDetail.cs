using MusicHelp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelp.Models
{
    public class TablatureDetail
    {
        [Display(Name = "ID")]
        public int TabID { get; set; }
        [Display(Name = "These tabs are for...")]
        public int InstrumentID { get; set; }
        public Instrument Instrument { get; set; }
        [Display(Name ="Song:")]
        public string TabName { get; set; }
        [Display(Name ="Artist")]
        public string TabArtist { get; set; }
        [Display(Name ="Album")]
        public string TabAlbum { get; set; }
        [Display(Name ="Difficulty (Out of 10")]
        public int TabDifficulty { get; set; }
        [Display(Name ="Source")]
        public string TabSource { get; set; }
        public string TabLink { get; set; }
        [Display(Name ="Date Added")]
        public DateTimeOffset CreatedUtc { get; set; }
        [UIHint("Starred")]
        public bool IsStarred { get; set; }
    }
}
