using TaskStatus = Core.TaskStatus;

namespace Data.Models
{
    public class TaskEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } 
        public DateOnly CreatedAt { get; set; }
        public DateOnly? DueDate { get; set; }
        public TaskStatus Status { get; set; }
        public List<CommentEntity> Comments { get; set; } = new List<CommentEntity>();
    }
}
