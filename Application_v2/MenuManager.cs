using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application_v2
{
    public class MenuManager
    {
        public List<MenuActions> MenuItems;
        private ScreenPrinter printer;

        public MenuManager(IScreen screen, ScreenPrinter printer)
        {
            MenuItems = screen.Actions.ToList();
            this.printer = printer;
        }

        public IScreen ManageMenu()
        {
            var currentMenuItem = 0;
            var menuLength = MenuItems.Count;
            Action moveDown = () => currentMenuItem += currentMenuItem < menuLength - 1 ? 1 : 0;
            Action moveUp = () => currentMenuItem -= currentMenuItem > 0 ? 1 : 0;

            while (true)
            {
                UpdateScreen();

                var key = Console.ReadKey(false).Key;

                switch (key)
                {
                    case ConsoleKey.DownArrow:
                        moveDown();
                        break;

                    case ConsoleKey.UpArrow:
                        moveUp();
                        break;

                    case ConsoleKey.Enter:
                        return MenuItems[currentMenuItem].Action();
                }
            }
            
            //тут хз насколько красиво делать локальную функцию
            void UpdateScreen()
            {
                Console.Clear();
                printer.PrintAppTitle();
                printer.PrintScreenTitle();
                printer.PrintScreenBody();
                printer.PrintScreenMenuItems(currentMenuItem);
            }
        }

    }
}
