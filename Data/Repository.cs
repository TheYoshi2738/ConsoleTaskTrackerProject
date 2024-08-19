using System.Text;
using Newtonsoft.Json;

namespace ConsoleTaskTracker
{
    public class Repository : ITaskRepository
    {
        private FileInfo TaskRepoFile { get; }
        private bool IsEmpty
        {
            get
            {
                return String.IsNullOrEmpty(File.ReadAllText(TaskRepoFile.FullName, Encoding.UTF8));
            }
        }
        public Repository(string pathToRepo)
        {
            var repoFileInfo = new FileInfo(pathToRepo);
            if (!repoFileInfo.Exists)
            {
                repoFileInfo.Create().Close();
            }
            TaskRepoFile = repoFileInfo;
        }

        public List<Task> GetAll()
        {
            var tasks = File.ReadAllText(TaskRepoFile.FullName, Encoding.UTF8);
            return JsonConvert.DeserializeObject<List<Task>>(tasks);
        }

        public void Save(Task task)
        {
            List<Task> allTasks;
            if (IsEmpty)
            {
                allTasks = new List<Task>();
            }
            else
            {
                allTasks = GetAll();
            }
            allTasks.Add(task);
            var jsonTasks = JsonConvert.SerializeObject(allTasks);
            File.WriteAllText(TaskRepoFile.FullName, jsonTasks);
        }
    }
}
