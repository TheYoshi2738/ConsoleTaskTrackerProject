using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Core;
using Task = Core.Task;
using Data;

namespace Application;

public class Program
{
    public static void Main()
    {
        //попытка держать все такси в одном месте и искать путь до ДБ динамично
        var path = Assembly.GetExecutingAssembly().Location;
        var regex = new Regex(".*ConsoleTaskTrackerProject\\b");
        path = Convert.ToString(regex.Match(path));
        var jsonRepository = new RepositoryJson(path + "\\jsonRepository.json");

        //StartChat(jsonRepository);
        ConfigureApplication(jsonRepository);
    }

    public static void ConfigureApplication(ITaskRepository taskRepository)
    {
        var allTasks = taskRepository.GetAllTasks();

        var menuScreen = new MainMenuScreen("Главное меню",
        [
            "Показать все задачи",
            "Создать задачу",
            "Выйти"
        ],
        [
            new AllTasksScreen("Все задачи", allTasks)
        ]);
        ManageApplication(menuScreen);
    }

    public static void ManageApplication(MainMenuScreen mainMenuScreen)
    {
        Console.CursorVisible = false;
        Console.Title = ("Таск-трекер");


        IAppScreen screen = mainMenuScreen;
        //Здесь основная идея, что сама работа приложения - это
        //переключения между экранами в бесконечном цикле +
        //выполнение какого-то функционала: создание, изменение и пр.(еще не реализовано)
        while (true)
        {
            screen.Print();
            screen = screen.NextScreen(MenuManager.ChooseItem(screen.MenuItems));
        }
    }

    //Этот метод может понадоибиться, чтобы 
    //создать таски через приложение, пока не доделаю функционал в UI 
    public static void StartChat(ITaskRepository tasksRepository)
    {
        while (true)
        {
            Console.Write("Введи название задачи для добавления: ");
            var taskName = Console.ReadLine();

            if (taskName == null)
            {
                Console.Clear();
                continue;
            }

            if (taskName.Equals(""))
            {
                tasksRepository.CreateTask(new Task());
            }
            else
            {
                var task = new Task(taskName);
                tasksRepository.CreateTask(task);
            }

            Console.WriteLine("Надо добавить еще?");
            var answer = Console.ReadLine();

            if (answer.Contains("да", StringComparison.OrdinalIgnoreCase))
                continue;
            else break;
        }

        var allTasks = tasksRepository.GetAllTasks();

        foreach (var task in allTasks)
        {
            Console.WriteLine($"Название: {task.Name}");
            Console.WriteLine($"Id: {task.Id}");
            Console.WriteLine($"Дата создания: {task.CreatedAt}");
            Console.WriteLine($"Дата дедлайна: {task.DueDate}");
            Console.WriteLine($"Активная задача?: {(task.IsActive ? "Да" : "Нет")}");
            Console.WriteLine($"Статус: {task.TaskStatus}");
            Console.WriteLine();
        }
    }
}