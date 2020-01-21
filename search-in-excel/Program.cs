using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;
using System.IO;

namespace search_in_excel
{
    class Program
    {
        static void Main(string[] args)
        {

            ConsoleSpiner spin = new ConsoleSpiner();
            Console.Write("Working....");
            while (true)
            {
                spin.Turn();
            }

            string testString1 = @"C:\Users\G1VECJM\Downloads\temp\SYS-060\Simulation\";
            string testString2 = @"C:\Users\G1VECJM\Desktop\Data\";

            DirectorySearch test = new DirectorySearch(testString1);

            test.searchForString("ZV_DWA_Einfachhorn_Anf");


            Console.ReadLine();
        }
    }
}
