
using Core;

namespace Application_v2
{
    public class TaskCommentAddScreen : IScreenInput
    {
        public IReadOnlyList<MenuActionInput> Actions { get; set; }

        public string? Title { get; } = "Введите комментарий";

        public AppContext AppContext { get; set; }

        IReadOnlyList<MenuAction> IScreen.Actions => Actions;

        public TaskCommentAddScreen(AppContext appContext, Task task)
        {
            AppContext = appContext;
            var actions = new List<MenuActionInput>();
            actions.Add(new MenuActionInput("Комментарий: ", () =>
            {
                Actions[0].UpdateTitileWithInput();
                return this;
            }));
            actions.Add(new MenuActionInput("Добавить комменатрий и вернуться к задаче", () =>
            {
                task.AddComment(Actions[0].Input);
                return AppContext.PopPreviousScreen();
            }));

            Actions = actions;
        }

    }
}
