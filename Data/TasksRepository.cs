using Core;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Task = Core.Task;

namespace Data
{
    public class TasksRepository : ITaskRepository
    {
        private readonly TaskTrackerDbContext _context;
        public TasksRepository(TaskTrackerDbContext dbContext)
        {
            _context = dbContext;
        }

        public void CreateTask(Task task)
        {
            _context.Tasks.Add(TaskToEntity(task));
            _context.SaveChanges();
        }

        public void DeleteTask(Task task)
        {
            _context.Tasks.Remove(TaskToEntity(task));
            _context.SaveChanges();
        }

        public IReadOnlyList<Task> GetAllTasks()
        {
            return _context.Tasks
                .AsNoTracking()
                .Include(t => t.Comments)
                .Select(t => EntityToTask(t))
                .ToList();
        }

        public void UpdateTask(string taskId, Task actualTask)
        {
            var entity = _context.Tasks.FirstOrDefault(t => t.Id.ToString() == actualTask.Id)
                ?? throw new ArgumentException();

            entity.Status = actualTask.Status;
            entity.DueDate = actualTask.DueDate;

            var newComments = actualTask.Comments
                .Where(c => !_context.Comments
                    .Select(c => c.Id.ToString()).Contains(c.Id));
                                                                    //Ищу комменты, которых нет в базе
            foreach (var comment in newComments)                    //и отдельно заношу каждый коммент
                AddComment(entity, comment);                        //в базу

            _context.Tasks.Update(entity);
            _context.SaveChanges();
        }

        private void AddComment(TaskEntity task, TaskComment comment)
        {
            _context.Comments.Add(CommentToEntity(comment, task));
            _context.SaveChanges();
        }

        private static TaskEntity TaskToEntity(Task task)
        {
            var entity = new TaskEntity()
            {
                Id = Guid.Parse(task.Id),
                Name = task.Name,
                CreatedAt = task.CreatedAt,
                DueDate = task.DueDate,
                Status = task.Status
            };

            entity.Comments = task.Comments
                .Select(c => CommentToEntity(c, entity))
                .ToList();

            return entity;
        }

        private static CommentEntity CommentToEntity(TaskComment comment, TaskEntity task)
        {
            return new CommentEntity()
            {
                Id = Guid.Parse(comment.Id),
                Text = comment.Text,
                CreatedAt = comment.CreatedAt,
                TaskEntity = task,
                TaskId = task.Id
            };
        }

        private static Task EntityToTask(TaskEntity entity)
        {
            return new Task(
                entity.Id.ToString(),
                entity.Name,
                entity.CreatedAt,
                entity.DueDate,
                entity.Status,
                entity.Comments.Select(c => EntityToComment(c)).ToList()
                );
        }

        private static TaskComment EntityToComment(CommentEntity comment)
        {
            return new TaskComment(
                comment.Id.ToString(),
                comment.CreatedAt,
                comment.Text
                );
        }
    }
}
