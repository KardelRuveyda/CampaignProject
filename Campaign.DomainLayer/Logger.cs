using System;
using System.Collections.Generic;
using System.Text;

namespace Campaign.DomainLayer
{
    public static class Logger
    {
        public static void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
