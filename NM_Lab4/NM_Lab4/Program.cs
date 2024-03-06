using System;

class Program
{
    const int pointsCount = 4;
    static int IER = -1;
    public static int GetNFromFile(string path)
    {
        using (StreamReader reader = new StreamReader("E:\\Лабы\\ЧМ\\NM_Lab4\\NM_Lab4\\" + path))
        {
            string line = reader.ReadLine();
            return int.Parse(line);
        }
    }

    public static decimal GetYValueFromPoly(decimal x, Polinom poly)
    {
        return poly.coefficients[0] + poly.coefficients[1] * x + poly.coefficients[2] * x * x + poly.coefficients[3] * x * x * x;
    }

    public static void FindError(Table table)
    {
        Polinom[] polys = new Polinom[table.N - 3];
        for (int i = 0; i <= table.N - pointsCount; i++)
        {
            decimal[] subsetX = new decimal[pointsCount];
            decimal[] subsetY = new decimal[pointsCount];
            Array.Copy(table.X, i, subsetX, 0, pointsCount);
            Array.Copy(table.Y, i, subsetY, 0, pointsCount);

            polys[i] = new Polinom(subsetX, subsetY);
        }

        int index = -1;
        for (int i = 0; i < polys.Length; i++)
        {
            for (int j = i + 1; j < polys.Length; j++)
            {
                if (Polinom.ArePolysEqual(polys[i], polys[j]))
                {
                    index = i;
                }
            }
        }

        if (index != -1)
        {
            for (int i = 0; i < table.N; i++)
            {
                table.YY[i] = GetYValueFromPoly(table.X[i], polys[index]);
            }

            bool flag = true;
            for (int i = 0; i < table.N && flag; i++)
            {
                if (table.Y[i] != table.YY[i])
                {
                    flag = false;
                }
            }

            if (flag)
            {
                IER = 1;
            }
            else
            {
                IER = 0;
            }
            polys[index].OutputPolinomToConsole();
        }
        else
        {
            IER = 2;
        }
    }

    public static void Main()
    {
        bool menu = true;
        while (menu)
        {
            Console.WriteLine("1) Ввод таблицы значений с файла;\n2) Выход");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Введите название файла: ");
                    string path = Console.ReadLine();
                    int N_from_table = GetNFromFile(path);
                    Table table_from_file = new Table(N_from_table);
                    table_from_file.InputFromFile(path);
                    table_from_file.OutputTableToConsole();
                    IER = table_from_file.Validations();
                    if (IER == -1)
                    {
                        FindError(table_from_file);
                        table_from_file.OutputFixedTableToConsole();
                    }
                    Console.WriteLine($"IER = {IER}");
                    break;  
                case "2":
                    menu = false;
                    break;
                default:
                    Console.WriteLine("Неправильный ввод");
                    break;
            }
        }
    }
}