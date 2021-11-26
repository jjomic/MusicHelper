using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelp.Models
{
    public class InstrumentDetail
    {
        [Display(Name = "ID")]
        public int InstrumentID { get; set; }
        [Display(Name = "Name")]
        public string InstrumentName { get; set; }
        [Display(Name = "Date Added")]
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
