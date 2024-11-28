namespace Application_v2
{
    public class MainMenuScreen : IScreen
    {
        public string Title { get; } = "Главное меню";
        public IReadOnlyList<MenuAction> Actions { get; }
        public AppContext AppContext { get; }

        public MainMenuScreen(AppContext context)
        {
            AppContext = context;
            var actions = new List<MenuAction>();

            actions.Add(new MenuAction("Показать все задачи", () =>
            {
                AppContext.PushPreviousScreen(this);
                return new AllTasksScreen(context);
            }));
            actions.Add(new MenuAction("Создать задачу", () =>
            {
                context.PushPreviousScreen(this);
                return new CreationTaskScreen(context);
            }));
            actions.Add(new MenuAction("Выйти", () =>
            {
                Environment.Exit(0);
                return null;
            }));

            Actions = actions;
        }
    }
}
