using System;

namespace Core
{
    public interface ITaskRepository
    {
        public void CreateTask(Task task);
        public IReadOnlyList<Task> GetAllTasks();
    }
}
