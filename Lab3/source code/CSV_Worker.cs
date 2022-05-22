using System;
using System.IO;

namespace ASD_Lab1
{
    public class CSV_Worker
    {
        public int[,] readTableFromCSV(string path)
        {
            StreamReader sr = new StreamReader(path);
            string data = sr.ReadToEnd();
            sr.Close();

            int[,] resultTable = _makeTable(data);

            return resultTable;
        }

        private int[,] _makeTable(string data)
        {
            string[] rows = data.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);
            
            string[][] dataRaw = new string[rows.Length][];
                
            for (int i = 0; i < rows.Length; i++)
            {
                dataRaw[i] = rows[i].Split(";");
            }

            int[,] dataArr = new int[dataRaw.Length, dataRaw[0].Length];
            
            for (int i = 0; i < dataRaw.Length; i++)
            {
                for (int j = 0; j < dataRaw[i].Length; j++)
                {
                    dataArr[i, j] = Convert.ToInt32(dataRaw[i][j]);
                }
            }

            return dataArr;
        }
    }
}