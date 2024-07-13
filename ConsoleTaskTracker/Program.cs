using System.Text.Json;

namespace ConsoleTaskTracker;

public class Program
{
    public static void Main()
    {
        ProgramLogic();
    }


    public static void ProgramLogic()
    {
        var allTasks = new List<Task>();

        while (true)
        {
            Console.Write("Введи название задачи для добавления: ");
            var taskName = Console.ReadLine();

            if (taskName == null)
            {
                Console.Clear();
                continue;
            }

            allTasks.Add(new Task(taskName));

            Console.WriteLine("Надо добавить еще?");
            var answer = Console.ReadLine();

            if (answer.Contains("да", StringComparison.OrdinalIgnoreCase))
                continue;
            else break;
        }

        var allTasksJson = JsonSerializer.Serialize(allTasks);
        File.AppendAllText("./TasksDB.json", allTasksJson);

        foreach (var task in allTasks)
        {
            Console.WriteLine("Задача: {0} создана {1}. Статус: {2}", task.Name, task.CreatedAt.ToString(), task.TaskStatus.ToString());
            File.Delete("./TasksDB.json");
        }
    }
}