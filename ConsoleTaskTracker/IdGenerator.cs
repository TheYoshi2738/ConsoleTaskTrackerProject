using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTaskTracker
{
    public static class IdGenerator
    {
        static int id = 0;

        public static int GetNewId()
        {
            return ++id;
        }
    }
}
