using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
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
    public int size;
    public decimal[,] generated_lambda;
    public decimal[] diagonalElements;

    public Matrix(int size)
    {
        matrix = new decimal[size, size];
        this.size = size;
        generated_lambda = new decimal[size, size];
        diagonalElements = new decimal[size];
    }

    public void Generate(decimal left, decimal right)
    {
        RandomGenerator rnd = new RandomGenerator(left, right);
        decimal[] omega = new decimal[size];
        decimal summ = 0;
        for (int i = 0; i < size; i++)
        {
            omega[i] = rnd.GetRandomDecimalValue();
            summ += omega[i]*omega[i];
        }
        for (int i = 0; i < size; i++)
        {
            omega[i] /= summ;
        }
        decimal[,] wwt = new decimal[size, size];
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                wwt[row, col] = 2 * omega[row] * omega[col];
            }
        }
        decimal[,] E = new decimal[size, size];
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (row == col)
                {
                    E[row, col] = 1;
                }
            }
        }
        decimal[,] H = new decimal[size, size];
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                H[row, col] = E[row, col] - wwt[row, col];
            }
        }
        
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (row == col)
                {
                    generated_lambda[row, col] = rnd.GetRandomDecimalValue();
                }
            }
        }
        decimal[,] TransposedH = Transpose(H);
        decimal[,] H_Lambda = Multiply(H, generated_lambda);
        matrix = Multiply(H_Lambda, TransposedH);
    }

    public decimal[,] Transpose(decimal[,] matrix)
    {
        decimal[,] TransposedMatrix = new decimal[size, size];

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                TransposedMatrix[col, row] = matrix[row, col];
            }
        }

        return TransposedMatrix;
    }

    public decimal[,] Multiply(decimal[,] first, decimal[,] second)
    {
        decimal[,] MultiplyedMatrix = new decimal[size, size];

        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                decimal sum = 0;
                for (int k = 0; k < size; k++)
                {
                    sum += first[row, k] * second[k, col];
                }
                MultiplyedMatrix[col, row] = sum;
            }
        }

        return MultiplyedMatrix;
    }

    public void PrintToFile(string path)
    {
        using (StreamWriter writer = new StreamWriter(path))
        {
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    writer.Write(string.Format("{0:f16} ", matrix[row, col]));
                }
                writer.WriteLine();
            }
            writer.WriteLine();
        }
    }

    public void PrintLambdaToFile(string path)
    {
        using (StreamWriter writer = new StreamWriter(path))
        {
            writer.WriteLine("Obtained eigenvalues:");
            for (int row = 0; row < size; row++)
            {
                writer.Write("λ" + (row + 1) + $" = {diagonalElements[row]:f16}");
                writer.WriteLine();
            }
            writer.WriteLine();
        }
    }

    public void PrintGeneratedLambdaToFile(string path)
    {
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine("Generated eigenvalues:");
            for (int row = 0; row < size; row++)
            {
                for (int col = 0; col < size; col++)
                {
                    if (row == col)
                    {
                        writer.Write("λ" + (row + 1) + $" = {diagonalElements[row]:f16}");
                        writer.WriteLine();
                    }
                }
            }
            writer.WriteLine();
        }
    }
}

