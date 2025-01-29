namespace Data.Models
{
    public class CommentEntity
    {
        public Guid Id { get; init; }
        public required string Text { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
        public required TaskEntity TaskEntity { get; init; }
        public Guid TaskId { get; init; }
    }
}
