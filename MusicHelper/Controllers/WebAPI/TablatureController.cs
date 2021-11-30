using Microsoft.AspNet.Identity;
using MusicHelp.Models;
using MusicHelp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MusicHelper.Controllers.WebAPI
{
    [Authorize]
    [RoutePrefix("api/Tablature")]
    public class TablatureController : ApiController
    {
        private bool SetStarState(int tabId, bool newState)
        {
            var userId = Guid.Parse(User.Identity.GetUserId());
            var service = new TablatureService(userId);

            var detail = service.GetTabByID(tabId);

            var updatedTab =
                new TablatureEdit
                {
                    TabID = detail.TabID,
                    InstrumentID = detail.InstrumentID,
                    TabName = detail.TabName,
                    TabArtist = detail.TabArtist,
                    TabAlbum = detail.TabAlbum,
                    TabDifficulty = detail.TabDifficulty,
                    TabSource = detail.TabSource,
                    TabLink = detail.TabLink,
                    IsStarred = newState
                };

            return service.UpdateTab(updatedTab);
        }

        [Route("{id}/Star")]
        [HttpPut]
        public bool ToggleStarOn(int id) => SetStarState(id, true);

        [Route("{id}/Star")]
        [HttpDelete]
        public bool ToggleStarOff(int id) => SetStarState(id, false);
    }
}
