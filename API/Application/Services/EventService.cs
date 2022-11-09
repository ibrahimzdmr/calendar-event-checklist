using API.Data;
using Application.Interfaces;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class EventService : IEventService
    {
        readonly AppDbContext _context;
        readonly IMapper _mapper;
        public EventService(AppDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventItem>> GetEventItemsAsync(CancellationToken cancellationToken)
        {
            return await _context.Set<EventItem>().AsNoTracking().Where(i => i.Status).ToListAsync(cancellationToken);
        }

        public async Task CreateEventAsync(EventCreateRequest request, CancellationToken cancellationToken)
        {
            var newEvent = _mapper.Map<EventItem>(request);
            await _context.Set<EventItem>().AddAsync(newEvent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEventAsync(Guid eventId, CancellationToken cancellationToken)
        {
            var eventItem = await _context.Set<EventItem>().FirstOrDefaultAsync(i => i.Id == eventId, cancellationToken);
            if (eventItem == null)
                throw new NullReferenceException();

            eventItem.Status = false;
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateEventAsync(EventItem eventItem, CancellationToken cancellationToken)
        {

        }


    }
}
