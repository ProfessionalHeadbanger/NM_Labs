using System;
using System.Net.Http.Headers;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("Размер матрицы: ");
        int n = Convert.ToInt32(Console.ReadLine());
        int k = n/2 + 1;

        Matrix matrix = new Matrix(n, k, -10, 10);
        StreamWriter sw = new StreamWriter("C:\\Users\\Всеволод\\Desktop\\C#Labs\\ConsoleApp1\\ConsoleApp1\\Test.txt");
        matrix.printToFile(sw);
        sw.Close();
    }
}