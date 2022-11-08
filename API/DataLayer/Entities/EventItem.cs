using DataLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace DataLayer.Entities
{
    public class EventItem : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Frequency Frequency { get; set; }
        public int RepeatTimeInFrequency { get; set; }
        public int ExecutionCount { get; set; } = 0;
        public bool Status { get; set; } = true;
    }
}
