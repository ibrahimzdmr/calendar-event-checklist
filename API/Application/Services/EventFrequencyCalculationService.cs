using API.Data;
using Application.Interfaces;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class EventFrequencyCalculationService : IEventFrequencyCalculationService
    {
        readonly AppDbContext _context;
        readonly IEventService _eventService;
        public EventFrequencyCalculationService(AppDbContext context, IEventService eventService)
        {
            _context = context;
            _eventService = eventService;
        }

        public async Task ControlAllEventsAsync(CancellationToken cancellationToken)
        {
            var events = await _context.Set<EventItem>().Where(i => i.Status).ToListAsync(cancellationToken);

            foreach (var item in events)
            {
                switch (item.Frequency)
                {
                    case DataLayer.Enums.Frequency.Daily:
                        await ControlDailyEventAsync(item, cancellationToken);
                        break;
                    case DataLayer.Enums.Frequency.Weekly:
                        await ControlWeeklyEventAsync(item, cancellationToken);
                        break;
                    case DataLayer.Enums.Frequency.Fortnight:
                        await ControlFortnightlyEventAsync(item, cancellationToken);
                        break;
                    case DataLayer.Enums.Frequency.Monthly:
                        await ControlMonthlyEventAsync(item, cancellationToken);
                        break;
                    case DataLayer.Enums.Frequency.Yearly:
                        await ControlYearlyEventAsync(item, cancellationToken);
                        break;
                    default:
                        break;
                }
                await RefreshLastControlDateAsync(item, cancellationToken);
            }

        }

        private async Task RefreshLastControlDateAsync(EventItem eventItem, CancellationToken cancellationToken)
        {
            eventItem.LastControlDate = DateTime.UtcNow;
            await _context.SaveChangesAsync(cancellationToken);
        }

        private void OneRepeatCalculation(ref EventItem eventItem)
        {
            var repeat = eventItem.CurrentRepeatExecutionCount - eventItem.RepeatTimeInFrequency;
            var point = repeat * eventItem.PointValue;
            eventItem.TotalPoint += point;
            eventItem.TotalRepeatCount += 1;
            eventItem.TotalExecutionCount += eventItem.CurrentRepeatExecutionCount;
            eventItem.CurrentRepeatExecutionCount = 0;
        }

        private void AbsentRepeatCalculation(ref EventItem eventItem, int repeatCount)
        {
            var point = repeatCount * eventItem.PointValue * eventItem.RepeatTimeInFrequency;
            eventItem.TotalPoint -= point;
            eventItem.TotalRepeatCount += repeatCount;
        }

        private async Task ControlDailyEventAsync(EventItem eventItem, CancellationToken cancellationToken)
        {
            var dateDifference = DateTime.UtcNow.Subtract(eventItem.LastRepeatDate);

            if (dateDifference.Days == 1)
            {
                OneRepeatCalculation(ref eventItem);

                eventItem.LastRepeatDate = eventItem.LastRepeatDate.AddDays(1);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else if (dateDifference.Days > 1)
            {
                var dayDifference = dateDifference.Days;

                OneRepeatCalculation(ref eventItem);
                AbsentRepeatCalculation(ref eventItem, dayDifference - 1);
                eventItem.LastRepeatDate = eventItem.LastRepeatDate.AddDays(dayDifference);

                await _context.SaveChangesAsync(cancellationToken);
            }

        }

        private async Task ControlWeeklyEventAsync(EventItem eventItem, CancellationToken cancellationToken)
        {
            var dateDifference = DateTime.UtcNow.Subtract(eventItem.LastRepeatDate.AddDays(-39));

            if (7 <= dateDifference.Days && dateDifference.Days <= 13)
            {
                OneRepeatCalculation(ref eventItem);

                eventItem.LastRepeatDate = eventItem.LastRepeatDate.AddDays(7);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else if ( dateDifference.Days > 13)
            {
                var weekDifference = Convert.ToInt32(Math.Floor(dateDifference.Days / 7.0));

                OneRepeatCalculation(ref eventItem);
                AbsentRepeatCalculation(ref eventItem, weekDifference - 1);
                eventItem.LastRepeatDate = eventItem.LastRepeatDate.AddDays(weekDifference * 7);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }

        private async Task ControlFortnightlyEventAsync(EventItem eventItem, CancellationToken cancellationToken)
        {

        }

        private async Task ControlMonthlyEventAsync(EventItem eventItem, CancellationToken cancellationToken)
        {

        }

        private async Task ControlYearlyEventAsync(EventItem eventItem, CancellationToken cancellationToken)
        {

        }
    }
}
