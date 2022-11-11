
namespace Application.Services
{
    public interface IEventFrequencyCalculationService
    {
        Task ControlAllEventsAsync(CancellationToken cancellationToken);
    }
}
