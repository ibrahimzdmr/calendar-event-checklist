using DataLayer.Enums;

namespace DataLayer.Models
{
    public class EventCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Frequency Frequency { get; set; }
        public int RepeatTimeInFrequency { get; set; }
        public double PointValue { get; set; }
    }
}
