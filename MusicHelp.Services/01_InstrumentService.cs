using MusicHelp.Data;
using MusicHelp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicHelp.Services
{
    public class InstrumentService
    {
        private readonly Guid _userID;

        public InstrumentService(Guid userID)
        {
            _userID = userID;
        }

        public InstrumentService() { }

        public bool CreateInstrument(InstrumentCreate model)
        {
            var entity =
                new Instrument()
                {
                    OwnerID = _userID,
                    InstrumentName = model.InstrumentName,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Instruments.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<InstrumentListItem> GetInstruments()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Instruments
                        .Select(
                            e =>
                                new InstrumentListItem
                                {
                                    InstrumentID = e.InstrumentID,
                                    InstrumentName = e.InstrumentName,
                                    CreatedUtc = e.CreatedUtc
                                }
                        );
                return query.ToArray();
            }
        }

        public InstrumentDetail GetInstrumentByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Instruments
                        .Single(e => e.InstrumentID == id);
                return
                    new InstrumentDetail
                    {
                        InstrumentID = entity.InstrumentID,
                        InstrumentName = entity.InstrumentName,
                        CreatedUtc = entity.CreatedUtc
                    };
            }
        }

        public bool UpdateInstrument(InstrumentEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Instruments
                        .Single(e => e.InstrumentID == model.InstrumentID);

                entity.InstrumentName = model.InstrumentName;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteInstrument(int instrumentID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Instruments
                        .Single(e => e.InstrumentID == instrumentID);

                ctx.Instruments.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
