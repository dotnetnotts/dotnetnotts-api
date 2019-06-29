using System.Threading;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

using Meetup.Api;

namespace dotnetnottsapi.Controllers
{
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        // GET: api/events
        [HttpGet]
        public async Task<Events> Get()
        {
            return await MeetupApi.Events.Events(13372672, PublishStatus.past, CancellationToken.None);
        }

        // GET api/events/next
        [HttpGet("next")]
        public async Task<Events> GetNext()
        {
            return await MeetupApi.Events.Events(13372672, PublishStatus.upcoming, CancellationToken.None);
        }
    }
}
