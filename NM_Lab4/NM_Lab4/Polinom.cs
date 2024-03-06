using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


class Polinom
{
    public decimal[] coefficients = new decimal[4];

    public Polinom(decimal[] XValues, decimal[] YValues)
    {
        decimal[,] system = new decimal[4, 4];
        for (int col = 0; col < 4; col++)
        {
            for (int row = 0; row < 4; row++)
            {
                if (col == 0)
                {
                    system[row, col] = 1;
                }
                else if (col == 1)
                {
                    system[row, col] = XValues[row];
                }
                else if (col == 2)
                {
                    system[row, col] = XValues[row] * XValues[row];
                }
                else if (col == 3)
                {
                    system[row, col] = XValues[row] * XValues[row] * XValues[row];
                }
            }
        }

        decimal[] y_col = new decimal[4];
        for (int i = 0; i < 4; i++)
        {
            y_col[i] = YValues[i];
        }

        for (int row = 0; row < 3; row++)
        {
            DivideLine(row, system, y_col);

            for (int subrow = row + 1; subrow < 4; subrow++)
            {
                Subtract(row, subrow, system, y_col);
            }
        }

        for (int row = 3; row > 0; row--)
        {
            DivideLine(row, system, y_col);

            for (int subrow = row - 1; subrow >= 0; subrow--)
            {
                Subtract(row, subrow, system, y_col);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            coefficients[i] = y_col[i];
        }
    }

    public static bool ArePolysEqual(Polinom poly1, Polinom poly2)
    {
        for (int coef = 0; coef < 4; coef++)
        {
            if (poly1.coefficients[coef] != poly2.coefficients[coef])
            {
                return false;
            }
        }
        return true;
    }

    public static void DivideLine(int rowIndex, decimal[,] matrix, decimal[] y_col)
    {
        decimal b_element = matrix[rowIndex, rowIndex];
        for (int col = 0; col < 4; col++)
        {
            matrix[rowIndex, col] /= b_element;
        }
        y_col[rowIndex] /= b_element;
    }

    public static void Subtract(int firstRow, int secondRow, decimal[,] matrix, decimal[] y_col) // из второй вычитается первая, умноженная на коэффициент во второй
    {
        decimal koef = matrix[secondRow, firstRow];
        for (int col = 0; col < 4; col++)
        {
            matrix[secondRow, col] -= matrix[firstRow, col] * koef;
        }
        y_col[secondRow] -= y_col[firstRow] * koef;
    }

    public bool ArePolinomsEqual(Polinom other)
    {
        return true;
    }

    public void OutputPolinomToConsole()
    {
        Console.WriteLine($"P = {coefficients[0]:f4} + {coefficients[1]:f4}x + {coefficients[2]:f4}x^2 + {coefficients[3]:f4}x^3");
    }
}

