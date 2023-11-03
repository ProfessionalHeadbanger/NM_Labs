using System;
using System.Net.Http.Headers;

class Program
{
    static void Main()
    {
        int n = 12;
        int k = n/2 + 1;

        Matrix matrix = new Matrix(n, k);
        matrix.PrintMatrix();
    }
}