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

        [TestCase(Core.TaskStatus.Backlog, Core.TaskStatus.InProgress, Core.TaskStatus.InProgress)]
        [TestCase(Core.TaskStatus.InProgress, Core.TaskStatus.Backlog, Core.TaskStatus.Backlog)]
        [TestCase(Core.TaskStatus.InProgress, Core.TaskStatus.Done, Core.TaskStatus.Done)]
        [TestCase(Core.TaskStatus.Backlog, Core.TaskStatus.Trashed, Core.TaskStatus.Trashed)]
        [TestCase(Core.TaskStatus.InProgress, Core.TaskStatus.Trashed, Core.TaskStatus.Trashed)]
        public void TaskSuccessStatusChange(Core.TaskStatus currentStatus, Core.TaskStatus statusToChange, Core.TaskStatus expected)
        {
            var task = new Task();

            task.ChangeStatus(currentStatus);
            task.ChangeStatus(statusToChange);

            var actual = task.Status;
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(Core.TaskStatus.Backlog, Core.TaskStatus.Done, "Задачу нельзя перевести в Done из Backlog")]
        [TestCase(Core.TaskStatus.Done, Core.TaskStatus.Backlog, "Невозможно изменить статус у неактивной задачи")]
        [TestCase(Core.TaskStatus.Done, Core.TaskStatus.InProgress, "Невозможно изменить статус у неактивной задачи")]
        [TestCase(Core.TaskStatus.Done, Core.TaskStatus.Trashed, "Невозможно изменить статус у неактивной задачи")]
        [TestCase(Core.TaskStatus.Trashed, Core.TaskStatus.Backlog, "Невозможно изменить статус у неактивной задачи")]
        [TestCase(Core.TaskStatus.Trashed, Core.TaskStatus.InProgress, "Невозможно изменить статус у неактивной задачи")]
        [TestCase(Core.TaskStatus.Trashed, Core.TaskStatus.Done, "Невозможно изменить статус у неактивной задачи")]
        public void TaskFailedStatusChange(Core.TaskStatus currentStatus, Core.TaskStatus statusToChange, string expectedMessage)
        {
            var task = new Task();

            if (currentStatus.Equals(Core.TaskStatus.Done))
            {
                task.ChangeStatus(Core.TaskStatus.InProgress);
            }

            task.ChangeStatus(currentStatus);

            var exception = Assert.Throws<InvalidOperationException>(() => task.ChangeStatus(statusToChange));

            Assert.That(exception.Message, Does.Contain(expectedMessage));
        }
    }
}