using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTaskTracker
{
    public class Task
    {
        [JsonProperty]
        public string Id { get; private set; }
        [JsonProperty]
        public string Name { get; private set; }
        [JsonProperty]
        public DateTime CreatedAt { get; private set; }
        private DateTime? _dueDate;
        [JsonProperty]
        public DateTime? DueDate
        {
            get
            {
                if (_dueDate.Equals(DateTime.MinValue))
                {
                    return null;
                }
                return _dueDate;
            }
            private set
            {
                _dueDate = value;
            }
        }
        public bool IsActive
        {
            get
            {
                if (TaskStatus == Status.Trashed ||
                    TaskStatus == Status.Done)
                {
                    return false;
                }
                return true;
            }
            set
            {

            }
        }
        public Status TaskStatus { get; private set; }
        public Task() : this($"Задача без названия. Создана {DateTime.Now.ToString("d")} в {DateTime.Now.ToString("t")}") { }
        public Task(string taskName)
        {
            Id = Guid.NewGuid().ToString();
            Name = taskName;
            CreatedAt = DateTime.Now;
            TaskStatus = Status.Backlog;
        }
        public Task(string taskName, DateTime dueDate) : this(taskName)
        {
            if (dueDate < DateTime.Now)
            {
                throw new ArgumentException("Дата дедлайна не может быть в прошло");
            }
            DueDate = dueDate;
        }
        public void ChangeStatus(Status status)
        {
            if (!IsActive)
            {
                throw new ArgumentException("Невозможно изменить статус у неактивной задачи");
            }

            if (TaskStatus == Status.Backlog && status == Status.Done)
            {
                throw new ArgumentException("Задачу нельзя перевести в Done из Backlog");
            }
        }
        public void ChangeDueDate(DateTime dueDate)
        {
            DueDate = dueDate;
        }

    }
}