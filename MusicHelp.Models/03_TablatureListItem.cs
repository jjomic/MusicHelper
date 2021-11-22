using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelp.Models
{
    public class TablatureListItem
    {
        public int TabID { get; set; }
        public int InstrumentID { get; set; }
        public string TabName { get; set; }
        public string TabArtist { get; set; }
        public string TabAlbum { get; set; }
        public int TabDifficulty { get; set; }
        public string TabSource { get; set; }
        public string TabLink { get; set; }
        public DateTimeOffset CreatedUtc { get; set; }
    }
}
