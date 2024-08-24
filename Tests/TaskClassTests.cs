using Core;
using Task = Core.Task;

namespace Tests
{
    public class TaskClassTests
    {
        [Test]
        public void TaskEmptyNameCreation()
        {
            var task = new Task();
            var expected = $"Задача без названия. Создана {DateTime.Now.ToString("d")} в {DateTime.Now.ToString("t")}";
            var actual = task.Name;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(Status.Backlog, Status.InProgress, Status.InProgress)]
        [TestCase(Status.InProgress, Status.Backlog, Status.Backlog)]
        [TestCase(Status.InProgress, Status.Done, Status.Done)]
        [TestCase(Status.Backlog, Status.Trashed, Status.Trashed)]
        [TestCase(Status.InProgress, Status.Trashed, Status.Trashed)]
        public void TaskSuccessStatusChange(Status currentStatus, Status statusToChange, Status expected)
        {
            var task = new Task();

            task.ChangeStatus(currentStatus);
            task.ChangeStatus(statusToChange);

            var actual = task.TaskStatus;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(Status.Backlog, Status.Done, "Задачу нельзя перевести в Done из Backlog")]
        [TestCase(Status.Done, Status.Backlog, "Невозможно изменить статус у неактивной задачи")]
        [TestCase(Status.Done, Status.InProgress, "Невозможно изменить статус у неактивной задачи")]
        [TestCase(Status.Done, Status.Trashed, "Невозможно изменить статус у неактивной задачи")]
        [TestCase(Status.Trashed, Status.Backlog, "Невозможно изменить статус у неактивной задачи")]
        [TestCase(Status.Trashed, Status.InProgress, "Невозможно изменить статус у неактивной задачи")]
        [TestCase(Status.Trashed, Status.Done, "Невозможно изменить статус у неактивной задачи")]
        public void TaskFailedStatusChange(Status currentStatus, Status statusToChange, string expectedMessage)
        {
            var task = new Task();

            if (currentStatus.Equals(Status.Done))
            {
                task.ChangeStatus(Status.InProgress);
            }

            task.ChangeStatus(currentStatus);

            var exception = Assert.Throws<InvalidOperationException>(() => task.ChangeStatus(statusToChange));

            Assert.That(exception.Message, Does.Contain(expectedMessage));
        }
    }
}