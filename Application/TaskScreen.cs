using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using Task = Core.Task;

namespace Application
{
    internal class TaskScreen : IAppScreen
    {
        public string Title { get; }
        public string[] MenuItems { get; }
        public Task Task { get; }
        public List<IAppScreen> AppScreens { get; }
        private IAppScreen PreviousScreen { get; set; }


        public TaskScreen(Task task)
        {
            Task = task;
            MenuItems =
            [
                "Сменить статус",
                "Изменить название",
                "Изменить дедлайн",
                "Вернуться ко всем задачам"
            ];

            AppScreens = new List<IAppScreen>();
        }

        public IAppScreen NextScreen(int menuItemIndex)
        {
            return menuItemIndex == MenuItems.Length - 1 ? PreviousScreen : AppScreens[menuItemIndex];
        }

        public void Print()
        {
            Console.Clear();
            Console.SetCursorPosition(3, 2);

            Console.WriteLine(Task.Name);
            Console.WriteLine();
            Console.WriteLine($"Статус: {Task.TaskStatus.ToString()}");
            Console.WriteLine($"Дедлайн: {Task.DueDate}");

            Console.CursorTop += 2;

            foreach (var item in MenuItems)
            {
                Console.CursorLeft = 2;
                Console.WriteLine(item);
            }
        }

        public void SetPreviousScreen(IAppScreen previousScreen)
        {
            if (PreviousScreen != null)
            {
                throw new InvalidOperationException($"Предыдущий экран для {GetType().Name} уже установлен");
            }
            PreviousScreen = previousScreen;
        }
    }
}
