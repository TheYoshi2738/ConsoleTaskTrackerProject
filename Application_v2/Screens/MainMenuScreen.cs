namespace Application_v2
{
    public class MainMenuScreen : IScreen
    {
        public string Title { get; } = "Главное меню";
        public IReadOnlyList<MenuActions> Actions { get; }
        public AppContext AppContext { get; }

        public MainMenuScreen(AppContext context)
        {
            AppContext = context;
            var actions = new List<MenuActions>();

            actions.Add(new MenuActions("Показать все задачи", () =>
            {
                AppContext.PushPreviousScreen(this);
                return new AllTasksScreen(context);
            }));
            actions.Add(new MenuActions("Создать задачу", () =>
            {
                context.PushPreviousScreen(this);
                return new CreationTaskScreen(context);
            }));
            actions.Add(new MenuActions("Выйти", () =>
            {
                Environment.Exit(0);
                return null;
            }));

            Actions = actions;
        }
    }
}
