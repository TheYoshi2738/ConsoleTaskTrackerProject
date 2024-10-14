using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using Core;

namespace Application_v2
{
    internal class CreationTaskScreen : IScreen
    {
        public string? Title { get; } = "Создание задачи";
        public List<string>? ScreenBodyLines { get; }
        public IReadOnlyList<MenuActions> Actions { get; }
        public AppContext AppContext { get; }
        private List<(string, string?)>  actionTitlesWithInputs { get; set; }

        public CreationTaskScreen(AppContext context)
        {
            AppContext = context;
            var actions = new List<MenuActions>();
            actionTitlesWithInputs = new List<(string, string?)>()
            {
                ("Название: ", null),
                ("Дедлайн: ", null)
            };

            actions.Add(new MenuActions(actionTitlesWithInputs[0].Item1, () =>
            {
                UpdateActionTitileWithInput(0);
                return this;
            }));
            actions.Add(new MenuActions(actionTitlesWithInputs[1].Item1, () =>
            {
                UpdateActionTitileWithInput(1);
                return this;
            }));
            actions.Add(new MenuActions("Создать задачу", () =>
            {
                AppContext.AllTasks.Add(new Task(actionTitlesWithInputs[0].Item2, actionTitlesWithInputs[1].Item2));
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
            return String.IsNullOrEmpty(input) ? "" : input;
        }
        private void UpdateActionTitileWithInput(int menuItemIndex)
        {
            var input = GetInputFromScreen();
            var currentItem = actionTitlesWithInputs[menuItemIndex];
            currentItem.Item2 = input;
            actionTitlesWithInputs[menuItemIndex] = currentItem;
            Actions![menuItemIndex].Title = currentItem.Item1 + currentItem.Item2;
        }
        public void UpdateScreenInfo()
        {
            throw new NotImplementedException();
        }
    }
}
