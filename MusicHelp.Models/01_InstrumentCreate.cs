using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelp.Models
{
    public class InstrumentCreate
    {
        public int InstrumentID { get; set; }
        [Display(Name = "Please enter the name of the instrument you would like to add...")]
        [MinLength(1, ErrorMessage = "You didn't enter anything...")]
        public string InstrumentName { get; set; }
    }
}
