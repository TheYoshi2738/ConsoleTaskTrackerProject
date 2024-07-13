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
        public enum Status
        {
            Backlog,
            InProgress,
            Done,
            Trashed
        }
        public readonly int Id;
        public string Name { get; private set; }
        public readonly DateTime CreatedAt;
        private DateTime _dueDate;
        public DateTime DueDate
        {
            get { return _dueDate; }
            private set
            {
                if (value < DateTime.Now)
                    throw new ArgumentException("Дата DueDate не может быть в прошлом");
            }
        }
        public bool IsActive { get; set; }
        private Status _taskStatus;
        public Status TaskStatus
        {
            get { return _taskStatus; }
            private set
            {
                if (value == Status.Trashed ||
                    value == Status.Done)
                    IsActive = false;
                _taskStatus = value;
            }
        }

        public Task() : this("Задача без названия")
        {

        }
        public Task(string taskName)
        {
            Id = IdGenerator.GetNewId();
            Name = taskName;
            CreatedAt = DateTime.Now;
            IsActive = true;
            TaskStatus = Status.Backlog;
        }

        public Task(string taskName, DateTime dueDate) : this(taskName)
        {
            DueDate = dueDate;
        }

        public void ChangeStatus(Status status)
        {
            if (!IsActive)
                throw new ArgumentException("Невозможно изменить статус у неактивной задачи");

            if (TaskStatus == Status.Backlog && status == Status.Done)
                throw new ArgumentException("Задачу нельзя перевести в Done из Backlog");

            TaskStatus = status;
        }

        public void ChangeDueDate(DateTime dueDate)
        {

        }
    }
}