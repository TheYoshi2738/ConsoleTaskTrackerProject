using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.HasKey(t => t.Id);

            builder
                .HasMany(t => t.Comments)
                .WithOne(c => c.TaskEntity)
                .HasForeignKey(c => c.TaskId);
        }
    }
}
