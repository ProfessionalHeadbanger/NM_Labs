class Program
{
    public static void RightJacobiMethod(Matrix matrix, decimal tolerance, int maxIterations)
    {
        decimal[,] rotationMatrix = new decimal[matrix.size, matrix.size];

        for (int i = 0; i < matrix.size; i++)
        {
            matrix.diagonalElements[i] = matrix.matrix[i, i];
            rotationMatrix[i, i] = 1.0M;
        }

        for (int iteration = 0; iteration < maxIterations; iteration++)
        {
            int p, q;
            FindMaxOffDiagonalElement(matrix, out p, out q);

            decimal maxOffDiagonal = Math.Abs(matrix.matrix[p, q]);
            if (maxOffDiagonal < tolerance)
            {
                break;
            }

            decimal theta = 0.5M * (decimal)Math.Atan2((double)(2 * matrix.matrix[p, q]), (double)(matrix.diagonalElements[q] - matrix.diagonalElements[p]));

            rotationMatrix[p, p] = rotationMatrix[q, q] = (decimal)Math.Cos((double)theta);
            rotationMatrix[p, q] = (decimal)Math.Sin((double)theta);
            rotationMatrix[q, p] = -(decimal)Math.Sin((double)theta);

            matrix.matrix = matrix.Multiply(matrix.matrix, rotationMatrix);
            matrix.diagonalElements[p] = matrix.matrix[p, p];
            matrix.diagonalElements[q] = matrix.matrix[q, q];
        }
    }

    static void FindMaxOffDiagonalElement(Matrix matrix, out int p, out int q)
    {
        decimal maxElement = 0.0M;
        p = q = 0;

        for (int i = 0; i < matrix.size - 1; i++)
        {
            for (int j = i + 1; j < matrix.size; j++)
            {
                decimal currentElement = Math.Abs(matrix.matrix[i, j]);
                if (currentElement > maxElement)
                {
                    maxElement = currentElement;
                    p = i;
                    q = j;
                }
            }
        }
    }



    public static void Main()
    {
        bool menu = true;
        while (menu)
        {
            Console.WriteLine("1) Найти собственные значения матрицы\n2) Набор тестов\n3) Выход");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Размер матрицы: ");
                    int n = Convert.ToInt32(Console.ReadLine());
                    Matrix generated_matrix = new Matrix(n);
                    generated_matrix.Generate(-2, 2);
                    generated_matrix.PrintToFile("D:\\Лабы\\ЧМ\\NM_Lab3\\NM_Lab3\\initial_generated_matrix.txt");
                    Console.Write("Максимальный по модулю внедиагональный элемент: ");
                    decimal tolerance = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Максимальное число поворотов: ");
                    int maxIterations = Convert.ToInt32(Console.ReadLine());
                    RightJacobiMethod(generated_matrix, tolerance, maxIterations);
                    generated_matrix.PrintLambdaToFile("D:\\Лабы\\ЧМ\\NM_Lab3\\NM_Lab3\\generated_matrix_values.txt");
                    generated_matrix.PrintGeneratedLambdaToFile("D:\\Лабы\\ЧМ\\NM_Lab3\\NM_Lab3\\generated_matrix_values.txt");
                    break;
                case "2":

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