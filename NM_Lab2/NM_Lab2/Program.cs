



class Program
{ 
    public static void DirectStroke(Matrix m)
    {
        for (int row = 0; row < m.N; row++)
        {
            m.DivideLine(row);
            
        }
    }
    static void Main()
    {
        Matrix generated = new Matrix(10, 3);
        generated.Generate(-10, 10);
    }
}
