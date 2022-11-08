using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EventItemLogModelBuilder : IEntityTypeConfiguration<EventItemLog>
{
    public void Configure(EntityTypeBuilder<EventItemLog> builder)
    {
        builder.HasKey(i => i.Id).HasName("PrimaryKey_EventLogId");
    }
}