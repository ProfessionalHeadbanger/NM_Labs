// Систему уравнений задают шесть векторов: a, b, c, f, p, q
// a, b, c - векторы для элементов матрицы А, расположенных на нижней кодиагонали, на главной диагонали и на верхней диагонали
// p, q - векторы для элементов k-й строки и k-ого столбца матрицы А (1 < k < n)
// f - вектор правой части системы уравнений
// Вариант 7
// | | | | | | |*| | | |*|*|
// | | | | | | |*| | |*|*|*|
// | | | | | | |*| |*|*|*| |
// | | | | | | |*|*|*|*| | |
// | | | | | | |*|*|*| | | |
// | | | | | |*|*|*| | | | |
// |*|*|*|*|*|*|*|*|*|*|*|*|
// | | | |*|*|*|*| | | | | |
// | | |*|*|*| |*| | | | | |
// | |*|*|*| | |*| | | | | |
// |*|*|*| | | |*| | | | | |
// |*|*| | | | |*| | | | | |

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Matrix
{
    private int[]? vectorA;
    private int[]? vectorB;
    private int[]? vectorC;
    private int[]? vectorF;
    private int[]? vectorP;
    private int[]? vectorQ;
    private int[]? vectorX;
    private int matrixSize;
    private int numberK;

    public Matrix(int matrixSize, int numberK)
    {
        this.matrixSize = matrixSize;
        this.numberK = numberK;

        Random random = new Random();

        vectorA = new int[matrixSize];
        vectorB = new int[matrixSize];
        vectorC = new int[matrixSize];
        vectorF = new int[matrixSize];
        vectorP = new int[matrixSize];
        vectorQ = new int[matrixSize];
        vectorX = new int[matrixSize];

        for (int row = 0; row < matrixSize; row++)
        {
            for (int col = 0; col < matrixSize; col++)
            {
                if (col == numberK - 1)
                {
                    vectorQ[row] = random.Next() % 100;
                }
                if (row == 0)
                {
                    vectorA[row] = 0;
                    vectorB[row] = random.Next() % 100;
                    vectorC[row] = random.Next() % 100;
                }
                if ((row >= 1 && row <= numberK - 1 - 3) || (row >= numberK - 1 + 1 && row <= matrixSize - 1 - 1))
                {
                    vectorA[row] = random.Next() % 100;
                    vectorB[row] = random.Next() % 100;
                    vectorC[row] = random.Next() % 100;
                }
                if (row == matrixSize - 1)
                {
                    vectorA[row] = random.Next() % 100;
                    vectorB[row] = random.Next() % 100;
                    vectorC[row] = 0;
                }
                if (row >= numberK - 1 - 2 && row <= numberK - 1 - 1)
                {
                    vectorA[row] = random.Next() % 100;
                    vectorB[row] = random.Next() % 100;
                    vectorC[row] = random.Next() % 100;
                    if (row == numberK - 1 - 2)
                    {
                        vectorQ[row] = vectorC[row];
                    }
                    if (row == numberK - 1 - 1)
                    {
                        vectorQ[row] = vectorB[row];
                    }
                }
                if (row == numberK - 1)
                {
                    vectorP[col] = random.Next() % 100;
                    if (col == numberK - 1 - 2)
                    {
                        vectorC[numberK - 1] = vectorP[numberK - 1 - 2];
                    }
                    if (col == numberK - 1 - 1)
                    {
                        vectorB[numberK - 1] = vectorP[numberK - 1 - 1];
                    }
                    if (col == numberK - 1)
                    {
                        vectorA[numberK - 1] = vectorP[numberK - 1];
                        vectorQ[numberK - 1] = vectorP[numberK - 1];
                    }
                }
            }
            vectorF[row] = random.Next() % 100;
        }
    }

    public void PrintMatrix()
    {
        Console.WriteLine("Matrix Size: " + matrixSize);
        Console.WriteLine("Number K: " + numberK);

        int end_col = matrixSize - 1;
        for (int row = 0; row < matrixSize; row++)
        {
            for (int col = 0; col < matrixSize; col++)
            {
                if (col == numberK - 1)
                {
                    Console.Write(vectorQ[row] + " ");
                }
                else if (row == numberK - 1)
                {
                    Console.Write(vectorP[col] + " ");
                }
                else if (col == end_col - 1)
                {
                    Console.Write(vectorC[row] + " ");
                }
                else if (col == end_col)
                {
                    Console.Write(vectorB[row] + " ");
                }
                else if (col == end_col + 1)
                {
                    Console.Write(vectorA[row] + " ");
                }
                else
                {
                    Console.Write("0  ");
                }
            }
            end_col--;
            Console.WriteLine();
        }
    }
}
