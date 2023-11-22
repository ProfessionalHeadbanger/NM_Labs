using System;
using System.Net.Http.Headers;
using System.IO;
using System.Drawing;

class Program
{
    public static void FirstStep(Matrix m)
    {
        for (int row = 0; row < m.k - 2; row++)
        {
            m.DivideLine(row);
            m.Subtract(row, row + 1);
            m.Subtract(row, m.k - 1);
        }
        SecondStep(m);
    }

    public static void SecondStep(Matrix m)
    {
        for (int row = m.size - 1; row > m.k - 3; row--)
        {
            m.DivideLine(row);
            m.Subtract(row, row - 1);
            if (row > m.k)
            {
                m.Subtract(row, m.k - 1);
            }
        }
        ThirdStep(m);
    }

    public static void ThirdStep(Matrix m)
    {
        for (int row = 0; row < m.size; row++)
        {
            if (row != m.k - 2 && row != m.k - 3)
            {
                m.Subtract(m.k - 2, row);
            }
        }
        FourthStep(m);
    }

    public static void FourthStep(Matrix m)
    {
        for (int row = m.k - 3; row > 0; row--)
        {
            m.Subtract(row, row - 1);
        }
        for (int row = m.k - 1; row < m.size - 1; row++)
        {
            m.Subtract(row, row + 1);
        }
        for (int row = 0; row < m.size; row++)
        {
            m.x[row] = m.f[row];
            m.x_one[row] = m.f_one[row];
        }
    }

    public static int InputSize(string path)
    {
        int size = 0;
        using (StreamReader reader = new StreamReader(path))
        {
            string line = reader.ReadLine();
            if (line != null)
            {
                size = line.Split('\t').Length;
            }
        }
        return size;
    }
    static void Main()
    {
        bool menu = true;
        while (menu)
        {
            Console.WriteLine("1) Решить СЛАУ из файла;\n2) Решить сгенерированную СЛАУ\n3) Выход");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    int size = InputSize("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\test.txt");
                    Matrix static_m = new Matrix(size, size / 2 + 1);
                    static_m.InputFromFile("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\test.txt");
                    static_m.PrintToFile("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\initial_static_matrix.txt");
                    FirstStep(static_m);
                    static_m.PrintSolutionsToFile("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\static_matrix_solutions.txt");
                    static_m.AccuracyTest(static_m.x_one, static_m.x_expect);
                    static_m.PrintAccuracyToFile("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\static_matrix_accuracy_test.txt");
                    break;
                case "2":
                    Console.Write("Размер матрицы: ");
                    int n = Convert.ToInt32(Console.ReadLine());
                    int k = n / 2 + 1;
                    Matrix generated_m = new Matrix(n, k);
                    generated_m.Generate(1, 10);
                    generated_m.PrintToFile("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\initial_generated_matrix.txt");
                    FirstStep(generated_m);
                    generated_m.PrintSolutionsToFile("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\generated_matrix_solutions.txt");
                    generated_m.AccuracyTest(generated_m.x, generated_m.x_generated);
                    generated_m.PrintAccuracyToFile("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\generated_matrix_accuracy_test1.txt");
                    generated_m.AccuracyTest(generated_m.x_one, generated_m.x_expect);
                    generated_m.PrintAccuracyToFile("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\generated_matrix_accuracy_test2.txt");
                    break;
                case "3":
                    menu = false;
                    break;
                default:
                    Console.WriteLine("Неправильный ввод");
                    break;
            }
        }
    }
}

