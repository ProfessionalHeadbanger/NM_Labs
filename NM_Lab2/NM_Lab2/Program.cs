class Program
{ 
    public static void DirectStroke(Matrix m)
    {
        for (int firstRow = 0; firstRow <= m.N - m.L; firstRow++)
        {
            m.DivideLine(firstRow);
            for (int secondRow = firstRow + 1; secondRow < firstRow + m.L; secondRow++)
            {
                m.SubtractDirectStroke(firstRow, secondRow);
            }
        }

        int endConstraint = m.L - 1;
        for (int firstRow = m.N - m.L + 1; firstRow < m.N; firstRow++)
        {
            m.DivideLine(firstRow);
            for (int secondRow = firstRow + 1; secondRow < firstRow + endConstraint; secondRow++)
            {
                m.SubtractDirectStroke(firstRow, secondRow);
            }
            endConstraint--;
        }

        BackwardStroke(m);
    }

    public static void BackwardStroke(Matrix m)
    {
        for (int firstRow = m.N - 1; firstRow >= m.L - 1; firstRow--)
        {
            for (int secondRow = firstRow - 1; secondRow > firstRow - m.L; secondRow--)
            {
                m.SubtractBackwardStroke(firstRow, secondRow);
            }
        }

        int endConstraint = m.L - 1;
        for (int firstRow = m.L - 2; firstRow > 0; firstRow--)
        {
            for (int secondRow = firstRow - 1; secondRow > firstRow - endConstraint; secondRow--)
            {
                m.SubtractBackwardStroke(firstRow, secondRow);
            }
            endConstraint--;
        }

        for (int row = 0; row < m.N; row++)
        {
            m.x[row] = m.f[row];
        }
    }

    public static void Tests(string inputpath, string outputpath)
    {
        StreamReader reader = new StreamReader(inputpath);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] numbers = line.Split(' ');
            int n = int.Parse(numbers[0]);
            int l = int.Parse(numbers[1]);
            decimal left = decimal.Parse(numbers[2]);
            decimal right = decimal.Parse(numbers[3]);

            Matrix matrix = new Matrix(n, l);
            matrix.Generate(left, right);
            DirectStroke(matrix);
            matrix.InnacuracyTest(matrix.x, matrix.x_generated);

            using (StreamWriter writer = new StreamWriter(outputpath, true))
            {
                writer.WriteLine($"N: {n}; L: {l}; Left: {left}; Right: {right}; Inaccuracy: {matrix.inaccuracy:e}");
            }
        }
    }

    public static int Grading(int k)
    {
        int result = 1;
        for (int i = 0; i < k; i++)
        {
            result *= 10;
        }
        return result;
    }

    public static void BadMatrixTests(string inputpath, string outputpath)
    {
        StreamReader reader = new StreamReader(inputpath);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] numbers = line.Split(' ');
            int n = int.Parse(numbers[0]);
            int l = int.Parse(numbers[1]);
            decimal left = decimal.Parse(numbers[2]);
            decimal right = decimal.Parse(numbers[3]);
            int k = int.Parse(numbers[4]);

            Matrix matrix = new Matrix(n, l);
            matrix.GenerateAndMultiply(left, right);
            DirectStroke(matrix);

            matrix.InnacuracyTest(matrix.x, matrix.x_generated);

            using (StreamWriter writer = new StreamWriter(outputpath, true))
            {
                writer.WriteLine($"N: {n}; L: {l}; K: {k}; Left: {left}; Right: {right}; Inaccuracy: {matrix.inaccuracy:e}");
            }
        }
    }

    static void Main()
    {
        bool menu = true;
        while (menu)
        {
            Console.WriteLine("1) Решить сгенерированную СЛАУ;\n" +
                "2) Набор тестов для ленточных матриц\n" +
                "3) Набор тестов для хорошо обусловленных матриц\n" +
                "4) Набор тестов для плохо обусловленных матриц\n" +
                "5) Выход\n");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Размер матрицы: ");
                    int n = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Ширина ленты: ");
                    int l = Convert.ToInt32(Console.ReadLine());
                    Matrix generated_m = new Matrix(n, l);
                    generated_m.Generate(-10, 10);
                    generated_m.PrintToFile("D:\\Лабы\\ЧМ\\NM_Lab2\\NM_Lab2\\initial_generated_matrix.txt");
                    DirectStroke(generated_m);
                    generated_m.PrintSolutionsToFile("D:\\Лабы\\ЧМ\\NM_Lab2\\NM_Lab2\\generated_matrix_solutions.txt");
                    generated_m.PrintGeneratedSolutionsToFile("D:\\Лабы\\ЧМ\\NM_Lab2\\NM_Lab2\\generated_matrix_solutions.txt");
                    generated_m.InnacuracyTest(generated_m.x, generated_m.x_generated);
                    generated_m.PrintInnacuracy("D:\\Лабы\\ЧМ\\NM_Lab2\\NM_Lab2\\generated_matrix_solutions.txt");
                    break;
                case "2":
                    Tests("D:\\Лабы\\ЧМ\\NM_Lab2\\NM_Lab2\\tape_matrix_tests.txt", "D:\\Лабы\\ЧМ\\NM_Lab2\\NM_Lab2\\tape_matrix_results.txt");
                    break;
                case "3":
                    Tests("D:\\Лабы\\ЧМ\\NM_Lab2\\NM_Lab2\\well_matrix_tests.txt", "D:\\Лабы\\ЧМ\\NM_Lab2\\NM_Lab2\\well_matrix_results.txt");
                    break;
                case "4":
                    BadMatrixTests("D:\\Лабы\\ЧМ\\NM_Lab2\\NM_Lab2\\bad_matrix_tests.txt", "D:\\Лабы\\ЧМ\\NM_Lab2\\NM_Lab2\\bad_matrix_results.txt");
                    break;
                case "5":
                    menu = false;
                    break;
                default:
                    Console.WriteLine("Неправильный ввод");
                    break;
            }
        }
    }
}
