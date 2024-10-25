using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class MenuManager
    {
        public static int ChooseItem(string[] menuItems)
        {
            int index = 0;
            bool isActive = true;

            Console.CursorTop -= menuItems.Length;
            Console.CursorLeft = 0;
            Console.Write('>');

            while (isActive)
            {
                switch (Console.ReadKey(false).Key)
                {
                    case (ConsoleKey.DownArrow):
                        {
                            if (index < menuItems.Length - 1)
                            {
                                index++;
                                Console.Write("\b \b");
                                Console.CursorTop++;
                                Console.Write('>');
                            }
                            break;
                        }

                    case (ConsoleKey.UpArrow):
                        {
                            if (index > 0)
                            {
                                index--;
                                Console.Write("\b \b");
                                Console.CursorTop--;
                                Console.Write('>');
                            }
                            break;
                        }

                    case (ConsoleKey.Enter):
                        {
                            isActive = false;
                            break;
                        }

                    default:
                        break;
                }
            }
            return index;
        }
    }
}
