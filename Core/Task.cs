using Newtonsoft.Json;
using System.Reflection.Emit;

namespace Core
{
    public class Task
    {
        public string Id { get; }
        public string Name { get; }
        public DateOnly CreatedAt { get; }
        public DateOnly? DueDate { get; private set; }
        public TaskStatus Status { get; private set; }
        public bool IsActive { get => !(Status == TaskStatus.Trashed || Status == TaskStatus.Done); }
        public IReadOnlyList<TaskComment> Comments { get => _comments; }
        private List<TaskComment> _comments {  get; } = new List<TaskComment>();
        public Task() : this(null)
        {
        }
        public Task(string? taskName)
        {
            Id = Guid.NewGuid().ToString();
            Name = taskName != null ? taskName.Trim() : "Задача " + Id.Substring(0, 5);
            CreatedAt = DateOnly.FromDateTime(DateTime.Now);
            Status = TaskStatus.Backlog;
        }
        public Task(string? taskName, string? dueDate) : this(taskName)
        {
            if (DateOnly.TryParse(dueDate, out var date))
            {
                DueDate = date;
            }
        }
        public Task(string id, string name, DateOnly createdAt, DateOnly? dueDate, TaskStatus status, List<TaskComment> comments)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
            DueDate = dueDate;
            Status = status;
            _comments = comments;
        }
        public IReadOnlyList<TaskStatus>? GetAvailableToChangeStatuses()
        {
            if (!IsActive)
                return null;

            var statuses = new List<TaskStatus>();

            for (var i = 0; i < Status.GetPossibleStatusesCount(); i++)
            {
                if (Status == (TaskStatus)i)
                {
                    continue;
                }
                if (Status == TaskStatus.Backlog && (TaskStatus)i == TaskStatus.Done)
                {
                    continue;
                }
                statuses.Add((TaskStatus)i);
            }
            return statuses;
        }
        public void ChangeStatus(TaskStatus status)
        {
            if (!IsActive)
            {
                throw new InvalidOperationException("Невозможно изменить статус у неактивной задачи");
            }

            if (Status == TaskStatus.Backlog && status == TaskStatus.Done)
            {
                throw new InvalidOperationException("Задачу нельзя перевести в Done из Backlog");
            }
            Status = status;
        }
        public void ChangeDueDate(DateOnly dueDate)
        {
            DueDate = dueDate;
        }
        public void ChangeDueDate(string dueDate)
        {
            if (DateOnly.TryParse(dueDate, out var date))
                ChangeDueDate(date);
        }
        public IEnumerable<string> GetCommentsForScreen()
        {
            if (_comments.Count == 0) yield break;

            foreach (var comment in _comments)
            {
                yield return $"{comment.CreatedAt} {comment.Text}";
            }
        }
        public void AddComment(string comment)
        {
            _comments.Add(new TaskComment(comment));
        }

    }
}