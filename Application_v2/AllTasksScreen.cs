using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application_v2
{
    public class AllTasksScreen : IScreen
    {
        public string Title { get; } = "Все задачи";
        public List<string>? ScreenBodyLines { get; }
        public IReadOnlyList<MenuActions> Actions { get; private set; }
        public AppContext AppContext { get; }

        public AllTasksScreen(AppContext context)
        {
            AppContext = context;
            Actions = CreateMenuActions(); 
        }
        private IReadOnlyList<MenuActions> CreateMenuActions()
        {
            var actions = new List<MenuActions>();

            foreach (var task in AppContext.AllTasks)
            {
                actions.Add(new MenuActions(task.Name, () =>
                {
                    AppContext.PushPreviousScreen(this);
                    return new TaskScreen(AppContext, task);
                })
                );
            }
            actions.Add(new MenuActions("Вернуться в главное меню", () => AppContext.PopPreviousScreen()));

            return actions;
        }

        public void UpdateScreenInfo()
        {
            Actions = CreateMenuActions();
        }
    }
}
