using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application_v2
{
    public class TaskStatusChangeScreen : IScreen
    {
        public string? Title { get; }
        public List<string>? ScreenBodyLines { get; }
        public IReadOnlyList<MenuActions> Actions { get; }
        public AppContext AppContext { get; }

        public TaskStatusChangeScreen(AppContext context, Task task)
        {

            AppContext = context;
            var statuses = task.GetAvailableToChangeStatuses();
            var actions = new List<MenuActions>();

            if (statuses != null)
            {
                foreach (var item in statuses)
                {
                    actions.Add(new MenuActions(item.GetStatusNameInRussian(), () =>
                    {
                        task.ChangeStatus(item);
                        return AppContext.PopAndUpdatePreviousScreen();
                    }));
                }

                Title = $"Выбери статус для задачи \"{task.Name}\"";

            }
            else
            {
                Title = $"Невозможно сменить статус у задачи \"{task.Name}\"";
            }
            actions.Add(new MenuActions("Вернуться к задаче", () => AppContext.PopAndUpdatePreviousScreen()));

            Actions = actions;
        }

        public void UpdateScreenInfo()
        {
            throw new NotImplementedException();
        }
    }
}
