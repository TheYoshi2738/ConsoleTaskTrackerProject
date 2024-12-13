namespace Data.Models
{
    public class CommentEntity
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public TaskEntity TaskEntity { get; set; }
        public Guid TaskId { get; set; }
    }
}
