using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelp.Models
{
    public class InstrumentEdit
    {
        public int InstrumentID { get; set; }
        [Display(Name="Please enter the updated name of this instrument...")]
        [MinLength(1, ErrorMessage ="You didn't enter anything...")]
        public string InstrumentName { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
        [UIHint("Starred")]
        public bool IsStarred { get; set; }
    }
}
