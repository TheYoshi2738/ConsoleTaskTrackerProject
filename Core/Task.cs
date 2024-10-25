using Newtonsoft.Json;

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
            if (DateTime.TryParse(dueDate, out var date))
            {
                DueDate = DateOnly.FromDateTime(date);
            }
        }
        public Task(string id, string name, DateOnly createdAt, DateOnly? dueDate, TaskStatus status)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
            DueDate = dueDate;
            Status = status;
        }
        public IReadOnlyList<TaskStatus>? GetAvailableToChangeStatuses()//А точно ли именно у Task должен быть такой метод?
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

        public override string ToString()
        {
            return $"{Name}, Статус: {Status}, Дедлайн: {DueDate}";
        }
    }
}