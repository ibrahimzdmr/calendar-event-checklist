using Application.Services;
using DataLayer.Entities;
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
        public async Task<bool> ControlAllEventsAsync(CancellationToken cancellationToken)
        {
            return await _eventFrequencyCalculationService.ControlAllEventsAsync(cancellationToken);
        }
    }
}
