using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
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
    public decimal[,] initial_matrix;
    public int size;
    public decimal[,] generated_lambda;
    public decimal[] diagonalElements;
    public decimal[,] eigenVectors;
    decimal[] omega;
    public decimal accuracyEvaluatuion;
    public decimal accuracyMeasure;

    public Matrix(int size)
    {
        matrix = new decimal[size, size];
        initial_matrix = new decimal[size, size];
        this.size = size;
        generated_lambda = new decimal[size, size];
        diagonalElements = new decimal[size];
        omega = new decimal[size];
        eigenVectors = new decimal[size, size];
    }

    public void Generate(decimal left, decimal right)
    {
        RandomGenerator rnd = new RandomGenerator(left, right);
        
        decimal summ = 0;
        for (int i = 0; i < size; i++)
        {
            omega[i] = rnd.GetRandomDecimalValue();
            summ += omega[i]*omega[i];
        }
        summ = (decimal)Math.Sqrt((double)summ);
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
        initial_matrix = matrix;
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

    public decimal[] MultiplyMatrixVector(decimal[,] matrix, decimal[] vector)
    {
        decimal[] result = new decimal[size];

        for (int i = 0; i < size; i++)
        {
            decimal sum = 0;
            for (int j = 0; j < size; j++)
            {
                sum += matrix[i, j] * vector[j];
            }
            result[i] = sum;
        }

        return result;
    }

    public void AccuracyEvaluationTest(decimal[] _lambda, decimal[,] _lambda_expect)
    {
        accuracyEvaluatuion = 0;
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                if (row == col)
                {
                    accuracyEvaluatuion = Math.Max(Math.Abs(_lambda[row] - _lambda_expect[row, col]), accuracyEvaluatuion);
                }
            }
        }
    }

    public void AccuracyMeasureTest(decimal[,] A, decimal[] lambda, decimal[,] x)
    {
        accuracyMeasure = 0;
        decimal accuracyMeasureVector = 0;
        for (int i = 0; i < size; i++)
        {
            decimal[] vector = new decimal[size];
            for (int j = 0; j < size; j++)
            {
                vector[j] = x[j, i];
            }
            decimal[] Ax = MultiplyMatrixVector(A, vector);
            for (int j = 0; j < size; j++)
            {
                vector[j] *= lambda[i];
                Ax[j] -= vector[j];
                accuracyMeasureVector = Math.Max(Math.Abs(Ax[j] - 0.0M), accuracyMeasureVector);
            }
            accuracyMeasure = Math.Max(accuracyMeasureVector, accuracyMeasure);
        }
    }

    public void PrintEvaluation(string path)
    {
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine($"Accuracy evaluation: {accuracyEvaluatuion:e}");
        }
    }

    public void PrintMeasure(string path)
    {
        using (StreamWriter writer = new StreamWriter(path, true))
        {
            writer.WriteLine($"Accuracy measure: {accuracyMeasure:e}");
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

