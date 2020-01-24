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
            string testString1 = @"C:\Users\G1VECJM\Downloads\temp\SYS-060\Simulation\";
            string testString2 = @"C:\Users\G1VECJM\Desktop\Data\";
            string exit;
            do
            {
                DirectorySearch searchObject = new DirectorySearch();
                Console.WriteLine("Pfad eingeben: ");
                string locationString = Console.ReadLine();

                searchObject.setLocation(locationString);

                Console.WriteLine("Zu suchendes Signal(Schreibweise muss 1:1 übereinstimmen) eingeben: ");
                string words = Console.ReadLine();

                

              

                

                Console.WriteLine(DateTime.Now);

                //searchObject.searchForString("ESP_v_Signal");


                foreach (string word in words.Split(null))
                {
                    searchObject.searchForString(word);

                }



                Console.WriteLine(DateTime.Now);
                Console.WriteLine("Zum Beenden 'E' eingeben\n\rEnter zum Starten einer neuen Suche");
                exit = Console.ReadLine();
                exit = exit.ToLower();

                locationString = "";
                words = "";
            } while (!exit.Equals("e"));

           
        }
    }
}
