using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;


namespace search_in_excel
{
    public class DirectorySearch
    {

        const int ZEILEN = 1048576/100;
        const int SPALTEN = 16384/100;

        string location;
        List<string> FileNames = new List<string>();
        List<DirectorySearch> directorys = new List<DirectorySearch>();
        List<KeyValuePair<string, List<string>>> directoryListWithFiles = new List<KeyValuePair<string, List<string>>>();

        public DirectorySearch(string location)
        {
            this.location = location;
        }
        public void showFilesInFolder(string location, Dictionary<string, List<string>> data)
        {
            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(this.location);

            Console.WriteLine($"Pfad: {this.location}");

            foreach (System.IO.FileInfo f in ParentDirectory.GetFiles())
            {
                Console.WriteLine("Datei: " + f.Name);
                this.FileNames.Add(f.Name);
            }

            directoryListWithFiles.Add(new KeyValuePair<string, List<string>>(this.location, this.FileNames));

            foreach (System.IO.DirectoryInfo d in ParentDirectory.GetDirectories())
            {
                Console.WriteLine("Folder: " + d.Name);

                string directoryString = this.location + d.Name + @"\";

                DirectorySearch dir = new DirectorySearch(directoryString);

                //dir.showFilesInFolder();

                directorys.Add(dir);
            }
        }

        public List<string> FilterExcelFiles(List<string> fileList)
        {
            List<string> retList = new List<string>();
            foreach(string file in fileList)
            {
                if(file.EndsWith("xlsx"))
                {
                    retList.Add(file);
                }
            }
            return retList;
        }

        public List<string> createDirectoryList(string startLocation)
        {
            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(startLocation);

            List<string> retList = new List<string>();

            try
            {
                foreach (System.IO.DirectoryInfo d in ParentDirectory.GetDirectories())
                {
                    string directoryString = startLocation + d.Name + @"\";

                    retList.Add(directoryString);

                    List<string> listToAdd = createDirectoryList(directoryString);

                    foreach (string l in listToAdd)
                    {
                        retList.Add(l);
                    }
                }
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
            
           
            return retList;
        }

        public List<string> createFileList(string location)
        {
            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(location);

            List<string> files = new List<string>();

            foreach (System.IO.FileInfo f in ParentDirectory.GetFiles())
            {
                files.Add(f.Name);
            }

            files = FilterExcelFiles(files);

            return files;
        }

        public KeyValuePair<string, List<string>> createFolderObject(string location)
        {
            List<string> files = createFileList(location);

            KeyValuePair<string, List<string>> FolderStructure = new KeyValuePair<string, List<string>>(location, files);

            return FolderStructure;
        }

        public List<KeyValuePair<string, List<string>>> createFolderObjectList(List<string> directoryList)
        {
            List<KeyValuePair<string, List <string>>> objectList = new List<KeyValuePair<string, List<string>>>();
            foreach(string path in directoryList)
            {
                objectList.Add(createFolderObject(path));
            }

            return objectList;
        }

        public string getRowOfWorksheet(ExcelWorksheet worksheet, int row)
        {
            string retString = $"In Reihe: {row} gefundene Werte...\n\r";
            for (var i = 1; i < 100; i++)
            {
                var lineValue = worksheet.Cells[row, i].Value;

                if (lineValue != null)
                {

                    if (!lineValue.Equals(""))
                    {
                        string colString = $"{worksheet.Cells[2, i].Value} - Wert: {lineValue}\n\r ";

                        retString = String.Concat(retString, colString);
                    }                
                }
            }
            return retString;
        }


        public string searchForString(string word)
        {

            string testString1 = @"C:\Users\G1VECJM\Downloads\temp\SYS-060\Simulation\";
            string testString2 = @"C:\Users\G1VECJM\Desktop\Data\";

            //DirectorySearch test = new DirectorySearch(testString1);

            List<string> dirList = createDirectoryList(testString1);

            List<KeyValuePair<string, List<string>>> variable = createFolderObjectList(dirList);

            foreach (KeyValuePair<string, List<string>> t in variable)
            {
                Console.WriteLine(t.Key);
                foreach (string s in t.Value)
                {
                    //Console.WriteLine(s);
                    FileInfo fileInfo = new FileInfo(t.Key + s);

                    //Console.WriteLine("Key + Value: " + t.Key + s);

                    using (var p = new ExcelPackage(fileInfo))
                    {

                        foreach(ExcelWorksheet worksheet in p.Workbook.Worksheets)
                        {
                            
                            for (var z = 1; z < ZEILEN; z++)
                            {
                                for (var sp = 1; sp < SPALTEN; sp++)
                                {
                                    var cellValue = worksheet.Cells[z, sp].Value;

                                    if(cellValue != null)
                                    {
                                        cellValue = cellValue.ToString();

                                        try
                                        {
                                            if ((String)cellValue == word)
                                            {
                                                Console.WriteLine("In Worksheet "+ worksheet.ToString() + "gefundene Werte:");
                                                Console.WriteLine();
                                                Console.WriteLine(getRowOfWorksheet(worksheet, z));
                                                Console.WriteLine();

                                                break;
                                            }
                                        }
                                        catch (InvalidCastException e)
                                        {
                                            Console.WriteLine(e.Message);
                                        }


                                    }
                                   
                                    
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }
        public void searchString(string word)
        {


            FileInfo newFile = new FileInfo(@"C:\Users\G1VECJM\Desktop\Data\test.xlsx");

            Console.WriteLine("Input: ");
            String input = Console.ReadLine();
            Console.WriteLine(input);

            using (var p = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = p.Workbook.Worksheets[1];

                Console.WriteLine(worksheet.Row(1));

                for (var z = 1; z < ZEILEN; z++)
                {
                    for (var s = 1; s < SPALTEN; s++)
                    {
                        var cellValue = worksheet.Cells[z, s].Value;
                        if ((String)cellValue == word)
                        {
                            for (var i = 1; i < 100; i++)
                            {
                                var lineValue = worksheet.Cells[z, i].Value;
                                if (lineValue != null)
                                {
                                    Console.WriteLine(lineValue);
                                }

                            }
                            Console.WriteLine($"{cellValue} gefunden in Zeile {z} und Spalte {s}");
                            break;
                        }
                    }
                }
            }
        }

    }
}
