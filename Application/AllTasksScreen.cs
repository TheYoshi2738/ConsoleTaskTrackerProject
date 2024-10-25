using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Core;
using Task = Core.Task;

namespace Application
{
    public class AllTasksScreen : IAppScreen
    {
        public string Title { get; }
        public IReadOnlyList<Task> Tasks { get; }
        public string[] MenuItems { get; }
        public List<IAppScreen> AppScreens { get; }
        public IAppScreen PreviousScreen { get; set; }

        public AllTasksScreen(string title, IReadOnlyList<Task> allTasks)
        {
            Title = title;

            MenuItems = new string[allTasks.Count + 1];
            AppScreens = new List<IAppScreen>();
            for (int i = 0; i < allTasks.Count; i++)
            {
                MenuItems[i] = allTasks[i].ToString();
                AppScreens.Add(new TaskScreen(allTasks[i]));
                AppScreens[i].SetPreviousScreen(this);
            }

            MenuItems[MenuItems.Length - 1] = "Вернуться в меню";
        }

        public IAppScreen NextScreen(int menuItemIndex)
        {
            return menuItemIndex == MenuItems.Length - 1 ? PreviousScreen : AppScreens[menuItemIndex];
        }

        public void Print()
        {
            Console.Clear();
            Console.SetCursorPosition(3, 2);
            Console.WriteLine(Title);

            Console.CursorTop += 2;

            if (MenuItems == null || MenuItems.Length == 0)
            {
                Console.WriteLine("Задач нет");
                return;
            }
            foreach (var item in MenuItems)
            {
                Console.CursorLeft = 2;
                Console.WriteLine(item);
            }
        }
        public  void SetPreviousScreen(IAppScreen previousScreen)
        {
            if (PreviousScreen != null)
            {
                throw new InvalidOperationException($"Предыдущий экран для {GetType().Name} уже установлен");
            }
            PreviousScreen = previousScreen;
        }
    }
}
