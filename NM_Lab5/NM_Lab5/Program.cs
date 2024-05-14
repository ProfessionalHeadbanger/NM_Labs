

class Program
{
    public static int Calculate(Koshi koshi)
    {
        if (koshi.A == koshi.C)
        {
            double x = koshi.A;
            while (koshi.B - (x + koshi.h) >= koshi.hMin)
            {
                koshi.calculateElementsPositive(x);

                if (koshi.Condition())
                {
                    if (koshi.h == koshi.hMin)
                    {
                        koshi.y = koshi.y1;
                        x += koshi.h;
                        koshi.PrintStep(x);
                        koshi.badDotsCount++;
                        koshi.minDotsCount++;
                    }
                    else
                    {
                        koshi.h = Math.Min(koshi.h / 2, koshi.hMin);
                    }
                }
                else
                {
                    koshi.y = koshi.y1;
                    x += koshi.h;
                    koshi.PrintStep(x);
                    koshi.dotsCount++;
                    if (koshi.h == koshi.hMin)
                    {
                        koshi.minDotsCount++;
                    }
                    koshi.h *= 2;
                }
            }

            if (koshi.B - x >= 2 * koshi.hMin)
            {
                koshi.h = (koshi.B - koshi.hMin) - x;
                koshi.calculateElementsPositive(x);
                if (koshi.Condition())
                {
                    koshi.badDotsCount++;
                }
                koshi.minDotsCount += 2;
                koshi.dotsCount += 2;
                koshi.y = koshi.y1;
                x += koshi.h;
                koshi.PrintStep(x);
                koshi.h = koshi.hMin;
                koshi.calculateElementsPositive(x);
                koshi.y = koshi.y1;
                x += koshi.h;
                koshi.PrintStep(x);
                if (koshi.Condition())
                {
                    koshi.badDotsCount++;
                }
                koshi.PrintPointsStats();
                if (koshi.Condition())
                {
                    return 1;
                }
            }
            else if (koshi.B - x <= 1.5*koshi.hMin)
            {
                koshi.h = koshi.B - x;
                koshi.calculateElementsPositive(x);
                koshi.minDotsCount++;
                koshi.dotsCount++;
                koshi.y = koshi.y1;
                x += koshi.h;
                koshi.h = koshi.hMin;
                koshi.PrintStep(x);
                if (koshi.Condition())
                {
                    koshi.badDotsCount++;
                }
                koshi.PrintPointsStats();
                if (koshi.Condition())
                {
                    return 1;
                }
            }
            else
            {
                koshi.h = (koshi.B - x) / 2;
                koshi.calculateElementsPositive(x);
                if (koshi.Condition())
                {
                    koshi.badDotsCount++;
                }
                koshi.minDotsCount += 2;
                koshi.dotsCount += 2;
                koshi.y = koshi.y1;
                x += koshi.h;
                koshi.PrintStep(x);
                koshi.calculateElementsPositive(x);
                koshi.y = koshi.y1;
                x += koshi.h;
                koshi.PrintStep(x);
                if (koshi.Condition())
                {
                    koshi.badDotsCount++;
                }
                koshi.PrintPointsStats();
                if (koshi.Condition())
                {
                    return 1;
                }
            }
        }
        else
        {
            double x = koshi.B;
            while((x - koshi.h) - koshi.A >= koshi.hMin)
            {
                koshi.calculateElementsNegative(x);
                if (koshi.Condition())
                {
                    if (koshi.h == koshi.hMin)
                    {
                        koshi.y = koshi.y1;
                        x -= koshi.h;
                        koshi.PrintStep(x);
                        koshi.badDotsCount++;
                        koshi.minDotsCount++;
                    }
                    else
                    {
                        koshi.h = Math.Min(koshi.h / 2, koshi.hMin);
                    }
                }
                else
                {
                    koshi.y = koshi.y1;
                    x -= koshi.h;
                    koshi.PrintStep(x);
                    koshi.dotsCount++;
                    if (koshi.h == koshi.hMin)
                    {
                        koshi.minDotsCount++;
                    }
                    koshi.h *= 2;
                }
            }
            if (x - koshi.A >= 2*koshi.hMin)
            {
                koshi.h = x - (koshi.A + koshi.hMin);
                koshi.calculateElementsNegative(x);
                if (koshi.Condition())
                {
                    koshi.badDotsCount++;
                }
                koshi.minDotsCount += 2;
                koshi.dotsCount += 2;
                koshi.y = koshi.y1;
                x -= koshi.h;
                koshi.h = koshi.hMin;
                koshi.PrintStep(x);
                koshi.calculateElementsNegative(x);
                koshi.y = koshi.y1;
                x -= koshi.h;
                koshi.PrintStep(x);
                if (koshi.Condition())
                {
                    koshi.badDotsCount++;
                }
                koshi.PrintPointsStats();
                if (koshi.Condition())
                {
                    return 1;
                }
            }
            else if (x - koshi.A <= 1.5*koshi.hMin)
            {
                koshi.h = x - koshi.A;
                koshi.calculateElementsNegative(x);
                koshi.minDotsCount++;
                koshi.dotsCount++;
                koshi.y = koshi.y1;
                x -= koshi.h;
                koshi.h = koshi.hMin;
                koshi.PrintStep(x);
                if (koshi.Condition())
                {
                    koshi.badDotsCount++;
                }
                koshi.PrintPointsStats();
                if (koshi.Condition())
                {
                    return 1;
                }
            }
            else
            {
                koshi.h = (x - koshi.A) / 2;
                koshi.calculateElementsNegative(x);
                if (koshi.Condition())
                {
                    koshi.badDotsCount++;
                }
                koshi.minDotsCount += 2;
                koshi.dotsCount += 2;
                koshi.y = koshi.y1;
                x -= koshi.h;
                koshi.PrintStep(x);
                koshi.calculateElementsNegative(x);
                koshi.y = koshi.y1;
                x -= koshi.h;
                koshi.PrintStep(x);
                if (koshi.Condition())
                {
                    koshi.badDotsCount++;
                }
                koshi.PrintPointsStats();
                if (koshi.Condition())
                {
                    return 1;
                }
            }
        }
        return 0;
    }
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
                    Koshi koshi = new();
                    koshi.InputFromFile(path);
                    koshi.PrintInitial();
                    Console.WriteLine("IER: " + Calculate(koshi));
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