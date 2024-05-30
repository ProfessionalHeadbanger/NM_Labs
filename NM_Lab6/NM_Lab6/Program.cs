class Program
{
    public static void Main()
    {
        

        bool menu = true;
        while (menu)
        {
            Console.WriteLine("1) Ввести данные с файла; \n2) Выход");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.WriteLine("Введите название файла: ");
                    string path = Console.ReadLine();
                    BoundaryTask task = new BoundaryTask(path);
                    task.calculateUVW();
                    task.calculateABG();
                    task.CalculateSystem();
                    task.OutputToConsole();
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