namespace Application_v2
{
    public class MenuActions
    {
        public string? Title { get; set; }
        public Func<IScreen> Action { get; set; }

        public MenuActions(string? title, Func<IScreen> action)
        {
            Title = title;
            Action = action;
        }
    }
}
