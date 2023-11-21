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

        Matrix m = new Matrix(n, k);
        m.InputFromFile("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\test.txt");
        m.PrintToConsole();

        m.FirstStep();
        m.PrintToConsole();

        m.SecondStep();
        m.PrintToConsole();

        m.ThirdStep();
        m.PrintToConsole();

        m.FourthStep();
        m.PrintToConsole();
    }
}