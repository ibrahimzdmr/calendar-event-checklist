using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("EventFrequencyCalculation")]
    public class EventFrequencyCalculationController : Controller
    {
        readonly IEventFrequencyCalculationService _eventFrequencyCalculationService;
        public EventFrequencyCalculationController(IEventFrequencyCalculationService eventFrequencyCalculationService)
        {
            _eventFrequencyCalculationService = eventFrequencyCalculationService;
        }

        [HttpPost("Control")]
        public async Task ControlAllEventsAsync(CancellationToken cancellationToken)
        {
            await _eventFrequencyCalculationService.ControlAllEventsAsync(cancellationToken);
        }
    }
}
