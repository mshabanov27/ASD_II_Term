using System;

namespace ASD_Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileName = "adjacencyMatrix.csv";
            CSV_Worker tableReader = new CSV_Worker();

            int[,] table = tableReader.readTableFromCSV(fileName);
            PrintMatrix.Print(table);
            Graph eulerGraph = new Graph(table);
            eulerGraph.MakeEulerPath();
            Console.WriteLine(eulerGraph.EulerPath);

            int[,] randomMatrix = RandomMatrixGenerator.MakeRandomMatrix();
            PrintMatrix.Print(randomMatrix);
            Graph randomGraph = new Graph(randomMatrix);
            randomGraph.MakeEulerPath();
            Console.WriteLine(randomGraph.EulerPath);
        }
    }
}
