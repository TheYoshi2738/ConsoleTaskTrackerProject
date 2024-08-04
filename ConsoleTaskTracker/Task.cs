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
        public Guid Id { get; } 
        public string Name { get; }
        public DateTime CreatedAt { get; }
        private DateTime _dueDate;
        public DateTime DueDate
        {
            get { return _dueDate.Date; }
            private set
            {
                if (value < DateTime.Now) //получается для решения проблемы надо инкапуслировать DateTime в отдельную сущность?
                {
                    throw new ArgumentException("Дата DueDate не может быть в прошлом");
                } 
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
        }
        public Status TaskStatus { get; private set; }
        public Task() : this($"Задача без названия. Создана {DateTime.Now.ToString("d")} в {DateTime.Now.ToString("t")}") { }
        public Task(string taskName)
        {
            Id = Guid.NewGuid();
            Name = taskName;
            CreatedAt = DateTime.Now;
            TaskStatus = Status.Backlog;
        }
        public Task(string taskName, DateTime dueDate) : this(taskName)
        {
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

            TaskStatus = status;
        }
        public void ChangeDueDate(DateTime dueDate)
        {
            DueDate = dueDate;
        }
    }
}