using DataLayer.Enums;

namespace DataLayer.Entities
{
    public class EventItem : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Frequency Frequency { get; set; }
        public int RepeatTimeInFrequency { get; set; }
        public int CurrentRepeatExecutionCount { get; set; } = 0;
        public DateTime LastControlDate { get; set; } = DateTime.UtcNow;
        public DateTime LastRepeatDate { get; set; } = DateTime.UtcNow;
        public double PointValue { get; set; }
        public double TotalPoint { get; set; } = 0;
        public int TotalRepeatCount { get; set; } = 0;
        public int TotalExecutionCount { get; set; } = 0;
        public bool Status { get; set; } = true;
    }
}
