using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using System.Globalization;

namespace Trade
{
    internal class CSVRead
    {
        public CSVRead()
        {

            //Console.Clear();
            //Console.WriteLine("Please enter value of Exchange Segment.");
            //Console.WriteLine("0 : IDX_I");
            //Console.WriteLine("1 : NSE_EQ");
            //Console.WriteLine("2 : NSE_FNO");
            //Console.WriteLine("3 : NSE_CURRENCY");
            //Console.WriteLine("4 : BSE_EQ");
            //Console.WriteLine("5 : MCX_COMM");
            //Console.WriteLine("7 : BSE_CURRENCY");
            //Console.WriteLine("8 : BSE_FNO");
            //int segIndex = int.Parse(Console.ReadLine());
            //Console.WriteLine("\nPlease enter Security ID : ");
            //string securityID = Console.ReadLine();
            //object[,] seg = { { segIndex, securityID } };
            //Info.setSegment(seg);
            // Replace 'your_file_path.csv' with the actual path to your CSV file
            string filePath = "./api-scrip-master.csv";

            // Replace 'search_value' with the value you want to search for
            Console.WriteLine("Please enter Trading Symbol.");
            string searchValue = Console.ReadLine();

            List<List<string>> matchingRows = SearchCsv(filePath, searchValue);

            //if (matchingRows.Count > 0)
            //{
            //    Console.WriteLine("Found matching row(s) in CSV:");
            //    foreach (var row in matchingRows)
            //    {
            //        foreach (var entry in row)
            //        {
            //            Console.WriteLine($"{entry.Key}: {entry.Value}");
            //        }
            //        Console.WriteLine();
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("No matching row found in CSV.");
            //}
        }

        static List<List<string>> SearchCsv(string filePath, string searchValue)
        {
            List<List<string>> matchingRows = new List<List<string>>();

            try
            {
                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    while (csv.Read())
                    {
                        var row = new List<string>();

                        string item = csv.Context.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }

            return matchingRows;
        }
    }
}
