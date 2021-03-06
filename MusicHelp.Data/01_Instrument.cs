using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelp.Data
{
    public class Instrument
    {
        [Key]
        public int InstrumentID { get; set; }
        public Guid OwnerID { get; set; }
        public virtual List<Lesson> _lessons { get; set; }
        public virtual List<Tablature> _tabs { get; set; }
        [Required]
        [Display(Name = "Instrument")]
        public string InstrumentName { get; set; }
        [Display(Name = "Added")]
        public DateTimeOffset CreatedUtc { get; set; }
        public DateTimeOffset? ModifiedUtc { get; set; }
        [DefaultValue(false)]
        public bool IsStarred { get; set; }
    }
}
