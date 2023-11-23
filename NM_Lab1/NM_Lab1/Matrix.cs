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
    private decimal left;
    private decimal right;
    Random random = new Random();

    public RandomGenerator(decimal left, decimal right)
    {
        this.left = left;
        this.right = right;

    }

    //public double GetRandomValue()
    //{
    //    return left + random.NextDouble() * (right - left);
    //}

    public decimal GetRandomDecimalValue()
    {
        var item = new decimal(random.NextDouble());
        return left + item * (right - left);
    }
}

class Matrix
{
    public decimal[,] matrix;
    public decimal[] f;
    public decimal[] f_one;
    public decimal[] x;
    public decimal[] x_one;
    public decimal[] x_expect;
    public decimal[] x_generated;
    public int size;
    public int k;
    public decimal delta;
    public decimal accuracy;

    public Matrix(int size, int k)
    {
        matrix = new decimal[size, size];
        f = new decimal[size];
        f_one = new decimal[size];
        x = new decimal[size]; // массив полученных решений
        x_one = new decimal[size]; // массив полученных единичных решений
        x_expect = new decimal[size]; // массив ожидаемых единичных решений
        x_generated = new decimal[size]; // массив сгенерированных решений
        this.size = size;
        this.k = k;
    }

    public void Generate(decimal left, decimal right)
    {
        RandomGenerator gen = new RandomGenerator(left, right);

        for (int col = 0; col < size; col++)
        {
            x_generated[col] = gen.GetRandomDecimalValue();
            x_expect[col] = 1.0M;
        }

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (col == k - 1 || row == k - 1)
                {
                    matrix[row, col] = gen.GetRandomDecimalValue();
                }
                else if (col == size - 2 - row || col == size - 1 - row || col == size - row)
                {
                    matrix[row, col] = gen.GetRandomDecimalValue();
                }
                else
                {
                    matrix[row, col] = 0;
                }
            }
        }

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                f[row] += x_generated[col] * matrix[row, col];
                f_one[row] += matrix[row, col];
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
                    matrix[row, col] = decimal.Parse(values[col]);
                    f_one[row] += matrix[row, col];
                }
                x_expect[row] = 1.0M;
            }

            reader.ReadLine();

            for (int i = 0; i < size; i++)
            {
                f[i] = decimal.Parse(reader.ReadLine());
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

    public void PrintGeneratedSolutionsToFile(string path)
    {
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine("Generated solutions:");
            for (int row = 0; row < size; row++)
            {
                writer.Write("x" + (row + 1) + $" = {x_generated[row]:f16}");
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
        decimal b_element = matrix[rowIndex, size - 1 - rowIndex];
        for (int col = 0; col < size; col++)
        {
            matrix[rowIndex, col] /= b_element;
        }
        f[rowIndex] /= b_element;
        f_one[rowIndex] /= b_element;
    }

    public void Subtract(int firstRow, int secondRow) // из второй вычитается первая, умноженная на коэффициент во второй
    {
        decimal koef = matrix[secondRow, size - 1 - firstRow];
        for (int col = 0; col < size; col++)
        {
            matrix[secondRow, col] -= matrix[firstRow, col] * koef;
        }
        f[secondRow] -= f[firstRow] * koef;
        f_one[secondRow] -= f_one[firstRow] * koef;
    }

    public void AccuracyTest(decimal[] _x, decimal[] _x_expect)
    {
        delta = 0;
        accuracy = 0;
        for (int i = 0; i < size; i++)
        {
            delta = Math.Max(Math.Abs((_x[i] - _x_expect[i]) / Math.Max(1, _x[i])), delta);
            accuracy = Math.Max(Math.Abs(_x[i] - 1.0M), accuracy);
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