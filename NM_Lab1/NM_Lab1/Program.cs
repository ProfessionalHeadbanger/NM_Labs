using System;
using System.Net.Http.Headers;
using System.IO;

// Переделать вывод, чтобы все выводилось в один файл (возможно даже убрать вывод промежуточных шагов, оставить только начальную матрицу и решение)
// Сделать какие то погрешности или че там еще короче хуйню какую то еще сделать
// Сделать тесты на разные размеры матрицы и разные границы генерации значений
class Program
{
    static void Main()
    {
        Console.WriteLine("Размер матрицы: ");
        int n = Convert.ToInt32(Console.ReadLine());
        int k = n / 2 + 1;

        Matrix matrix = new Matrix(n, k, 1, 5);
        StreamWriter initialMatrix = new StreamWriter("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\initialMatrix.txt");
        matrix.PrintMatrixToFile(initialMatrix);
        initialMatrix.Close();

        matrix.FindSolutions();
        StreamWriter step1 = new StreamWriter("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\step1.txt");
        matrix.PrintMatrixToFile(step1);
        step1.Close();

       
    }
}