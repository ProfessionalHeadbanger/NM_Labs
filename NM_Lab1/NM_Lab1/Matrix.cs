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
using System.Data;

class RandomGenerator
{
    private double left;
    private double right;
    Random random = new Random();

    public RandomGenerator(double left, double right) 
    {
        this.left = left;
        this.right = right;
        
    }

    public double GetRandomValue()
    {
        return left + random.NextDouble() * (right - left);
    }
}

class Matrix
{
    private double[,] matrix;
    private double[] f;
    private double[] x;
    private int size;
    private int k;

    public Matrix(int size, int k) 
    {
        matrix = new double[size, size];
        f = new double[size];
        x = new double[size];
        this.size = size;
        this.k = k;
    }
    public void Generate(double left, double right)
    {
        RandomGenerator gen = new RandomGenerator(left, right);

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (col == k - 1 || row == k - 1)
                {
                    matrix[row, col] = gen.GetRandomValue();
                }
                else if (row == size - 1 - col)
                {
                    matrix[row, col] = gen.GetRandomValue();
                    if (row > 0)
                    {
                        matrix[row, col + 1] = gen.GetRandomValue();
                    }
                    if (row < size - 1)
                    {
                        matrix[row, col - 1] = gen.GetRandomValue();
                    }
                }
                else
                {
                    matrix[row, col] = 0;
                }
            }
        }
    }

    public void InputFromFile(string path)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            for (int row = 0; row < size; row++)
            {
                string line = reader.ReadLine();
                string[] values = line.Split('\t');

                for (int col = 0; col < size; col++)
                {
                    matrix[row, col] = double.Parse(values[col]);
                }
            }

            reader.ReadLine();

            for (int i = 0; i < size; i++)
            {
                f[i] = double.Parse(reader.ReadLine());
            }
        }
    }

    public void PrintToFile(string path)
    {
        using (StreamWriter writer = new StreamWriter(path))
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (matrix[row, col] == 0)
                    {
                        writer.Write(string.Format("{0:f4} ", 0.0));
                    }
                    else
                    {
                        writer.Write(string.Format("{0:f4} ", matrix[row, col]));
                    }

                    if (col < size - 1)
                    {
                        writer.Write(" ");
                    }
                }

                writer.WriteLine($" = {f[row]:f4}");
            }
            writer.WriteLine();
        }
    }

    public void PrintToConsole()
    {
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (matrix[row, col] == 0)
                {
                    Console.Write(string.Format("{0:f4} ", 0.0));
                }
                else
                {
                    Console.Write(string.Format("{0:f4} ", matrix[row, col]));
                }
                
                if (col < size - 1)
                {
                    Console.Write(" ");
                }
            }

            Console.WriteLine($" = {f[row]:f4}");
        }
        Console.WriteLine();
    }

    public void DivideLine(int rowIndex)
    {
        double b_element = matrix[rowIndex, size - 1 - rowIndex];
        if (Math.Abs(b_element) < double.Epsilon)
        {
            return;
        }
        for (int col = 0; col < size; col++)
        {
            matrix[rowIndex, col] /= b_element;
        }
        f[rowIndex] /= b_element;
    }

    public void Subtract(int firstRow, int secondRow) // из второй вычитается первая, умноженная на коэффициент во второй
    {
        double koef = matrix[secondRow, size - 1 - firstRow];
        for (int col = 0; col < size; col++)
        {
            matrix[secondRow, col] -= matrix[firstRow, col] * koef;
        }
        f[secondRow] -= f[firstRow] * koef;
    }

    public void FirstStep()
    {
        for (int row = 0; row < k - 2; row++)
        {
            DivideLine(row);
            Subtract(row, row + 1);
            Subtract(row, k - 1);
        }
    }

    public void SecondStep()
    {
        for (int row = size - 1; row > k - 3; row--)
        {
            DivideLine(row);
            Subtract(row, row - 1);
            if (row > k)
            {
                Subtract(row, k - 1);
            }
        }
    }

    public void ThirdStep()
    {
        for (int row = 0; row < size; row++)
        {
            if (row != k - 2 && row != k - 3)
            {
                Subtract(k - 2, row);
            }
        }
    }

    public void FourthStep()
    {
        for (int row = k - 3; row > 0; row--)
        {
            Subtract(row, row - 1);
        }
        for (int row = k - 1; row < size - 1; row++)
        {
            Subtract(row, row + 1);
        }
    }
}