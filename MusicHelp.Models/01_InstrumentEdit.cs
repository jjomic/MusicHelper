using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelp.Models
{
    public class InstrumentEdit
    {
        public int InstrumentID { get; set; }
        public string InstrumentName { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
    }
}
