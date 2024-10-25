using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        public List<string>? ScreenBody { get; }
        public List<string?> MenuItemsTitles { get; }
        public ScreenPrinter(IScreen screen)
        {
            ScreenTitle = screen.Title;
            ScreenBody = screen.ScreenBodyLines;

            MenuItemsTitles = new List<string?>();
            foreach (var item in screen.Actions)
            {
                MenuItemsTitles.Add(item.Title);
            }
        }
        public void PrintAppTitle() => Console.Write(_appTitleLeftPadding + _appTitle);
        public void PrintScreenTitle() => Console.Write(_screenTtileTopPadding + _screenTtileLeftPadding + ScreenTitle);
        public void PrintScreenBody()
        {
            if (ScreenBody == null)
                return;

            Console.Write(_screenBodyTopPadding);
            foreach (var line in ScreenBody)
            {
                Console.WriteLine(_screenBodyLeftPadding + line);
            }
        }
        public void PrintScreenMenuItems(int currentMenuItem)
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

    }
}
