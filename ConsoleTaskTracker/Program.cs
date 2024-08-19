using System.Text.Json;

namespace ConsoleTaskTracker;

public class Program
{
    public static void Main()
    {
        var repo = new Repository("C:\\Users\\nikit\\source\\repos\\TheYoshi2738\\ConsoleTaskTrackerProject\\repo.json");
        StartChat(repo);
    }

    public static void StartChat(ITaskRepository repo)
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
                repo.Save(new Task());
            }
            else
            {
                var task = new Task(taskName);
                repo.Save(task);
            }

            Console.WriteLine("Надо добавить еще?");
            var answer = Console.ReadLine();

            if (answer.Contains("да", StringComparison.OrdinalIgnoreCase))
                continue;
            else break;
        }

        var allTasks = repo.GetAll();

        foreach (var task in allTasks)
        {
            Console.WriteLine($"Название: {task.Name}");
            Console.WriteLine($"Id: {task.Id}");
            Console.WriteLine($"Дата создания: {task.CreatedAt}");
            Console.WriteLine($"Дата дедлайна: {task.DueDate}");
            Console.WriteLine($"Активная задача?: {(task.IsActive ? "Да" : "Нет")}");
        }
    }
}