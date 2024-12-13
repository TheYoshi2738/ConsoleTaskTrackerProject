namespace Core
{
    public class TaskComment
    {
        public string Id { get; }
        public DateTime CreatedAt { get; }
        public string Text { get; }
        public TaskComment(string text)
        {
            Id = Guid.NewGuid().ToString();
            Text = text;
            CreatedAt = DateTime.Now;
        }
        public TaskComment(string id, DateTime createdAt, string text)
        {
            Id = id;
            CreatedAt = createdAt;
            Text = text;
        }
    }
}