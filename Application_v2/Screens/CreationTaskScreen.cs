using Core;

namespace Application_v2
{
    internal class CreationTaskScreen : IScreenInput
    {
        public string? Title { get; } = "Создание задачи";
        public IReadOnlyList<MenuActionInput> Actions { get; }
        IReadOnlyList<MenuAction> IScreen.Actions => Actions;
        public AppContext AppContext { get; }


        public CreationTaskScreen(AppContext context)
        {
            AppContext = context;
            var actions = new List<MenuActionInput>();

            actions.Add(new MenuActionInput("Название: ", () =>
            {
                Actions[0].UpdateTitileWithInput();
                return this;
            }));
            actions.Add(new MenuActionInput("Дедлайн: ", () =>
            {
                Actions[1].UpdateTitileWithInput();
                return this;
            }));
            actions.Add(new MenuActionInput("Создать задачу", () =>
            {
                AppContext.AllTasks.Add(new Task(Actions[0].Input, Actions[1].Input));
                return AppContext.PopPreviousScreen();
            }));
            actions.Add(new MenuActionInput("Вернуться в меню", () => AppContext.PopPreviousScreen()));

            Actions = actions;
        }
    }
}
