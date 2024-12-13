using Core;

namespace Application_v2
{
    public class AppContext
    {
        public IReadOnlyList<Task> AllTasks
        {
            get
            {
                return _taskRepository.GetAllTasks();
            }
        }
        public readonly Stack<IScreen> _previousScreen = new Stack<IScreen>();
        private ITaskRepository _taskRepository { get; }

        public AppContext(ITaskRepository repository)
        {
            _taskRepository = repository;
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
        public void AddTaskToRepository(Task task)
        {
            _taskRepository.CreateTask(task);
        }
        public void UpdateTaskInRepository(string taskId, Task actualTask)
        {
            _taskRepository.UpdateTask(taskId, actualTask);
        }
        public void DeleteTaskInRepository(Task taskId)
        {
            _taskRepository.DeleteTask(taskId);
        }
    }
}
