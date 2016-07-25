using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace compareCSV
{
    class Program
    {
        static void Main()
        {
            String thisFolder = Directory.GetCurrentDirectory();
            var fileNames = Directory.GetFiles(thisFolder, "*.csv", System.IO.SearchOption.TopDirectoryOnly).Select(f => Path.GetFileName(f));
            String[] fileNamesArray = fileNames.Cast<String>().ToArray();
            if (fileNamesArray.Contains("target.csv"))
            {
                List<string> target = new List<string>();
                List<string> source = new List<string>();
                List<string> report = new List<string>();
                List<string> reportReverse = new List<string>();
                var targetIndex = Array.IndexOf(fileNamesArray, "target.csv");
                String[] csvPaths = Directory.GetFiles(thisFolder, "*.csv", System.IO.SearchOption.TopDirectoryOnly);
                List<string[]> csvFiles = new List<string[]>();
                for (var i = 0; i < csvPaths.Length; i++)
                {
                    using (TextFieldParser parser = new TextFieldParser(@csvPaths[i]))
                    {
                        parser.TextFieldType = FieldType.Delimited;
                        parser.SetDelimiters(",");
                        var csvFields = new List<string>();
                        while (!parser.EndOfData)
                        {
                            csvFields.Add(parser.ReadLine());
                        }
                        csvFiles.Add(csvFields.ToArray());
                    }
                }
                /*  Merge diferent source files into one    */
                for(var i = 0; i < csvFiles.Count; i++)
                {
                    if(i != targetIndex)
                    {
                        foreach(var data in csvFiles[i])
                        {
                            source.Add(data);
                        }
                    }
                    else
                    {
                        foreach (var data in csvFiles[i])
                        {
                            target.Add(data);
                        }
                    }
                }

                /*  Optimize files (remove duplicates and sort) */
    
                target = target.Distinct().ToList();
                source = source.Distinct().ToList();

                for(var i = 0; i < target.Count; i++)
                {
                    if (target[i] == "")
                    {
                        target.Remove(target[i]);
                    }
                }
                for (var i = 0; i < source.Count; i++)
                {
                    if (source[i] == "")
                    {
                        source.Remove(source[i]);
                    }
                }

                foreach (var data in source)
                {
                    if (!target.Contains(data))
                    {
                        report.Add(data);
                    }
                }

                foreach(var data in target)
                {
                    if (!source.Contains(data))
                    {
                        reportReverse.Add(data);
                    }
                }

                /*  Write Report    */
                File.WriteAllLines(thisFolder + "\\" + "report.txt",report);
                File.WriteAllLines(thisFolder + "\\" + "target.txt", target);
                File.WriteAllLines(thisFolder + "\\" + "reportReverse.txt", reportReverse);
            }
            else
            {
                Console.WriteLine("No target.csv found");
                Console.ReadLine();
            }
            
        }
            
    }
}
