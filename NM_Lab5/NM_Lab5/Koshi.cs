using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


internal class Koshi
{
    public double A;
    public double B;
    public double C;
    public double yC;
    public double hMin;
    public double eps;
    public double y;
    public double y1, y2;
    public double k1, k2, k3, k4, k5, k6;
    public double h;

    public int dotsCount = 0;
    public int badDotsCount = 0;
    public int minDotsCount = 0;

    public Koshi() { }

    public void InputFromFile(string path)
    {
        using (StreamReader reader = new StreamReader("E:\\Лабы\\ЧМ\\NM_Lab5\\NM_Lab5\\" + path))
        {
            string[] line1 = reader.ReadLine().Split(' ');
            A = double.Parse(line1[0]);
            B = double.Parse(line1[1]);
            C = double.Parse(line1[2]);
            yC = double.Parse(line1[3]);
            string[] line2 = reader.ReadLine().Split(' ');
            hMin = double.Parse(line2[0]);
            eps = double.Parse(line2[1]);
            y = yC;
            h = Math.Abs(B - A) / 10;
        }
    }

    public static double func_calc(double x, double y)
    {
        //return 3 * x + 2 * y - 4 * x * x;
        //return 2 * x + y - x * x;
        return 2 * x + y - x*x;
        //return 2 * x * x;
    }

    public void calculateElementsPositive(double x)
    {
        k1 = h * func_calc(x, y);
        k2 = h * func_calc(x + h / 2, y + k1 / 2);
        k3 = h * func_calc(x + h / 2, y + k1 / 4 + k2 / 4);
        k4 = h * func_calc(x + h, y - k2 + 2 * k3);
        k5 = h * func_calc(x + 2 * h / 3, y + 7 * k1 / 27 + 10 * k2 / 27 + k4 / 27);
        k6 = h * func_calc(x + h * h / 5, y - (28 * k1 - 125 * k2 + 546 * k3 + 54 * k4 + 378 * k5) / 625);
        y1 = y + (k1 + 4 * k3 + k4) / 6;
        y2 = y + (14 * k1 + 35 * k4 + 162 * k5 + 125 * k6)/336;
    }

    public void calculateElementsNegative(double x)
    {
        k1 = h * func_calc(x, y);
        k2 = h * func_calc(x - h / 2, y - k1 / 2);
        k3 = h * func_calc(x - h / 2, y - k1 / 4 - k2 / 4);
        k4 = h * func_calc(x - h, y + k2 - 2 * k3);
        k5 = h * func_calc(x - 2 * h / 3, y - 7 * k1 / 27 - 10 * k2 / 27 - k4 / 27);
        k6 = h * func_calc(x - h * h / 5, y + (28 * k1 - 125 * k2 + 546 * k3 + 54 * k4 + 378 * k5) / 625);
        y1 = y - (k1 + 4 * k3 + k4) / 6;
        y2 = y - (14 * k1 + 35 * k4 + 162 * k5 + 125 * k6)/336;
    }

    public bool Condition()
    {
        return (-42 * k1 - 224 * k3 - 21 * k4 + 162 * k5 + 125 * k6) / 336 > eps;
    }

    public void PrintStep(double x)
    {
        Console.WriteLine("X: " + x + "; Y: " + y + "; E: " + ((-42 * k1 - 224 * k3 - 21 * k4 + 162 * k5 + 125 * k6) / 336) + "; H: " + h);
    }

    public void PrintPointsStats()
    {
        Console.WriteLine("Число точек интегрирования: " + dotsCount + "; Точность не достигается: " + badDotsCount + "; Минимальные шаги: " + minDotsCount);
    }

    public void PrintInitial()
    {
        Console.WriteLine("A: " + A + "; B: " + B + "; C: " + C + "; yC: " + yC);
    }
}

