using System;
using System.Net.Http.Headers;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Размер матрицы: ");
        int n = Convert.ToInt32(Console.ReadLine());
        int k = n / 2 + 1;

        Matrix matrix = new Matrix(n, k, -10, 10);
        StreamWriter initialMatrix = new StreamWriter("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\initialMatrix.txt");
        matrix.PrintMatrixToFile(initialMatrix);
        initialMatrix.Close();

        matrix.StepFirst();
        StreamWriter step1 = new StreamWriter("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\step1.txt");
        matrix.PrintMatrixToFile(step1);
        step1.Close();

        matrix.StepSecond();
        StreamWriter step2 = new StreamWriter("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\step2.txt");
        matrix.PrintMatrixToFile(step2);
        step2.Close();
    }
}
