using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocket4Net;
using WebSocket4Net.Command;

namespace Trade
{
    internal class MainPro
    {
        public static MainPro instance = null;

        public void runProcess(){
            init();
        }
        public static void init() {
            main(null);
        }

        public static void getInfoFromUser()
        {
            Console.Write("Please enter websocket address : ");
            string socketUrl = Console.ReadLine();
            Info.setSocketUrl(socketUrl);

            Console.WriteLine("\nPlease enter http address (without router) : ");
            string httpUrl = Console.ReadLine();
            Info.setHttpUrl(httpUrl);

            Console.Write("\nSymbol Input Example : XXX,XXX,XXX");
            Console.Write("\nPlease enter Symbol : ");
            Info.setSymbol(Console.ReadLine().Split(','));

            Console.Write("\nPlease enter your token : ");
            string token = Console.ReadLine();
            Info.setToken(token);

            Console.Write("\nPlease enter your client ID : ");
            string clientID = Console.ReadLine();
            Info.setClientID(clientID);

            Console.Write("\nPlease enter Instrument : ");
            string instrument = Console.ReadLine();
            Info.setInstrument(instrument);

            //CSVRead csv = new CSVRead();
        }

        public static void main(String[] args)
        {
            getInfoFromUser();

            string[] options = { "-Ticker", "-Quote", "-Market Depth", "-Historical", "-Exit" };
            int selectedIndex = 0;

            ConsoleKeyInfo keyInfo;
            do
            {
                Console.Clear();
                Console.WriteLine("Select a Feed...");

                Console.WriteLine("Stop : Backspace Click \n");

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
                    if(selectedIndex == 0)
                    {
                        Ticker ticker = null;
                        ticker = new Ticker();
                        ticker.getSocket();
                    }
                    else if(selectedIndex == 1)
                    {
                        Quote quote = null;
                        quote = new Quote();
                        quote.getSocket();
                    }
                    else if (selectedIndex == 2)
                    {
                        Depth quote = null;
                        quote = new Depth();
                        quote.getSocket();
                    }
                    else if (selectedIndex == 3)
                    {
                        Historical histo = null;
                        histo = new Historical();
                        histo.runProcess();
                    }
                    else if (selectedIndex == 4)
                    {
                        break;
                    }
                }
            } while (keyInfo.Key != ConsoleKey.Escape || selectedIndex == options.Length - 1);
        }

        public static MainPro getInstance()
        {
            if(instance == null) instance = new MainPro();
            return instance;
        }
    }
}
