using Newtonsoft.Json;

namespace Core
{
    public class Task
    {
        public string Id { get; }
        public string Name { get; }
        public DateOnly CreatedAt { get; }
        public DateOnly? DueDate { get; private set; }
        public Status TaskStatus { get; private set; }
        public bool IsActive { get => !(TaskStatus == Status.Trashed || TaskStatus == Status.Done); }
        public Task() : this(null) 
        {
            Name  = "Задача " + Id.Substring(0, 5);
        }
        public Task(string taskName)
        {
            Id = Guid.NewGuid().ToString();
            Name = taskName;
            CreatedAt = DateOnly.FromDateTime(DateTime.Now);
            TaskStatus = Status.Backlog;
        }
        public Task(string id, string name, DateOnly createdAt, DateOnly? dueDate, Status status)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
            DueDate = dueDate;
            TaskStatus = status;
        }
        public void ChangeStatus(Status status)
        {
            if (!IsActive)
            {
                throw new InvalidOperationException("Невозможно изменить статус у неактивной задачи");
            }

            if (TaskStatus == Status.Backlog && status == Status.Done)
            {
                throw new InvalidOperationException("Задачу нельзя перевести в Done из Backlog");
            }
            TaskStatus = status;
        }
        public void ChangeDueDate(DateOnly dueDate)
        {
            DueDate = dueDate;
        }
    }
}