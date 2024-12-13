using System;

namespace Core
{
    public interface ITaskRepository
    {
        public IReadOnlyList<Task> GetAllTasks();
        public void CreateTask(Task task);
        public void UpdateTask(string taskId, Task actualTask);
        public void DeleteTask(Task task);
    }
}
