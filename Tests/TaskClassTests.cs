using Task = ConsoleTaskTracker.Task;

namespace ConsoleTaskTrackerTests
{
    public class TaskClassTests
    {
        [Test]
        public void TaskEmptyNameCreation()
        {
            var task = new Task();
            var expected = "Задача без названия";
            var actual = task.Name;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void TaskDueDateInPastCreation()
        {
            string actual = string.Empty;
            try
            {
                var task = new Task("Задача", DateTime.Parse("2020-10-10"));
            }
            catch (ArgumentException e)
            {
                actual = e.Message;
            }

            var expected = "Дата DueDate не может быть в прошлом";
            Assert.That(actual, Does.Contain(expected));
        }

        [TestCase(Task.Status.Backlog, Task.Status.InProgress, Task.Status.InProgress)]
        [TestCase(Task.Status.InProgress, Task.Status.Backlog, Task.Status.Backlog)]
        [TestCase(Task.Status.InProgress, Task.Status.Done, Task.Status.Done)]
        [TestCase(Task.Status.Backlog, Task.Status.Trashed, Task.Status.Trashed)]
        [TestCase(Task.Status.InProgress, Task.Status.Trashed, Task.Status.Trashed)]
        public void TaskSuccessStatusChange(Task.Status currentStatus, Task.Status statusToChange, Task.Status expected)
        {
            var task = new Task();

            task.ChangeStatus(currentStatus);
            task.ChangeStatus(statusToChange);

            var actual = task.TaskStatus;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(Task.Status.Backlog, Task.Status.Done, "Задачу нельзя перевести в Done из Backlog")]
        [TestCase(Task.Status.Done, Task.Status.Backlog, "Невозможно изменить статус у неактивной задачи")]
        [TestCase(Task.Status.Done, Task.Status.InProgress, "Невозможно изменить статус у неактивной задачи")]
        [TestCase(Task.Status.Done, Task.Status.Trashed, "Невозможно изменить статус у неактивной задачи")]
        [TestCase(Task.Status.Trashed, Task.Status.Backlog, "Невозможно изменить статус у неактивной задачи")]
        [TestCase(Task.Status.Trashed, Task.Status.InProgress, "Невозможно изменить статус у неактивной задачи")]
        [TestCase(Task.Status.Trashed, Task.Status.Done, "Невозможно изменить статус у неактивной задачи")]
        public void TaskFailedStatusChange(Task.Status currentStatus, Task.Status statusToChange, string expected)
        {
            var task = new Task();
            string actual = string.Empty;

            if (currentStatus.Equals(Task.Status.Done))
                task.ChangeStatus(Task.Status.InProgress);

            task.ChangeStatus(currentStatus);

            try
            {
                task.ChangeStatus(statusToChange);
            }
            catch (ArgumentException e)
            {
                actual = e.Message;
            }

            Assert.That(actual, Does.Contain(expected));
        }
    }
}