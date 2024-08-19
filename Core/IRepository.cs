using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleTaskTracker
{
    public interface ITaskRepository
    {
        public void Save(Task task);
        public List<Task> GetAll();
    }
}
