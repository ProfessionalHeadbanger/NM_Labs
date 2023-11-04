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
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class Matrix
{
    private double[]? vectorA;
    private double[]? vectorB;
    private double[]? vectorC;
    private double[]? vectorF;
    private double[]? vectorP;
    private double[]? vectorQ;
    private double[]? vectorX;
    private int matrixSize;
    private int numberK;
    private double left;
    private double right;

    public Matrix(int matrixSize, int numberK, double left, double right)
    {
        this.matrixSize = matrixSize;
        this.numberK = numberK;

        Random random = new Random();

        vectorA = new double[matrixSize];
        vectorB = new double[matrixSize];
        vectorC = new double[matrixSize];
        vectorF = new double[matrixSize];
        vectorP = new double[matrixSize];
        vectorQ = new double[matrixSize];
        vectorX = new double[matrixSize];

        for (int row = 0; row < matrixSize; row++)
        {
            for (int col = 0; col < matrixSize; col++)
            {
                if (col == numberK - 1)
                {
                    vectorQ[row] = left + random.NextDouble()*(right - left);
                }
                if (row == 0)
                {
                    vectorA[row] = 0;
                    vectorB[row] = left + random.NextDouble()*(right-left);
                    vectorC[row] = left + random.NextDouble()*(right-left);
                }
                if ((row >= 1 && row <= numberK - 1 - 3) || (row >= numberK - 1 + 1 && row <= matrixSize - 1 - 1))
                {
                    vectorA[row] = left + random.NextDouble()*(right-left);
                    vectorB[row] = left + random.NextDouble()*(right-left);
                    vectorC[row] = left + random.NextDouble()*(right-left);
                }
                if (row == matrixSize - 1)
                {
                    vectorA[row] = left + random.NextDouble()*(right-left);
                    vectorB[row] = left + random.NextDouble()*(right-left);
                    vectorC[row] = 0;
                }
                if (row >= numberK - 1 - 2 && row <= numberK - 1 - 1)
                {
                    vectorA[row] = left + random.NextDouble()*(right-left);
                    vectorB[row] = left + random.NextDouble()*(right-left);
                    vectorC[row] = left + random.NextDouble()*(right-left);
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
                    vectorP[col] = left + random.NextDouble()*(right-left);
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
            vectorF[row] = left + random.NextDouble()*(right-left);
        }

        this.left = left;
        this.right = right;
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
                    Console.Write(String.Format("{0}  ", vectorQ[row]));
                }
                else if (row == numberK - 1)
                {
                    Console.Write(String.Format("{0}  ",vectorP[col]));
                }
                else if (col == end_col - 1)
                {
                    Console.Write(String.Format("{0}  ",vectorC[row]));
                }
                else if (col == end_col)
                {
                    Console.Write(String.Format("{0}  ",vectorB[row]));
                }
                else if (col == end_col + 1)
                {
                    Console.Write(String.Format("{0}  ",vectorA[row]));
                }
                else
                {
                    Console.Write(String.Format("{0, 15}  ", 0));
                }
            }
            end_col--;
            Console.WriteLine();
        }
    }

    public void printToFile(StreamWriter sw)
    {
		sw.WriteLine("Matrix Size: " + matrixSize);
		sw.WriteLine("Number K: " + numberK);

		int end_col = matrixSize - 1;
		for (int row = 0; row < matrixSize; row++)
		{
			for (int col = 0; col < matrixSize; col++)
			{
				if (col == numberK - 1)
				{
					sw.Write(String.Format("{0}  ", vectorQ[row]));
				}
				else if (row == numberK - 1)
				{
					sw.Write(String.Format("{0}  ", vectorP[col]));
				}
				else if (col == end_col - 1)
				{
					sw.Write(String.Format("{0}  ", vectorC[row]));
				}
				else if (col == end_col)
				{
					sw.Write(String.Format("{0}  ", vectorB[row]));
				}
				else if (col == end_col + 1)
				{
					sw.Write(String.Format("{0}  ", vectorA[row]));
				}
				else
				{
					sw.Write(String.Format("{0, 15}  ", 0));
				}
			}
			end_col--;
			sw.WriteLine();
		}
	}
}
