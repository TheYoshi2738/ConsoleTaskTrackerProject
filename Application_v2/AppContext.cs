using Core;

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
            var screen = _previousScreen.Pop();
            if (screen is IDynamicScreen)
            {
                ((IDynamicScreen)screen).UpdateScreenInfo(); //каст должен быть безопасен. Вопрос насколько такой способ приемлем
            }

            return screen;
        }
    }
}
