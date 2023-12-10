// Метод исключения Гаусса со схемой единственного деления для ленточных матриц.
// Входные параметры: 
// N, L - размерность системы и половина ширины ленты матрицы
// A - массив размерности N(2L - 1), содержащий ленту матрицы исходной системы уравнений
// f - вектор правой части системы размерности N
// Выходные параметры:
// IER - код завершения
// х - вектор решения размерности N


using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    public decimal[] x;
    public decimal[] x_generated;
    public decimal inaccuracy;
    public int N;
    public int L;

    public Matrix(int N, int L)
    {
        matrix = new decimal[N, 2*L - 1];
        f = new decimal[N];
        x = new decimal[N];
        x_generated = new decimal[N];
        this.N = N;
        this.L = L;
    }

    public void Generate(decimal left, decimal right)
    {
        RandomGenerator gen = new RandomGenerator(left, right);

        for (int col = 0; col < N; col++)
        {
            x_generated[col] = gen.GetRandomDecimalValue();
        }

        int i = L - 1;
        for (int row = 0; row < N / 2; row++)
        {
            for (int col = 0; col < 2 * L - 1; col++)
            {
                if (col < i)
                {
                    matrix[row, col] = 0;
                }
                else
                {
                    matrix[row, col] = gen.GetRandomDecimalValue();
                }
            }
            i--;
        }

        int j = L - 1;
        for (int row = N - 1; row > N / 2 - 1; row--)
        {
            for (int col = 2 * L - 2; col >= 0; col--)
            {
                if (col > 2 * L - 2 - j)
                {
                    matrix[row, col] = 0;
                }
                else
                {
                    matrix[row, col] = gen.GetRandomDecimalValue();
                }
            }
            j--;
        }

        for (int row = 0; row < N; row++)
        {
            for (int col = 0; col < 2 * L - 1; col++)
            {
                f[row] += matrix[row, col] * x_generated[row + col - L - 1];
            }
        }
    }

    public void DivideLine(int rowIndex)
    {
        decimal koef = matrix[rowIndex, L - 1];
        for (int col = 0; col < 2 * L - 1; col++)
        {
            matrix[rowIndex, col] /= koef;
        }
        f[rowIndex] /= koef;
    }

    public void Subtract(int firstRow, int secondRow) // из второй вычитается первая, умноженная на коэффициент во второй
    {
        int position = L - 1 - secondRow + firstRow;
        decimal koef = matrix[secondRow, position];
        for (int col = L - 1; col < 2 * L - 1; col++)
        {
            matrix[secondRow, position] -= koef * matrix[firstRow, col];
            position++;
        }
        f[secondRow] -= f[firstRow] * koef;
    }

    public void PrintToFile(string path)
    {
        using (StreamWriter writer = new StreamWriter(path))
        {
            for (int row = 0; row < N; row++)
            {
                for (int col = 0; col < 2 * L - 1; col++)
                {
                    if (matrix[row, col] == 0)
                    {
                        writer.Write(string.Format("{0:f16} ", 0.0));
                    }
                    else
                    {
                        writer.Write(string.Format("{0:f16} ", matrix[row, col]));
                    }

                    if (col < 2 * L - 2)
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
            for (int row = 0; row < N; row++)
            {
                writer.Write("x" + (row + 1) + $" = {x[row]:f16}");
                writer.WriteLine();
            }
            writer.WriteLine();
        }
    }

    public void PrintGeneratedSolutionsToFile(string path)
    {
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine("Generated solutions:");
            for (int row = 0; row < N; row++)
            {
                writer.Write("x" + (row + 1) + $" = {x_generated[row]:f16}");
                writer.WriteLine();
            }
            writer.WriteLine();
        }
    }

    public void InnacuracyTest(decimal[] _x, decimal[] _x_expect)
    {
        inaccuracy = 0;
        for (int i = 0; i < N; i++)
        {
            inaccuracy = Math.Max(Math.Abs((_x[i] - _x_expect[i]) / Math.Max(1, _x[i])), inaccuracy);
        }
    }

    public void PrintInnacuracy(string path)
    {
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine($"Inaccuracy: {inaccuracy:e}");
        }
    }
}
