using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace LengthApp
{
    class Program
    {
        static string currentDir = Environment.CurrentDirectory;

        static string[] strUnit = { "mile", "yard", "inch", "foot", "fath", "furlong" };
        static string[] strUnits = { "miles", "yards", "inches", "feet", "faths", "furlong" };

        static Dictionary<string, string> dicRule = new Dictionary<string, string>();       // Rule list
        static List<string> lstArithmetic = new List<string>(); // Arithmetic list
        static List<decimal> lstResult = new List<decimal>();   // Result list

        /// <summary>
        /// Load and analysis the inout file
        /// </summary>
        /// <param name="filePath"></param>
        static void LoadAndAnalysisInputFile(string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line = null;
                bool isRule = true;
                while ((line = sr.ReadLine()) != null)
                {
                    line = line.Trim();

                    if ("".Equals(line))
                    {
                        line = sr.ReadLine();
                        isRule = false;
                    }

                    if (isRule)
                    {
                        string[] strs = line.Split(' ');
                        dicRule.Add(strs[1], " * " + strs[3]);
                    }
                    else
                    {
                        line = AnalysisArithmetic(line);
                        lstArithmetic.Add(line);
                    }
                }
            }
        }

        /// <summary>
        /// Analysis arithmetic, replace the plural to singular
        /// </summary>
        /// <param name="arithmetic"></param>
        /// <returns></returns>
        static string AnalysisArithmetic(string arithmetic)
        {
            for (int i = 0; i < strUnits.Count(); i++)
            {
                if (arithmetic.Contains(strUnits[i]))
                {
                    arithmetic = arithmetic.Replace(strUnits[i], strUnit[i]);
                }
            }

            return arithmetic;
        }

        /// <summary>
        /// Calculate the results and output to file
        /// </summary>
        static void CalculateResultsAndOutputFile()
        {
            string arithmetic = null;
            for (int i = 0; i < lstArithmetic.Count; i++)
            {
                arithmetic = lstArithmetic[i];
                for (int j = 0; j < strUnit.Count(); j++)
                {
                    if (arithmetic.Contains(strUnit[j]))
                    {
                        arithmetic = arithmetic.Replace(strUnit[j], dicRule[strUnit[j]]);
                    }
                }

                decimal result = (decimal)new DataTable().Compute(arithmetic, "");
                lstResult.Add(Math.Round(result, 2));
            }

            // Output
            using (StreamWriter sw = new StreamWriter(currentDir + @"\output.txt"))
            {
                sw.WriteLine("zxz414644665@163.com");
                sw.WriteLine("");
                foreach (var item in lstResult)
                {
                    sw.WriteLine(item + " m");
                }
            }
        }

        static void Main(string[] args)
        {
            string filePath = currentDir + @"\input.txt";
            LoadAndAnalysisInputFile(filePath);
            CalculateResultsAndOutputFile();

            Console.WriteLine(@"You can get the output.txt at path: " + currentDir + @"\output.txt");
            Console.ReadKey();
        }
    }
}
