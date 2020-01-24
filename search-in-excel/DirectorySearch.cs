using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OfficeOpenXml;
using System.Collections;

namespace search_in_excel
{
    public class DirectorySearch
    {

        const int ZEILEN = 1048576/100;
        const int SPALTEN = 16384/100;

        string location;
        List<string> FileNames = new List<string>();
       // List<DirectorySearch> directorys = new List<DirectorySearch>();
        List<KeyValuePair<string, List<string>>> directoryListWithFiles = new List<KeyValuePair<string, List<string>>>();

        

        List<List<string>> retObj = new List<List<string>>();

        /// <summary>
        /// {
        ///     pfad: [
        ///         worksheet,
        ///         signalname,
        ///         ...
        ///     ],
        ///     
        /// }
        /// </summary>

        // neuer Hashtable für jedes Worksheet
        List<string> data = new List<string>();



       
        public DirectorySearch()
        {

        }
        public DirectorySearch(string location)
        {
            this.location = location;
        }

        public void setLocation(string location)
        {
            this.location = location;
        }

        public List<List<string>> getListObjects()
        {

            return retObj;
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
            //cellObj = new KeyValuePair<string, string>("worksheet", worksheet.ToString());
            //rowObj.Add(cellObj);
            //retObj.Add(rowObj);
            string retString = $"Signal in Zeile {row} gefunden: \n\r\n\r";
            for (var i = 1; i < 100; i++)
            {
                var lineValue = worksheet.Cells[row, i].Value;

                if (lineValue != null)
                {

                    if (!lineValue.Equals(""))
                    {
                        
                        string tabs = (worksheet.Cells[2, i].Value == null || worksheet.Cells[2, i].Value.ToString().Length < 7) ? "\t\t" : "";
                        string colString = $"{worksheet.Cells[2, i].Value} - \t\t{tabs}Wert: {lineValue}\n\r ";

                        if(worksheet.Cells[2, i].Value != null && worksheet.Cells[2, i].Value.ToString() == "Signal")
                        {
                            List<string> foundValue = new List<string>();
                            foundValue.Add(worksheet.ToString());
                            foundValue.Add(lineValue.ToString());
                            this.retObj.Add(foundValue);
                        }

                        retString = String.Concat(retString, colString);
                    }                
                }
            }
            return retString;
        }


        public string searchForString(string word)
        {
            List<string> dirList = createDirectoryList(this.location);

            List<KeyValuePair<string, List<string>>> variable = createFolderObjectList(dirList);

            foreach (KeyValuePair<string, List<string>> t in variable)
            {
                foreach (string s in t.Value)
                {
                    FileInfo fileInfo = new FileInfo(t.Key + s);

                    

                    using (var p = new ExcelPackage(fileInfo))
                    {

                        foreach(ExcelWorksheet worksheet in p.Workbook.Worksheets)
                        {
                            
                            for (var z = 1; z < ZEILEN; z++)
                            {

                                for (var sp = 1; sp < SPALTEN; sp++)
                                {
                                    if (worksheet.Cells[z, 1].Value == null) break;

                                    var cellValue = worksheet.Cells[z, sp].Value;

                                    if (cellValue != null)
                                    {
                                        cellValue = cellValue.ToString();

                                        try
                                        {
                                            if ((String)cellValue == word)
                                            {
                                                Console.WriteLine($"Im File gefunden: \n\r{t.Key + s}\n\r");
                                                Console.WriteLine("In Worksheet " + worksheet.ToString() + "gefundene Werte:");
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
                    
                    
                    //this.data.Clear();
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
