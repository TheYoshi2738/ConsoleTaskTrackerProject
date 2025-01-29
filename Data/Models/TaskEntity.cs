using TaskStatus = Core.TaskStatus;

namespace Data.Models
{
    public class TaskEntity
    {
        public Guid Id { get; init; }
        public required string Name { get; init; } 
        public DateOnly CreatedAt { get; init; }
        public DateOnly? DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public List<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
    }
}
