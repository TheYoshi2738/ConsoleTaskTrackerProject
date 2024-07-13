using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTaskTracker
{
    internal class DBLogic
    {
        private static string pathToDb;

        private static void GetPathToDb()
        {
            var parentDir = new DirectoryInfo("./").Parent;
            var filesInParent = parentDir.GetFiles();


            foreach (var file in filesInParent)
            {
                if (file.Name.Equals("TasksDB"))
                    return;
            }

        }
    }
}
