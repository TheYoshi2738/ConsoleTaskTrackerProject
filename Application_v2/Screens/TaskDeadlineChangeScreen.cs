using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace Application_v2
{
    public class TaskDeadlineChangeScreen : IScreenInput
    {
        public string? Title { get; set; } = "Укажите дедлайн для задачи";
        public IReadOnlyList<MenuActionInput> Actions { get; set; }
        IReadOnlyList<MenuAction> IScreen.Actions => Actions;
        public AppContext AppContext { get; set; }

        public TaskDeadlineChangeScreen(AppContext context, Task task)
        {
            Title = $"{Title} \"{task.Name}\"";
            AppContext = context;

            var actions = new List<MenuActionInput>();

            actions.Add(new MenuActionInput("Дедлайн: ", () =>
            {
                Actions[0].UpdateTitileWithInput();
                return this;
            }, task.DueDate.ToString()));
            actions.Add(new MenuActionInput("Обновить дедлайн и вернуться к задаче", () =>
            {
                task.ChangeDueDate(Actions[0].Input);
                return AppContext.PopPreviousScreen();
            }));

            Actions = actions;
        }
    }
}
