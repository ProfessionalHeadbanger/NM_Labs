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
            m.x[row] = m.f[m.size - 1 - row];
            m.x_one[row] = m.f_one[m.size - 1 - row];
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

    public static void Tests(string inputpath, string outputpath)
    {
        StreamReader reader = new StreamReader(inputpath);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] numbers = line.Split(' ');
            int size = int.Parse(numbers[0]);
            int k = size / 2 + 1;
            decimal left = decimal.Parse(numbers[1]);
            decimal right = decimal.Parse(numbers[2]);

            Matrix matrix = new Matrix(size, k);
            matrix.Generate(left, right);
            FirstStep(matrix);
            matrix.AccuracyTest(matrix.x_one);
            matrix.InaccuracyTest(matrix.x, matrix.x_generated);

            using (StreamWriter writer = new StreamWriter(outputpath, true))
            {
                writer.WriteLine($"Size: {size}; Left: {left}; Right: {right}; Accuracy: {matrix.accuracy:e}; Inaccuracy: {matrix.inaccuracy:e}");
            }
        }
    }
    static void Main()
    {
        bool menu = true;
        while (menu)
        {
            Console.WriteLine("1) Решить СЛАУ из файла;\n2) Решить сгенерированную СЛАУ;\n3) Набор тестов;\n4) Выход");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    int size = InputSize("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\variant.txt");
                    Matrix static_m = new Matrix(size, size / 2 + 1);
                    static_m.InputFromFile("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\variant.txt");
                    static_m.PrintToFile("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\initial_static_matrix.txt");
                    FirstStep(static_m);
                    static_m.PrintSolutionsToFile("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\static_matrix_solutions.txt");
                    static_m.AccuracyTest(static_m.x_one);
                    static_m.PrintAccuracy("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\static_matrix_solutions.txt");
                    break;
                case "2":
                    Console.Write("Размер матрицы: ");
                    int n = Convert.ToInt32(Console.ReadLine());
                    int k = n / 2 + 1;
                    Matrix generated_m = new Matrix(n, k);
                    generated_m.Generate(-10, 10);
                    generated_m.PrintToFile("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\initial_generated_matrix.txt");
                    FirstStep(generated_m);
                    generated_m.PrintSolutionsToFile("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\generated_matrix_solutions.txt");
                    generated_m.PrintGeneratedSolutionsToFile("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\generated_matrix_solutions.txt");
                    generated_m.AccuracyTest(generated_m.x_one);
                    generated_m.InaccuracyTest(generated_m.x, generated_m.x_generated);
                    generated_m.PrintAccuracy("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\generated_matrix_solutions.txt");
                    generated_m.PrintInaccuracy("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\generated_matrix_solutions.txt");
                    break;
                case "3":
                    Tests("D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\tests.txt", "D:\\Лабы\\ЧМ\\NM_Lab1\\NM_Lab1\\result_tests.txt");
                    break;
                case "4":
                    menu = false;
                    break;
                default:
                    Console.WriteLine("Неправильный ввод");
                    break;
            }
        }
    }
}