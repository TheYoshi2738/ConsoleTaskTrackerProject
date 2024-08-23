using System;

namespace Core
{
    public interface ITaskRepository
    {
        public void Save(Task task);
        public List<Task> GetAll();
    }
}
