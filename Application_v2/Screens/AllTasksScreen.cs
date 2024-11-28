using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application_v2
{
    public class AllTasksScreen : IDynamicScreen
    {
        public string Title { get; } = "Все задачи";
        public IReadOnlyList<MenuAction> Actions { get; private set; }
        public AppContext AppContext { get; }

        public AllTasksScreen(AppContext context)
        {
            AppContext = context;
            Actions = CreateMenuActions();
        }
        private IReadOnlyList<MenuAction> CreateMenuActions()
        {
            var actions = new List<MenuAction>();

            foreach (var task in AppContext.AllTasks)
            {
                actions.Add(new MenuAction(task.Name, () =>
                {
                    AppContext.PushPreviousScreen(this);
                    return new TaskScreen(AppContext, task);
                })
                );
            }
            actions.Add(new MenuAction("Вернуться в главное меню", () => AppContext.PopPreviousScreen()));

            return actions;
        }
        public void UpdateScreenInfo()
        {
            Actions = CreateMenuActions();
        }
    }
}
