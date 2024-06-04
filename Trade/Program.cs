using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace Trade
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainPro mainpro = new MainPro();
            mainpro.runProcess();
            Environment.Exit(0);
        }
    }
}
