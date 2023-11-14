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
using System.ComponentModel.Design;

class Matrix
{
    private double[]? vectorA;
    private double[]? vectorB;
    private double[]? vectorC;
    private double[]? vectorF;
    private double[]? vectorP;
    private double[]? vectorQ;
    private double[]? vectorX;
    private double[]? vectorSolutions;
    private int matrixSize;
    private int numberK;

    public Matrix(int matrixSize, int numberK, int left, int right)
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
        vectorSolutions = new double[matrixSize];

        for (int i = 0; i < matrixSize; i++)
        {
            vectorX[i] = left + random.Next(left, right);
        }

        for (int row = 0; row < matrixSize; row++)
        {
            for (int col = 0; col < matrixSize; col++)
            {
                if (col == numberK - 1)
                {
                    vectorQ[row] = left + random.Next(left, right);
                }
                
                if (row == numberK - 1)
                {
                    vectorP[col] = left + random.Next(left, right);
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
        }

        for (int row = 0; row < matrixSize; row++)
        {
            if (row == 0)
            {
                vectorA[row] = 0;
                vectorB[row] = left + random.Next(left, right);
                vectorC[row] = left + random.Next(left, right);
                vectorF[row] = vectorQ[row] * vectorX[numberK - 1] + vectorC[row] * vectorX[matrixSize - 1 - row - 1] + vectorB[row] * vectorX[matrixSize - 1 - row];
            }
            if ((row >= 1 && row <= numberK - 1 - 3) || (row >= numberK - 1 + 1 && row <= matrixSize - 1 - 1))
            {
                vectorA[row] = left + random.Next(left, right);
                vectorB[row] = left + random.Next(left, right);
                vectorC[row] = left + random.Next(left, right);
                vectorF[row] = vectorQ[row] * vectorX[numberK - 1] + vectorC[row] * vectorX[matrixSize - 1 - row - 1] + vectorB[row] * vectorX[matrixSize - 1 - row] + vectorA[row] * vectorX[matrixSize - 1 - row + 1];
            }
            if (row == matrixSize - 1)
            {
                vectorA[row] = left + random.Next(left, right);
                vectorB[row] = left + random.Next(left, right);
                vectorC[row] = 0;
                vectorF[row] = vectorQ[row] * vectorX[numberK - 1] + vectorB[row] * vectorX[matrixSize - 1 - row] + vectorA[row] * vectorX[matrixSize - 1 - row + 1];
            }
            if (row >= numberK - 1 - 2 && row <= numberK - 1 - 1)
            {
                if (row == numberK - 1 - 2)
                {
                    vectorC[row] = vectorQ[row];
                    vectorA[row] = left + random.Next(left, right);
                    vectorB[row] = left + random.Next(left, right);
                }
                if (row == numberK - 1 - 1)
                {
                    vectorB[row] = vectorQ[row];
                    vectorA[row] = left + random.Next(left, right);
                    vectorC[row] = left + random.Next(left, right);
                }
                
                vectorF[row] = vectorC[row] * vectorX[matrixSize - 1 - row - 1] + vectorB[row] * vectorX[matrixSize - 1 - row] + vectorA[row] * vectorX[matrixSize - 1 - row + 1];
            }
            if (row == numberK - 1)
            {
                for (int col = 0; col < matrixSize; col++)
                {
                    vectorF[row] += vectorP[col] * vectorX[col];
                }
            }
        }
    }

    public void FindSolutions()
    {
        for (int row = 0; row < numberK - 1 - 2; row++)
        {
            vectorQ[row] /= vectorB[row];
            vectorC[row] /= vectorB[row];
            vectorF[row] /= vectorB[row];
            vectorB[row] /= vectorB[row];

            vectorQ[row + 1] -= vectorQ[row] * vectorA[row + 1];
            vectorB[row + 1] -= vectorC[row] * vectorA[row + 1];
            vectorF[row + 1] -= vectorF[row] * vectorA[row + 1];
            vectorA[row + 1] -= vectorB[row] * vectorA[row + 1];

            vectorP[numberK - 1] -= vectorQ[row] * vectorP[matrixSize - 1 - row];
            vectorQ[numberK - 1] = vectorA[numberK - 1] = vectorP[numberK - 1];
            vectorP[matrixSize - 1 - row - 1] -= vectorC[row] * vectorP[matrixSize - 1 - row];
            vectorF[numberK - 1] -= vectorF[row] * vectorP[matrixSize - 1 - row];
            vectorP[matrixSize - 1 - row] -= vectorB[row] * vectorP[matrixSize - 1 - row]; 
        }

        for (int row = matrixSize - 1; row > numberK - 1 + 1; row--)
        {
            vectorQ[row] /= vectorB[row];
            vectorA[row] /= vectorB[row];
            vectorF[row] /= vectorB[row];
            vectorB[row] /= vectorB[row];

            vectorQ[row - 1] -= vectorQ[row] * vectorC[row - 1];
            vectorB[row - 1] -= vectorA[row] * vectorC[row - 1];
            vectorF[row - 1] -= vectorF[row] * vectorC[row - 1];
            vectorC[row - 1] -= vectorB[row] * vectorC[row - 1];

            vectorP[numberK - 1] -= vectorQ[row] * vectorP[matrixSize - 1 - row];
            vectorQ[numberK - 1] = vectorA[numberK - 1] = vectorP[numberK - 1];
            vectorP[matrixSize - 1 - row + 1] -= vectorA[row] * vectorP[matrixSize - 1 - row];
            vectorF[numberK - 1] -= vectorF[row] * vectorP[matrixSize - 1 - row];
            vectorP[matrixSize - 1 - row] -= vectorB[row] * vectorP[matrixSize - 1 - row];
        }

        vectorQ[numberK - 1 - 2] /= vectorB[numberK - 1 - 2];
        vectorC[numberK - 1 - 2] = vectorQ[numberK - 1 - 2];
        vectorF[numberK - 1 - 2] /= vectorB[numberK - 1 - 2];
        vectorB[numberK - 1 - 2] /= vectorB[numberK - 1 - 2];

        vectorQ[numberK - 1 - 1] -= vectorQ[numberK - 1 - 2] * vectorA[numberK - 1 - 1];
        vectorB[numberK - 1 - 1] = vectorQ[numberK - 1 - 1];
        vectorF[numberK - 1 - 1] -= vectorF[numberK - 1 - 2] * vectorA[numberK - 1 - 1];
        vectorA[numberK - 1 - 1] -= vectorB[numberK - 1 - 2] * vectorA[numberK - 1 - 1];

        vectorP[numberK - 1] -= vectorQ[numberK - 1 - 2] * vectorP[numberK - 1 + 1];
        vectorQ[numberK - 1] = vectorA[numberK - 1] = vectorP[numberK - 1];
        vectorF[numberK - 1] -= vectorF[numberK - 1 - 2] * vectorP[numberK - 1 + 1];
        vectorP[numberK - 1 + 1] -= vectorB[numberK - 1 - 2] * vectorP[numberK - 1 + 1];

        vectorC[numberK - 1 - 1] /= vectorQ[numberK - 1 - 1];
        vectorF[numberK - 1 - 1] /= vectorQ[numberK - 1 - 1];
        vectorQ[numberK - 1 - 1] /= vectorQ[numberK - 1 - 1];
        vectorB[numberK - 1 - 1] = vectorQ[numberK - 1 - 1];

        vectorB[numberK - 1] -= vectorC[numberK - 1 - 1] * vectorP[numberK - 1];
        vectorF[numberK - 1] -= vectorF[numberK - 1 - 1] * vectorP[numberK - 1];
        vectorP[numberK - 1] -= vectorQ[numberK - 1 - 1] * vectorP[numberK - 1];
        vectorA[numberK - 1] = vectorQ[numberK - 1] = vectorP[numberK - 1];

        vectorA[numberK - 1 + 1] -= vectorC[numberK - 1 - 1] * vectorQ[numberK - 1 + 1];
        vectorF[numberK - 1 + 1] -= vectorF[numberK - 1 - 1] * vectorQ[numberK - 1 + 1];
        vectorQ[numberK - 1 + 1] -= vectorQ[numberK - 1 - 1] * vectorQ[numberK - 1 + 1];

        vectorA[numberK - 1 + 1] /= vectorB[numberK - 1 + 1];
        vectorF[numberK - 1 + 1] /= vectorB[numberK - 1 + 1];
        vectorB[numberK - 1 + 1] /= vectorB[numberK - 1 + 1];

        vectorP[numberK - 1 - 1] -= vectorA[numberK - 1 + 1] * vectorP[numberK - 1 - 2];
        vectorB[numberK - 1] = vectorP[numberK - 1 - 1];
        vectorF[numberK - 1] -= vectorF[numberK - 1 + 1] * vectorP[numberK - 1 - 2];
        vectorP[numberK - 1 - 2] -= vectorB[numberK - 1 + 1] * vectorP[numberK - 1 - 2];
        vectorC[numberK - 1] = vectorP[numberK - 1 - 2];

        vectorF[numberK - 1] /= vectorB[numberK - 1];
        vectorB[numberK - 1] /= vectorB[numberK - 1];
        vectorP[numberK - 1 - 1] = vectorB[numberK - 1];

        vectorF[numberK - 1 - 1] -= vectorF[numberK - 1] * vectorC[numberK - 1 - 1];
        vectorC[numberK - 1 - 1] -= vectorB[numberK - 1] * vectorC[numberK - 1 - 1];

        vectorF[numberK - 1 - 2] -= vectorF[numberK - 1 - 1] * vectorQ[numberK - 1 - 2];
        vectorQ[numberK - 1 - 2] -= vectorQ[numberK - 1 - 1] * vectorQ[numberK - 1 - 2];
        vectorC[numberK - 1 - 2] = vectorQ[numberK - 1 - 2];

        vectorF[numberK - 1 + 1] -= vectorF[numberK - 1] * vectorA[numberK - 1 + 1];
        vectorA[numberK - 1 + 1] -= vectorB[numberK - 1] * vectorA[numberK - 1 + 1];

        vectorSolutions[numberK - 1 + 1] = vectorF[numberK - 1 - 2];
        vectorSolutions[numberK - 1] = vectorF[numberK - 1 - 1];
        vectorSolutions[numberK - 1 - 1] = vectorF[numberK - 1];
        vectorSolutions[numberK - 1 - 2] = vectorF[numberK - 1 + 1];
    }
    

    public void PrintMatrixToFile(StreamWriter sw)
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
                    sw.Write(String.Format("{0:f1}  ", vectorQ[row]));
                }
                else if (row == numberK - 1)
                {
                    sw.Write(String.Format("{0:f1}  ", vectorP[col]));
                }
                else if (col == end_col - 1)
                {
                    sw.Write(String.Format("{0:f1}  ", vectorC[row]));
                }
                else if (col == end_col)
                {
                    sw.Write(String.Format("{0:f1}  ", vectorB[row]));
                }
                else if (col == end_col + 1)
                {
                    sw.Write(String.Format("{0:f1}  ", vectorA[row]));
                }
                else
                {
                    sw.Write(String.Format("{0:f1}  ", 0.0));
                }
            }
            sw.Write(String.Format("  =  {0:f1}  ", vectorF[row]));
            end_col--;
            sw.WriteLine();
        }
    }

    public void PrintSolutionToConsole()
    {
        for (int i = 0; i < matrixSize; i++)
        {
            Console.WriteLine(String.Format("X{0} = {1:f1}", i, vectorSolutions[i]));
        }
    }
}