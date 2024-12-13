using Data;
using Microsoft.EntityFrameworkCore;

namespace Application_v2
{
    public class Program
    {
        static void Main(string[] args)
        {
            //var dbContextOptions = new DbContextOptionsBuilder<TaskTrackerDbContext>();

            var dbContext = new TaskTrackerDbContext();
            var repository = new TasksRepository(dbContext);

            AppContext applicationContext = new AppContext(repository);

            Console.CursorVisible = false;
            IScreen currentScreen = new MainMenuScreen(applicationContext);

            while (true)
            {
                if (currentScreen == null) break;
                var printer = new ScreenPrinter(currentScreen);
                var manager = new MenuManager(currentScreen, printer);
                var nextScreen = manager.ManageMenu();
                currentScreen = nextScreen;
            }
            Console.WriteLine("Вышли из приложения");
        }
    }

}
