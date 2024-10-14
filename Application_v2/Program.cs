
using Core;
using Data;
using System.Reflection;
using System.Text.RegularExpressions;
using System;

namespace Application_v2
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region
            var path = Assembly.GetExecutingAssembly().Location;
            var regex = new Regex(".*ConsoleTaskTrackerProject\\b");
            path = Convert.ToString(regex.Match(path));
            var jsonRepository = new RepositoryJson(path + "\\jsonRepository.json");
            #endregion


            AppContext applicationContext = new AppContext(jsonRepository);
            Console.CursorVisible = false;

            IScreen currentScreen = new MainMenuScreen(applicationContext);

            while (true)
            {
                var printer = new ScreenPrinter(currentScreen);
                var manager = new MenuManager(currentScreen, printer);
                var nextScreen = manager.ManageMenu();
                currentScreen = nextScreen;
            }
        }
    }

}
