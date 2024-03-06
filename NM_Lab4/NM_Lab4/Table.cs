using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

class Table
{
    public decimal[] X;
    public decimal[] Y;
    public decimal[] YY;
    public int N;

    public Table(int N)
    {
        X = new decimal[N];
        Y = new decimal[N];
        YY = new decimal[N];
        this.N = N;
    }

    public void InputFromFile(string path)
    {
        using (StreamReader reader = new StreamReader("E:\\Лабы\\ЧМ\\NM_Lab4\\NM_Lab4\\" + path))
        {
            reader.ReadLine();

            string[] line1 = reader.ReadLine().Split(' ');
            for (int i = 0; i < N; i++)
            {
                X[i] = decimal.Parse(line1[i]);
            }

            string[] line2 = reader.ReadLine().Split(' ');
            for (int i = 0; i < N; i++)
            {
                Y[i] = decimal.Parse(line2[i]);
            }

            string[] line3 = reader.ReadLine().Split(' ');
            int index = int.Parse(line3[0]) - 1;
            if (index != -2)
            {
                Y[index] = decimal.Parse(line3[1]);
            }
        }
    }

    public void OutputTableToConsole()
    {
        Console.Write("X | ");
        for (int i = 0; i < N; i++)
        {
            Console.Write($"{X[i]:f4}" + " |");
        }
        Console.WriteLine();
        Console.Write("Y | ");
        for (int i = 0; i < N; i++)
        {
            Console.Write($"{Y[i]:f4}" + " |");
        }
        Console.WriteLine();
    }

    public void OutputFixedTableToConsole()
    {
        Console.Write("X | ");
        for (int i = 0; i < N; i++)
        {
            Console.Write($"{X[i]:f4}" + " |");
        }
        Console.WriteLine();
        Console.Write("YY | ");
        for (int i = 0; i < N; i++)
        {
            Console.Write($"{YY[i]:f4}" + " |");
        }
        Console.WriteLine();
    }

    public bool CheckAscendingX()
    {
        for (int i = 1; i < N; i++)
        {
            if (X[i] <= X[i - 1])
            {
                return false;
            }
        }
        return true;
    }

    public int Validations()
    {
        int ier = -1;

        if (!CheckAscendingX())
        {
            Console.WriteLine("Нарушен порядок возрастания аргумента X");
            ier = 3;
            return ier;
        }

        return ier;
    }
}
