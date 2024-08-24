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
        //Console.CursorVisible = false;
        //Console.WriteLine("Консольный таск-трекер");
        //var menu = new MainMenuScreen("Главное меню",
        //[
        //    "Показать все задачи",
        //    "Создать задачу",
        //    "Выйти"
        //]);
        //menu.Print();

        //menu.NextScreen(MenuManager.ChooseItem(menu.MenuItems));

        //попытка держать все такси в одном месте и искать путь до ДБ динамично
        var path = Assembly.GetExecutingAssembly().Location;
        var regex = new Regex(".*ConsoleTaskTrackerProject\\b");
        path = Convert.ToString(regex.Match(path));
        var jsonRepository = new RepositoryJson(path + "\\TaskRepository\\jsonRepository.json");

        StartChat(jsonRepository);
    }

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