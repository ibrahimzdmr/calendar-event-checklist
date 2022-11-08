namespace DataLayer.Entities
{
    public class EventItemLog : EntityBase
    {
        public Guid EventId { get; set; }
        public int CurrentExecutionCount  { get; set; }
    }
}
