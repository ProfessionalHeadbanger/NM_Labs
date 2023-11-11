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
        vectorSolutions = new double[matrixSize];

        //Дописать генерацию значений вектора F как сумму произведений сгенерированных Х на их коэффициенты из других векторов
        for (int row = 0; row < matrixSize; row++)
        {
            for (int col = 0; col < matrixSize; col++)
            {
                if (col == numberK - 1)
                {
                    vectorQ[row] = left + random.NextDouble() * (right - left);
                }
                if (row == 0)
                {
                    vectorA[row] = 0;
                    vectorB[row] = left + random.NextDouble() * (right - left);
                    vectorC[row] = left + random.NextDouble() * (right - left);
                }
                if ((row >= 1 && row <= numberK - 1 - 3) || (row >= numberK - 1 + 1 && row <= matrixSize - 1 - 1))
                {
                    vectorA[row] = left + random.NextDouble() * (right - left);
                    vectorB[row] = left + random.NextDouble() * (right - left);
                    vectorC[row] = left + random.NextDouble() * (right - left);
                }
                if (row == matrixSize - 1)
                {
                    vectorA[row] = left + random.NextDouble() * (right - left);
                    vectorB[row] = left + random.NextDouble() * (right - left);
                    vectorC[row] = 0;
                }
                if (row >= numberK - 1 - 2 && row <= numberK - 1 - 1)
                {
                    vectorA[row] = left + random.NextDouble() * (right - left);
                    vectorB[row] = left + random.NextDouble() * (right - left);
                    vectorC[row] = left + random.NextDouble() * (right - left);
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
                    vectorP[col] = left + random.NextDouble() * (right - left);
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
            
            vectorX[row] = left + random.NextDouble() * (right - left);
        }

        this.left = left;
        this.right = right;
    }

    public void StepFirst()
    {
        for (int row = 0; row < numberK - 1 - 1; row++)
        {
            double koef = vectorB[row]; 
            vectorB[row] /= koef; 
            vectorC[row] /= koef; 
            vectorQ[row] /= koef; 
            vectorF[row] /= koef; 

            koef = vectorA[row + 1]; 
            vectorA[row + 1] -= koef * vectorB[row]; 
            vectorB[row + 1] -= koef * vectorC[row]; 
            vectorQ[row + 1] -= koef * vectorQ[row]; 
            vectorF[row + 1] -= koef * vectorF[row]; 

            koef = vectorP[matrixSize - 1 - row]; 
            vectorP[matrixSize - 1 - row] -= koef * vectorB[row]; 
            vectorP[matrixSize - 1 - row - 1] -= koef * vectorC[row]; 
            vectorF[numberK - 1] -= koef * vectorF[row];
            vectorQ[numberK - 1] -= koef * vectorQ[row];
            vectorA[numberK - 1] = vectorQ[numberK - 1];
            vectorP[numberK - 1] = vectorQ[numberK - 1];
            vectorC[numberK - 1 - 2] = vectorQ[numberK - 1 - 2];
            vectorB[numberK - 1 - 1] = vectorQ[numberK - 1 - 1];
        }
    }
    public void StepSecond()
    {
        for (int row = matrixSize - 1; row > numberK - 1; row--)
        {
            double koef = vectorB[row];
            vectorB[row] /= koef;
            vectorA[row] /= koef;
            vectorQ[row] /= koef;
            vectorF[row] /= koef;

            koef = vectorC[row - 1];
            vectorC[row - 1] -= koef * vectorB[row];
            vectorB[row - 1] -= koef * vectorA[row];
            vectorQ[row - 1] -= koef * vectorQ[row];
            vectorF[row - 1] -= koef * vectorF[row];

            koef = vectorP[matrixSize - 1 - row];
            vectorP[matrixSize - 1 - row] -= koef * vectorB[row];
            vectorP[matrixSize - 1 - row + 1] -= koef * vectorA[row];
            vectorF[numberK - 1] -= koef * vectorF[row];
            vectorQ[numberK - 1] -= koef * vectorQ[row];
            vectorA[numberK - 1] = vectorQ[numberK - 1];
            vectorP[numberK - 1] = vectorQ[numberK - 1];
            vectorC[numberK - 1 - 2] = vectorQ[numberK - 1 - 2];
            vectorB[numberK - 1 - 1] = vectorQ[numberK - 1 - 1];
            vectorB[numberK - 1] = vectorP[numberK - 1 - 1];
        }
        double koef_dop = vectorB[numberK - 1];
        vectorB[numberK - 1] /= koef_dop;
        vectorP[numberK - 1 - 1] = vectorB[numberK - 1];
        vectorQ[numberK - 1] /= koef_dop;
        vectorP[numberK - 1] = vectorQ[numberK - 1];
        vectorA[numberK - 1] = vectorQ[numberK - 1];
        koef_dop = vectorC[numberK - 1 - 1];
        vectorC[numberK - 1 - 1] -= koef_dop * vectorB[numberK - 1];
        vectorQ[numberK - 1 - 1] -= koef_dop * vectorQ[numberK - 1];
        vectorB[numberK - 1 - 1] = vectorQ[numberK - 1 - 1];
        vectorF[numberK - 1 - 1] -= koef_dop * vectorF[numberK - 1];
    }

    public void StepThird()
    {
        vectorSolutions[numberK - 1] = vectorF[numberK - 1 - 1] / vectorQ[numberK - 1 - 1];
        vectorSolutions[numberK - 1 + 1] = vectorF[numberK - 1 - 2] - vectorQ[numberK - 1 - 2] * vectorSolutions[numberK - 1];
        vectorSolutions[numberK - 1 - 1] = vectorF[numberK - 1] - vectorQ[numberK - 1] * vectorSolutions[numberK - 1];

        for (int row = numberK - 1 - 3; row >= 0; row--)
        {
            vectorSolutions[matrixSize - 1 - row] = vectorF[row] - vectorQ[row] * vectorSolutions[numberK - 1] - vectorC[row] * vectorSolutions[matrixSize - 1 - row - 1];
        }
        for (int row = numberK - 1 + 1; row < matrixSize; row++)
        {
            vectorSolutions[matrixSize - 1 - row] = vectorF[row] - vectorQ[row] * vectorSolutions[numberK - 1] - vectorA[row] * vectorSolutions[matrixSize - 1 - row + 1];
        }
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
                    sw.Write(String.Format("{0:f12}  ", vectorQ[row]));
                }
                else if (row == numberK - 1)
                {
                    sw.Write(String.Format("{0:f12}  ", vectorP[col]));
                }
                else if (col == end_col - 1)
                {
                    sw.Write(String.Format("{0:f12}  ", vectorC[row]));
                }
                else if (col == end_col)
                {
                    sw.Write(String.Format("{0:f12}  ", vectorB[row]));
                }
                else if (col == end_col + 1)
                {
                    sw.Write(String.Format("{0:f12}  ", vectorA[row]));
                }
                else
                {
                    sw.Write(String.Format("{0:f12}  ", 0.0));
                }
            }
            sw.Write(String.Format("  =  {0:f12}  ", vectorF[row]));
            end_col--;
            sw.WriteLine();
        }
    }

    public void PrintSolutionToConsole()
    {
        for (int i = 0; i < matrixSize; i++)
        {
            Console.WriteLine(String.Format("X{0} = {1:f12}", i, vectorSolutions[i]));
        }
    }
}