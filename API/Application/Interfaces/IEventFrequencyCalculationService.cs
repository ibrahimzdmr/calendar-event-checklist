

namespace Application.Services
{
    public interface IEventFrequencyCalculationService
    {
        Task<bool> ControlAllEventsAsync(CancellationToken cancellationToken);
    }
}
