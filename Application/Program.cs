﻿using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleTaskTracker;

public class Program
{
    public static void Main()
    {
        //попытка держать все такси в одном месте и искать путь до ДБ динамично
        var path = Assembly.GetExecutingAssembly().Location;
        var regex = new Regex(".*ConsoleTaskTrackerProject\\b");
        path = Convert.ToString(regex.Match(path));
        var repo = new Repository(path + "\\TaskRepository\\repo.json");

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
            Console.WriteLine($"Статус: {task.TaskStatus}");
            Console.WriteLine();
        }
    }
}