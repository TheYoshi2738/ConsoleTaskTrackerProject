using System;
using Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application_v2
{
    public class AppContext
    {
        public List<Task> AllTasks { get; private set; }
        public readonly Stack<IScreen> _previousScreen = new Stack<IScreen>();
        public ITaskRepository TaskRepository { get; }

        public AppContext(ITaskRepository repository)
        {
            AllTasks = repository.GetAllTasks().ToList();
            TaskRepository = repository;
        }
        public void PushPreviousScreen(IScreen previousScreen)
        {
            _previousScreen.Push(previousScreen);
        }
        public IScreen PopPreviousScreen()
        {
            return _previousScreen.Pop();
        }
        public IScreen PopAndUpdatePreviousScreen()
        {
            var screen = _previousScreen.Pop();
            screen.UpdateScreenInfo();
            return screen;
        }
    }
}
