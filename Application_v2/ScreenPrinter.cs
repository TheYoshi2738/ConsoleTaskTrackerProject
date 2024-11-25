namespace Application_v2
{
    public class ScreenPrinter
    {
        readonly string _appTitle = "Таск-трекер";
        readonly string _appTitleLeftPadding = "    ";
        readonly string _screenTtileTopPadding = "\n\n\n";
        readonly string _screenTtileLeftPadding = "  ";
        readonly string _screenBodyTopPadding = "\n\n";
        readonly string _screenBodyLeftPadding = "  ";
        readonly string _menuItemsTopPadding = "\n\n";
        readonly string _menuItemsLeftPadding = "  ";
        readonly string _lastMenuItemTopPadding = "\n";
        readonly string _menuItemsCursor = "> ";
        public string? ScreenTitle { get; }
        public IReadOnlyList<string>? ScreenBody { get; }
        public IReadOnlyList<string?> MenuItemsTitles { get; }
        public ScreenPrinter(IScreen screen)
        {
            ScreenTitle = screen.Title;

            if (screen is IBodyInfoScreen)
            {
                ScreenBody = ((IBodyInfoScreen)screen).ScreenBodyLines;
            }

            var menuItemsTitles = new List<string?>();
            foreach (var item in screen.Actions)
            {
                menuItemsTitles.Add(item.Title);
            }
            MenuItemsTitles = menuItemsTitles;
        }
        private void PrintAppTitle() => Console.Write(_appTitleLeftPadding + _appTitle);
        private void PrintScreenTitle() => Console.Write(_screenTtileTopPadding + _screenTtileLeftPadding + ScreenTitle);
        private void PrintScreenBody()
        {
            if (ScreenBody == null)
                return;

            Console.Write(_screenBodyTopPadding);
            foreach (var line in ScreenBody)
            {
                Console.WriteLine(_screenBodyLeftPadding + line);
            }
        }
        private void PrintScreenMenuItems(int currentMenuItem)
        {
            Console.Write(_menuItemsTopPadding);
            for (var i = 0; i < MenuItemsTitles.Count; i++)
            {
                if (i == MenuItemsTitles.Count - 1)
                {
                    Console.Write(_lastMenuItemTopPadding);
                }

                if (i == currentMenuItem)
                {
                    Console.WriteLine(_menuItemsCursor + MenuItemsTitles[i]);
                } else
                {
                    Console.WriteLine(_menuItemsLeftPadding + MenuItemsTitles[i]);
                }
            }
        }
        public void UpdateScreen(int currentMenuItem)
        {
            Console.Clear();
            PrintAppTitle();
            PrintScreenTitle();
            PrintScreenBody();
            PrintScreenMenuItems(currentMenuItem);
        }
    }
}
