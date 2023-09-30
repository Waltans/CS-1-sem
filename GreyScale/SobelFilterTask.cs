using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var result = new double[width, height];
            var sy = TransposeMatrix(sx);
            
            for (int x = 1; x < width - 1; x++)
                for (int y = 1; y < height - 1; y++)
                {
                    // Вместо этого кода должно быть поэлементное умножение матриц sx и полученной транспонированием из неё sy на окрестность точки (x, y)
                    // Такая операция ещё называется свёрткой (Сonvolution)
                    
                }
            return result;
        }
        
        static double[,] TransposeMatrix(double[,] matrix)
        {
            int N = matrix.GetUpperBound(0) + 1;
            var newMatrix = new double[N, N];

            for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
                newMatrix[i, j] = matrix[j, i];
            
            return newMatrix;
        }
        
        public static double[,] MultiplicationMatrix(double[,] firstMatrix, double[,] secondMatrix, int width, int height)
        {
            var resultMatrix = new double[width, height];

            for (int i = 0; i < firstMatrix.GetLength(0); i++)
                for (int j = 0; j < secondMatrix.GetLength(1); j++)
                    for (int k = 0; k < secondMatrix.GetLength(0); k++)
                        resultMatrix[i, j] += firstMatrix[i, k] * secondMatrix[k, j];

            return resultMatrix;
        }
    }
}

/* using System;

namespace Recognizer
{
    internal static class SobelFilterTask
    {
        public static double[,] SobelFilter(double[,] g, double[,] sx)
        {
            var width = g.GetLength(0);
            var height = g.GetLength(1);
            var sy = TransposeMatrix(sx);
            var result = MultiplicationMatrix(sx,sy,width,height);

            return result;
        }
        
        
        
        public static double[,] MultiplicationMatrix(double[,] firstMatrix, double[,] secondMatrix, int width, int height)
        {
            var resultMatrix = new double[width, height];

            for (int i = 0; i < firstMatrix.GetLength(0); i++)
            for (int j = 0; j < secondMatrix.GetLength(1); j++)
            for (int k = 0; k < secondMatrix.GetLength(0); k++)
            {
                resultMatrix[i, j] += firstMatrix[i, k] * secondMatrix[k, j];
            }

            return resultMatrix;
        }
    }
}
*/ 