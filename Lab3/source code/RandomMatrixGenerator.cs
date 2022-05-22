using System;

namespace ASD_Lab1
{
    public class RandomMatrixGenerator
    {
        public static int[,] MakeRandomMatrix()
        {
            Console.Write("Enter matrix size: ");
            int size = int.Parse(Console.ReadLine());

            int[,] matrix = new int[size, size];
            Random rand = new Random();
            
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = rand.Next() % 2;
                }
            }

            return matrix;
        }
    }
}