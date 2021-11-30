using MusicHelp.Data;
using MusicHelp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MusicHelp.Services
{
    public class TablatureService
    {
        private readonly Guid _userID;

        public TablatureService(Guid userID)
        {
            _userID = userID;
        }

        public TablatureService() { }

        public bool CreateTab(TablatureCreate model)
        {
            var entity =
                new Tablature()
                {
                    OwnerID = _userID,
                    TabName = model.TabName,
                    InstrumentID = model.InstrumentID,
                    TabArtist = model.TabArtist,
                    TabAlbum = model.TabAlbum,
                    TabDifficulty = model.TabDifficulty,
                    TabSource = model.TabSource,
                    TabLink = model.TabLink,
                    CreatedUtc = DateTimeOffset.Now
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Tabs.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }

        public IEnumerable<TablatureListItem> GetTabs()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Tabs
                        .Select(
                            e =>
                                new TablatureListItem
                                {
                                    TabID = e.TabID,
                                    InstrumentID = e.InstrumentID,
                                    Instrument = e.Instrument,
                                    TabName = e.TabName,
                                    TabArtist = e.TabArtist,
                                    TabAlbum = e.TabAlbum,
                                    TabDifficulty = e.TabDifficulty,
                                    TabSource = e.TabSource,
                                    TabLink = e.TabLink,
                                    CreatedUtc = e.CreatedUtc,
                                    IsStarred = e.IsStarred
                                }
                        );
                return query.ToArray();
            }
        }

        public TablatureDetail GetTabByID(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Tabs
                        .Single(e => e.TabID == id);
                return
                    new TablatureDetail
                    {
                        TabID = entity.TabID,
                        TabName = entity.TabName,
                        InstrumentID = entity.InstrumentID,
                        Instrument = entity.Instrument,
                        TabArtist = entity.TabArtist,
                        TabAlbum = entity.TabAlbum,
                        TabDifficulty = entity.TabDifficulty,
                        TabSource = entity.TabSource,
                        TabLink = entity.TabLink,
                        CreatedUtc = entity.CreatedUtc
                    };
            }
        }

        public bool UpdateTab(TablatureEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Tabs
                        .Single(e => e.TabID == model.TabID);

                entity.TabID = model.TabID;
                entity.TabName = model.TabName;
                entity.InstrumentID = model.InstrumentID;
                entity.TabArtist = model.TabArtist;
                entity.TabAlbum = model.TabAlbum;
                entity.TabDifficulty = model.TabDifficulty;
                entity.TabSource = model.TabSource;
                entity.TabLink = model.TabLink;
                entity.ModifiedUtc = DateTimeOffset.UtcNow;
                entity.IsStarred = model.IsStarred;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteTab(int tabID)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Tabs
                        .Single(e => e.TabID == tabID);

                ctx.Tabs.Remove(entity);

                return ctx.SaveChanges() == 1;
            }
        }
    }
}
