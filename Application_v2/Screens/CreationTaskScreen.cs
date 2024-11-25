using Core;

namespace Application_v2
{
    internal class CreationTaskScreen : IScreen
    {
        public string? Title { get; } = "Создание задачи";
        public IReadOnlyList<MenuActions> Actions { get; }
        public AppContext AppContext { get; }
        private List<TitleInput> _actionTitlesWithInputs { get; set; }

        public CreationTaskScreen(AppContext context)
        {
            AppContext = context;
            var actions = new List<MenuActions>();
            _actionTitlesWithInputs = new List<TitleInput>()
            {
                new TitleInput("Название: "),
                new TitleInput("Дедлайн: ")
            };

            actions.Add(new MenuActions(_actionTitlesWithInputs[0].Title, () =>
            {
                UpdateActionTitileWithInput(0);
                return this;
            }));
            actions.Add(new MenuActions(_actionTitlesWithInputs[1].Title, () =>
            {
                UpdateActionTitileWithInput(1);
                return this;
            }));
            actions.Add(new MenuActions("Создать задачу", () =>
            {
                AppContext.AllTasks.Add(new Task(_actionTitlesWithInputs[0].Input, _actionTitlesWithInputs[1].Input));
                return AppContext.PopPreviousScreen();
            }));
            actions.Add(new MenuActions("Вернуться в меню", () => AppContext.PopPreviousScreen()));

            Actions = actions;
        }
        private string GetInputFromScreen()
        {
            var inputLinePadding = 2;
            Console.SetCursorPosition(Console.CursorLeft, Console.CursorTop + inputLinePadding);
            Console.Write("Ввод: ");
            var input = Console.ReadLine();
            return string.IsNullOrEmpty(input) ? "" : input;
        }
        private void UpdateActionTitileWithInput(int menuItemIndex)
        {
            var input = GetInputFromScreen();
            var currentItem = _actionTitlesWithInputs[menuItemIndex];
            currentItem.Input = input;
            _actionTitlesWithInputs[menuItemIndex] = currentItem;
            Actions[menuItemIndex].Title = currentItem.Title + currentItem.Input;
        }
        private class TitleInput(string title, string? input = null)
        {
            public string Title { get; set; } = title;
            public string? Input { get; set; } = input;
        }
    }
}
