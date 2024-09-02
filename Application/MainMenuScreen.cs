using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application
{
    public class MainMenuScreen : IAppScreen
    {
        public string Title { get; }
        public string[] MenuItems { get; }
        public List<IAppScreen> AppScreens { get; }
        private IAppScreen PreviousScreen {  get; set; }

        public MainMenuScreen(string title, string[] menuItems, IAppScreen[] appScreens)
        {
            Title = title;
            MenuItems = menuItems;
            AppScreens = appScreens.ToList<IAppScreen>();
            foreach (var appScreen in AppScreens)
            {
                appScreen.SetPreviousScreen(this);
            }
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
