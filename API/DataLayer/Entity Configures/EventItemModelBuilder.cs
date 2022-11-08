using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EventItemModelBuilder : IEntityTypeConfiguration<EventItem>
{
    public void Configure(EntityTypeBuilder<EventItem> builder)
    {
        builder.HasKey(i => i.Id).HasName("PrimaryKey_EventId");
    }
}