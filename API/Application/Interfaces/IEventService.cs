using DataLayer.Entities;
using DataLayer.Models;

namespace Application.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventItem>> GetEventItemsAsync(CancellationToken cancellationToken);
        Task CreateEventAsync(EventCreateRequest request, CancellationToken cancellationToken);
        Task DeleteEventAsync(Guid eventId, CancellationToken cancellationToken);
        Task UpdateEventAsync(EventItem eventItem, CancellationToken cancellationToken);
    }
}
