using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FLib
{
    public class DebugUtility
    {
        public static string DumpArrayToCsv<T>(string title, T[] array)
        {
            System.IO.StringWriter sb = new System.IO.StringWriter();
            sb.Write(title);
            foreach (var item in array)
            {
                sb.Write("," + item);
            }
            sb.WriteLine();
            return sb.ToString();
        }

        public static string DumpMatrixToCsv<T>(string title, T[] matrix, int column)
        {
            System.IO.StringWriter sb = new System.IO.StringWriter();
            sb.Write(title);

            for (int i = 0; i < matrix.Length; i++)
            {
                if (i % column == 0)
                {
                    sb.WriteLine();
                }
                sb.Write("," + matrix[i]);
            }

            sb.WriteLine();
            return sb.ToString();
        }

        public static void SaveAsCsv(string filePrefix, string dumpText, bool dumpCondition = true, bool openAfterSave = false)
        {
            if (false == dumpCondition)
            {
                return;
            }

            string filepath = System.IO.Path.Combine(@"C:\Temp", SaveFileName(filePrefix) + ".csv");
            System.IO.File.WriteAllText(filepath, dumpText);

            if (false == openAfterSave)
            {
                return;
            }
            
            System.Diagnostics.Process.Start(filepath);
        }

        public static void SaveAsTxt(string filePrefix, string dumpText, bool dumpCondition = true, bool openAfterSave = false)
        {
            if (false == dumpCondition)
            {
                return;
            }

            string filepath = System.IO.Path.Combine(@"C:\Temp", SaveFileName(filePrefix) + ".txt");
            System.IO.File.WriteAllText(filepath, dumpText);

            if (false == openAfterSave)
            {
                return;
            }
            
            System.Diagnostics.Process.Start(filepath);
        }

        public static string SaveFileName(string filePrefix)
        {
            var now = DateTime.Now;
            string filename = string.Format(
                "{0}{1}{2}{3}{4}{5}{6}",
                filePrefix,
                now.Year,
                now.Month,
                now.Day,
                now.Hour,
                now.Minute,
                now.Second);

            return filename;
        }
    }
}
