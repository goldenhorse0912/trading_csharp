using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trade
{
    internal class Historical
    {
        public void runProcess()
        {
            init();
        }
        public static void init()
        {
            main(null);
        }

        public static async void main(String[] args)
        {
            string[] options = { "-Daily", "-Intraday", "-Exit" };
            int selectedIndex = 0;

            ConsoleKeyInfo keyInfo;
            do
            {
                Console.Clear();
                Console.WriteLine("Select a Type...");

                Console.WriteLine("Stop : Backspace double click \n");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == options.Length - 1)
                    {
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (i == selectedIndex)
                    {
                        Console.BackgroundColor = ConsoleColor.Blue;
                        Console.ForegroundColor = ConsoleColor.White;
                    }

                    Console.WriteLine(options[i]);

                    Console.ResetColor();
                }

                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex = Math.Max(0, selectedIndex - 1);
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow)
                {
                    selectedIndex = Math.Min(options.Length - 1, selectedIndex + 1);
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    if (selectedIndex == 0)
                    {
                        Daily daily = null;
                        daily = new Daily();
                        await daily.runAsync();
                    }
                    else if (selectedIndex == 1)
                    {
                        Intraday intraday = null;
                        intraday = new Intraday();
                        await intraday.runAsync();
                    }
                    else if (selectedIndex == 2)
                    {
                        break;
                    }
                }
            } while (keyInfo.Key != ConsoleKey.Escape || selectedIndex == options.Length - 1);
        }
    }
}
