using System.Reflection.Emit;

namespace Application_v2
{
    public class MenuAction
    {
        public string? Title { get; set; }
        public Func<IScreen?> Action { get; set; }

        public MenuAction(string? title, Func<IScreen?> action)
        {
            Title = title;
            Action = action;
        }
    }
    public class MenuActionInput : MenuAction
    {
        private readonly string? TitleName;
        public string? Input { get; private set; }
        public MenuActionInput(string? title, Func<IScreen?> action, string? input = null)
            : base(title, action)
        {
            TitleName = title;
            if (input != null)
            {
                Title = title + input;
            }
        }

        public void UpdateTitileWithInput()
        {
            var input = GetInputFromScreen();
            Title = TitleName + input;
        }

        private string GetInputFromScreen()
        {
            var inputLinePadding = 2;
            var topPosition = Console.CursorTop + inputLinePadding;
            var leftPosition = Console.CursorLeft;
            string? input = null;

            while (string.IsNullOrEmpty(input))
            {
                if (input != null)
                {
                    Console.Beep();
                }

                Console.SetCursorPosition(leftPosition, topPosition);
                Console.Write("Ввод: ");
                input = Console.ReadLine();
            }

            Input = input;
            return input; 
        }
    }
}
