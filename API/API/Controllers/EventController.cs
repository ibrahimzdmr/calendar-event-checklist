using Application.Interfaces;
using DataLayer.Entities;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("Event")]
    public class EventController : Controller
    {
        readonly IEventService _eventService;
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IEnumerable<EventItem>> GetEventItemsAsync(CancellationToken cancellationToken)
        {
            return await _eventService.GetEventItemsAsync(cancellationToken);
        }

        [HttpPost]
        public async Task CreateEventAsync([FromBody] EventCreateRequest request, CancellationToken cancellationToken)
        {
            await _eventService.CreateEventAsync(request, cancellationToken);
        }

        [HttpDelete]
        public async Task DeleteEventAsync(Guid eventId, CancellationToken cancellationToken)
        {
            await _eventService.DeleteEventAsync(eventId, cancellationToken);
        }

        [HttpPut]
        public async Task UpdateEventAsync([FromBody] EventItem request, CancellationToken cancellationToken)
        {
            await _eventService.UpdateEventAsync(request, cancellationToken);
        }
    }
}
