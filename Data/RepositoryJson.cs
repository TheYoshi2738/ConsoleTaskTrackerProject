using System.Text;
using Core;
using Task = Core.Task;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace Data
{
    public class RepositoryJson : ITaskRepository
    {
        private FileInfo TaskRepoFile { get; }
        private JsonSerializerSettings settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter>()
            {
                new TaskJsonConverter()
            } 
        };
       
        public RepositoryJson(FileInfo repositoryFile)
        {
            TaskRepoFile = repositoryFile;
        }

        public IReadOnlyList<Task> GetAllTasks()
        {
            if (string.IsNullOrEmpty(File.ReadAllText(TaskRepoFile.FullName, Encoding.UTF8)))
            {
                return new List<Task>();
            }

            var tasks = File.ReadAllText(TaskRepoFile.FullName, Encoding.UTF8);
            return JsonConvert.DeserializeObject<List<Task>>(tasks, settings);
        }

        public void CreateTask(Task task)
        {
            var allTasks = (List<Task>)GetAllTasks();
            allTasks.Add(task);
            var jsonTasks = JsonConvert.SerializeObject(allTasks);
            File.WriteAllText(TaskRepoFile.FullName, jsonTasks);
        }

        public void UpdateTask(Task task)
        {
            throw new NotImplementedException();
        }

        public void UpdateTask(string taskId, Task actualTask)
        {
            throw new NotImplementedException();
        }

        public void DeleteTask(Task task)
        {
            throw new NotImplementedException();
        }
    }

    public class TaskJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Task);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);

            var id = (string)jObject["Id"];
            var name = (string)jObject["Name"];
            var createdAt = DateOnly.FromDateTime((DateTime)jObject["CreatedAt"]);
            DateOnly? dueDate = DateOnly.TryParse((string)jObject["DueDate"], out DateOnly result) ? result : null;
                
            var status = (Core.TaskStatus)(int)jObject["TaskStatus"];

            var task = new Task(id, name, createdAt, dueDate, status, null);

            return task;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
