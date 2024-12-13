using Core;

namespace Application_v2
{
    public class TaskStatusChangeScreen : IScreen
    {
        public string? Title { get; }
        public IReadOnlyList<MenuAction> Actions { get; }
        public AppContext AppContext { get; }

        public TaskStatusChangeScreen(AppContext context, Task task)
        {

            AppContext = context;
            var statuses = task.GetAvailableToChangeStatuses();
            var actions = new List<MenuAction>();

            if (statuses != null)
            {
                foreach (var item in statuses)
                {
                    actions.Add(new MenuAction(item.GetStatusNameInRussian(), () =>
                    {
                        task.ChangeStatus(item);
                        AppContext.UpdateTaskInRepository(task.Id, task);
                        return AppContext.PopPreviousScreen();
                    }));
                }

                Title = $"Выбери статус для задачи \"{task.Name}\"";
            }
            else
            {
                Title = $"Невозможно сменить статус у задачи \"{task.Name}\"";
            }
            actions.Add(new MenuAction("Вернуться к задаче", () => AppContext.PopPreviousScreen()));

            Actions = actions;
        }
    }
}
