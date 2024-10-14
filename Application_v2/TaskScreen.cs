using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application_v2
{
    public class TaskScreen : IScreen
    {
        public string? Title { get; }
        public List<string>? ScreenBodyLines { get; private set; }
        public IReadOnlyList<MenuActions> Actions { get; }
        public AppContext AppContext { get; }
        public Task Task { get; }

        public TaskScreen(AppContext context, Task task)
        {
            Title = task.Name;
            Task = task;
            AppContext = context;

            CreateBodyLines();

            var actions = new List<MenuActions>();

            actions.Add(new MenuActions("Сменить статус", () =>
            {
                AppContext.PushPreviousScreen(this);
                return new TaskStatusChangeScreen(AppContext, task);
            }));
            actions.Add(new MenuActions("Изменить дедлайн", () => throw new NotImplementedException()));
            actions.Add(new MenuActions("Вернуться ко всем задачам", () => AppContext.PopAndUpdatePreviousScreen() ));

            Actions = actions;
        }

        private void CreateBodyLines()
        {
            ScreenBodyLines =
            [
                "Статус: " + Task.Status.GetStatusNameInRussian(),
                "Дедлайн: " + (Task.DueDate != null ? Task.DueDate!.Value.ToShortDateString() : "Нет"),
            ];
        }

        public void UpdateScreenInfo()
        {
            CreateBodyLines();
        }
    }
}
