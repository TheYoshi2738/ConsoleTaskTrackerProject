using System.IO;
using Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Application_v2
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appconfig.json")
                .Build();

            var dbContext = new TaskTrackerDbContext(new DbContextOptionsBuilder<TaskTrackerDbContext>()
                .UseNpgsql(config.GetConnectionString("postgres")));
            
            var repository = new TasksRepository(dbContext);

            AppContext applicationContext = new AppContext(repository);

            Console.CursorVisible = false;
            IScreen? currentScreen = new MainMenuScreen(applicationContext);

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
