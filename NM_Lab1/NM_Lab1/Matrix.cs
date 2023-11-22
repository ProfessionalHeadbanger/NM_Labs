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
using System.Transactions;

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
    public double[,] matrix;
    public double[] f;
    public double[] f_one;
    public double[] x;
    public double[] x_one;
    public double[] x_expect;
    public double[] x_generated;
    public int size;
    public int k;
    public double delta;
    public double accuracy;

    public Matrix(int size, int k)
    {
        matrix = new double[size, size];
        f = new double[size];
        f_one = new double[size];
        x = new double[size]; // массив полученных решений
        x_one = new double[size]; // массив полученных единичных решений
        x_expect = new double[size]; // массив ожидаемых единичных решений
        x_generated = new double[size]; // массив сгенерированных решений
        this.size = size;
        this.k = k;
    }
    public void Generate(double left, double right)
    {
        RandomGenerator gen = new RandomGenerator(left, right);

        for (int col = 0; col < size; col++)
        {
            x_generated[col] = gen.GetRandomValue();
        }

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (col == k - 1 || row == k - 1)
                {
                    matrix[row, col] = gen.GetRandomValue();
                }
                else if (col == size - 2 - row || col == size - 1 - row || col == size - row)
                {
                    matrix[row, col] = gen.GetRandomValue();
                }
                else
                {
                    matrix[row, col] = 0;
                }
                f[row] += x_generated[col] * matrix[row, col];
                f_one[row] += matrix[row, col];
            }
            x_expect[row] = 1.0;
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
                    f_one[row] += matrix[row, col];
                }
                x_expect[row] = 1.0;
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
                        writer.Write(string.Format("{0:f16} ", 0.0));
                    }
                    else
                    {
                        writer.Write(string.Format("{0:f16} ", matrix[row, col]));
                    }

                    if (col < size - 1)
                    {
                        writer.Write(" ");
                    }
                }

                writer.WriteLine($" = {f[row]:f16}");
            }
            writer.WriteLine();
        }
    }

    public void PrintSolutionsToFile(string path)
    {
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine("Solutions:");
            for (int row = 0; row < size; row++)
            {
                writer.Write("x" + (row + 1) + $" = {x[row]:f16}");
                writer.WriteLine();
            }
            writer.WriteLine();
            writer.WriteLine("Expected by 1 solutions:");
            for (int row = 0; row < size; row++)
            {
                writer.Write("x" + (row + 1) + $" = {x_one[row]:f16}");
                writer.WriteLine();
            }
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
                    Console.Write(string.Format("{0:f16} ", 0.0));
                }
                else
                {
                    Console.Write(string.Format("{0:f16} ", matrix[row, col]));
                }

                if (col < size - 1)
                {
                    Console.Write(" ");
                }
            }

            Console.WriteLine($" = {f[row]:f16}");
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
        f_one[rowIndex] /= b_element;
    }

    public void Subtract(int firstRow, int secondRow) // из второй вычитается первая, умноженная на коэффициент во второй
    {
        double koef = matrix[secondRow, size - 1 - firstRow];
        for (int col = 0; col < size; col++)
        {
            matrix[secondRow, col] -= matrix[firstRow, col] * koef;
        }
        f[secondRow] -= f[firstRow] * koef;
        f_one[secondRow] -= f_one[firstRow] * koef;
    }

    public void AccuracyTest(double[] _x, double[] _x_expect)
    {
        delta = 0;
        accuracy = 0;
        for (int i = 0; i < size; i++)
        {
            delta = Math.Max(Math.Abs((_x[i] - _x_expect[i]) / Math.Max(1, _x[i])), delta);
            accuracy = Math.Max(Math.Abs(_x[i] - 1.0), accuracy);
        }
    }

    public void PrintAccuracyToFile(string path)
    {
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.Write($"delta = {delta:f16}");
            writer.WriteLine();
            writer.Write($"accuracy = {accuracy:f16}");
        }
    }
}