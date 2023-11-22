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

        Matrix m = new Matrix(n, k);
        m.InputFromFile("C:\\Users\\Всеволод\\Desktop\\ЧМ\\NM_Lab1\\NM_Lab1\\test.txt");
        m.PrintToFile("C:\\Users\\Всеволод\\Desktop\\ЧМ\\NM_Lab1\\NM_Lab1\\initialMatrix.txt");

        m.FirstStep();
        m.PrintToFile("C:\\Users\\Всеволод\\Desktop\\ЧМ\\NM_Lab1\\NM_Lab1\\step1.txt");

        m.SecondStep();
        m.PrintToFile("C:\\Users\\Всеволод\\Desktop\\ЧМ\\NM_Lab1\\NM_Lab1\\step2.txt");

        m.ThirdStep();
        m.PrintToFile("C:\\Users\\Всеволод\\Desktop\\ЧМ\\NM_Lab1\\NM_Lab1\\step3.txt");

        m.FourthStep();
        m.PrintToFile("C:\\Users\\Всеволод\\Desktop\\ЧМ\\NM_Lab1\\NM_Lab1\\step4.txt");
        m.PrintSolutionsToFile("C:\\Users\\Всеволод\\Desktop\\ЧМ\\NM_Lab1\\NM_Lab1\\solutions.txt");

        m.AccuracyTest();
    }
}