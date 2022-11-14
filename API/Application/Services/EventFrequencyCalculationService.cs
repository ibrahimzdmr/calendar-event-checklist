using API.Data;
using Application.Interfaces;
using DataLayer.Entities;
using DataLayer.Enums;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class EventFrequencyCalculationService : IEventFrequencyCalculationService
    {
        readonly AppDbContext _context;
        public EventFrequencyCalculationService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ControlAllEventsAsync(CancellationToken cancellationToken)
        {
            var events = await _context.Set<EventItem>().Where(i => i.Status).ToListAsync(cancellationToken);

            foreach (var item in events)
            {
                await ControlEventAsync(item, GetDayFromFrequency(item.Frequency), cancellationToken);
                await RefreshLastControlDateAsync(item, cancellationToken);
            }

            return true;
        }

        private int GetDayFromFrequency(Frequency frequency)
        {
            switch (frequency)
            {
                case Frequency.Daily:
                    return 1;
                case Frequency.Weekly:
                    return 7;
                case Frequency.Fortnight:
                    return 14;
                case Frequency.Monthly:
                    return 30;
                case Frequency.Yearly:
                    return 365;
                default:
                    return int.MaxValue;
            }
        }

        private async Task RefreshLastControlDateAsync(EventItem eventItem, CancellationToken cancellationToken)
        {
            eventItem.LastControlDate = DateTime.UtcNow;
            eventItem.NextRepeatDate = eventItem.LastRepeatDate.AddDays(GetDayFromFrequency(eventItem.Frequency));
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

        private async Task ControlEventAsync(EventItem eventItem, int day, CancellationToken cancellationToken)
        {
            var dateDifference = DateTime.UtcNow.Subtract(eventItem.LastRepeatDate);
            if ( day <= dateDifference.Days && dateDifference.Days <= (day * 2) - 1)
            {
                OneRepeatCalculation(ref eventItem);

                eventItem.LastRepeatDate = eventItem.LastRepeatDate.AddDays(day);
                await _context.SaveChangesAsync(cancellationToken);
            }
            else if( dateDifference.Days >= (day * 2))
            {
                var dayDifference = Convert.ToInt32(Math.Floor(dateDifference.Days / day * 1.0));

                OneRepeatCalculation(ref eventItem);
                AbsentRepeatCalculation(ref eventItem, dayDifference - 1);
                eventItem.LastRepeatDate = eventItem.LastRepeatDate.AddDays(dayDifference);

                await _context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
