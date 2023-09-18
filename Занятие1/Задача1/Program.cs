//решения задача Фибоначчи

using System.Diagnostics;
using System.Runtime.ExceptionServices;

static Int32 FibRec(Int32 a)
{
    if (a == 1 || a == 2)
        return 1;
    else
        return FibRec(a - 1) + FibRec(a - 2);
}

static Int32 FibIt1(Int32 a)
{
    if (a == 1 || a == 2)
        return 1;
    else
    {
        Int32 first = 1;
        Int32 second = 1;
        for (int i = 3; i <= a; i++)
        {
            second = first + second;
            first = second - first;
        }
        return second;
    }
}

static Int32 FibIt2(Int32 a, ref Int32[] fibs)
{
    if (a <= fibs.Length)
        return fibs[a - 1];
    Int32[] temp = new Int32[fibs.Length * 2];
    for (int i = 0; i < fibs.Length; i++)
        temp[i] = fibs[i];
    fibs = temp;
    for (int i = fibs.Length / 2; i < fibs.Length; i++)
        fibs[i] = fibs[i - 1] + fibs[i - 2];
    return fibs[a - 1];
}

ConsoleKey temp = ConsoleKey.Spacebar;
Int32[] fibs = new Int32[2];
fibs[0] = 1;
fibs[1] = 1;
Stopwatch time;

do
{
    bool isTrue = false;
    int a = 0;
    while (!isTrue)
    {
        Console.Write("Введите порядковый номер искомого числа Фибоначчи: ");
        string? str = Console.ReadLine();
        isTrue = Int32.TryParse(str, out a);
        if (!isTrue)
            Console.WriteLine("Введено не число\n");
        if (a < 1 && isTrue)
        {
            Console.WriteLine("Введенное число не может быть порядковым номером\n");
            isTrue = false;
        }
    }
    Int32 res;

    time = Stopwatch.StartNew();
    time.Restart();
    res = FibRec(a);
    time.Stop();

    Console.WriteLine($"Число Фибоначчи под номером {a} = {res} Было найдено за {time.ElapsedTicks} тиков рекурсивным способом");

    time = Stopwatch.StartNew();
    time.Restart();
    res = FibIt1(a);
    time.Stop();

    Console.WriteLine($"Число Фибоначчи под номером {a} = {res} Было найдено за {time.ElapsedTicks} тиков иттеративным способом");

    time = Stopwatch.StartNew();
    time.Restart();
    res = FibIt2(a, ref fibs);
    time.Stop();

    Console.WriteLine($"Число Фибоначчи под номером {a} = {res} Было найдено за {time.ElapsedTicks} тиков иттеративным способом с массивом");

    Console.WriteLine("\nНажмите любую клавишу для продолжения или Escape для выхода\n");
    temp = Console.ReadKey(true).Key;
} while (temp != ConsoleKey.Escape);