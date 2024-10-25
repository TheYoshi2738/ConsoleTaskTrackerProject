using System;

namespace Core
{
    public interface ITaskRepository
    {
        public void CreateTask(Task task);
        public void UpdateTask(Task task);
        public IReadOnlyList<Task> GetAllTasks();
    }
}
