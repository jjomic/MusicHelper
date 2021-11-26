using MusicHelp.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelp.Models
{
    public class InstrumentListItem
    {
        [Display(Name = "Instrument ID")]
        public int InstrumentID { get; set; }
        [Display(Name = "Instrument")]
        public string InstrumentName { get; set; }
        [Display(Name = "Date Added")]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
