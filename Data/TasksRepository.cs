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

            //Ищу комменты, которых нет в базе
            //и отдельно заношу каждый коммент
            //в базу
            
            var newComments = actualTask.Comments
                .Where(commentCoreModel => !_context.Comments
                    .Select(commentEntity => commentEntity.Id.ToString()).Contains(commentCoreModel.Id));
            
            foreach (var comment in newComments)                    
                AddComment(entity, comment);                        

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
                CreatedAt = comment.CreatedAt.ToUniversalTime(),
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
                entity.Comments.Select(EntityToComment).ToList()
                );
        }

        private static TaskComment EntityToComment(CommentEntity comment)
        {
            return new TaskComment(
                comment.Id.ToString(),
                comment.CreatedAt.ToLocalTime(),
                comment.Text
                );
        }
    }
}
