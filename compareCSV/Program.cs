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
            String[] csvPaths = Directory.GetFiles(thisFolder, "*.csv", System.IO.SearchOption.TopDirectoryOnly);
            List<string[]> csvFiles = new List<string[]>();
            for(var i = 0; i < csvPaths.Length; i++) {
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
        }
    }
}
