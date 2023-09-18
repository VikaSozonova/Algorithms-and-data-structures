// нахождение определителя матрицы

using System.Diagnostics;

Int32[,] matrix;

static Int32 Determinant(Int32[,] matrix)
{
    int len = matrix.GetLength(0);
    if (len == 1)
        return matrix[0, 0];
    Int32 temp = 0;
    Int32[,] sMatrix = new Int32[len - 1, len - 1];
    for (int i = 0; i < len; i++)
    {
        for (int j = 0; j < len - 1; j++)
        {
            for (int k = 0; k < i; k++)
            {
                sMatrix[j, k] = matrix[j + 1, k];

            }
            for (int k = i; k < len - 1; k++)
            {
                sMatrix[j, k] = matrix[j + 1, k + 1];
            }
        }
        if (i % 2 == 0)
            temp += matrix[0, i] * Determinant(sMatrix);
        else
            temp -= matrix[0, i] * Determinant(sMatrix);
    }
    return temp;
}

static Int32[,] RandomInit(Int32[,] matrix)
{
    Random rnd = new Random();
    for (int i = 0; i <  matrix.GetLength(0); i++)
        for (int j = 0; j < matrix.GetLength(0);  j++)
            matrix[i, j] = rnd.Next(-100, 100);
    return matrix;
}

Stopwatch time;
ConsoleKey temp = ConsoleKey.Spacebar;

do
{
    bool isTrue = false;
    int len = 0;
    while (!isTrue)
    {
        Console.Write("Введите размерность квадратной матрицы: ");
        string? str = Console.ReadLine();
        isTrue = Int32.TryParse(str, out len);
        if (!isTrue)
            Console.WriteLine("Введено не число\n");
        if (len < 1 && isTrue)
        {
            Console.WriteLine("У матрицы такого порядка не может быть определителя\n");
            isTrue = false;
        }
    }
    matrix = new Int32[len, len];
    bool isInit = false;
    do
    {
        int i;
        isInit = true;
        Console.Clear();
        Console.WriteLine("   Сгенерировать матрицу вручную\n" +
            "   Сгенерировать матрицу рандомно");
        Checker(out i, 2);
        if (i == 0)
            for (int k = 0; k < len; i++)
                for (int j = 0; j < len; j++)
                {
                    isTrue = false;
                    while (!isTrue)
                    {
                        Console.Write($"Введите элемент {k + 1} строки {j + 1} столбца: ");
                        string? str = Console.ReadLine();
                        isTrue = Int32.TryParse(str, out matrix[k, j]);
                        if (!isTrue)
                            Console.WriteLine("Введено не число\n");
                    }
                }
        if (i == 1)
            matrix = RandomInit(matrix);
        else
        {
            Console.WriteLine("Неверный ввод");
            isInit = false;
        }
    } while (!isInit);

    time = Stopwatch.StartNew();
    time.Restart();
    Int32 det = Determinant(matrix);
    time.Stop();


    Console.WriteLine("\nОпределитель матрицы");
    for (int i = 0; i < len; i++)
    {
        for (int j = 0; j < len - 1; j++)
            Console.Write(matrix[i, j] + " ");
        Console.WriteLine(matrix[i, len - 1]);
    }
    Console.WriteLine($"Равен {det} и был найден за {time.ElapsedTicks} тиков\n");

    Console.WriteLine("\nНажмите любую клавишу для продолжения или Escape для выхода\n");
    temp = Console.ReadKey(true).Key;
} while (temp != ConsoleKey.Escape);

static void Checker(out int numberOfFunction, int count, int startPos = 0, string arrow = "->")
{
    string empty = new string(' ', arrow.Length);
    int i = startPos;
    numberOfFunction = -1;
    Console.SetCursorPosition(0, startPos);
    Console.Write(arrow);
    ConsoleKey key;
    do
    {
        key = Console.ReadKey(true).Key;
        switch (key)
        {
            case ConsoleKey.DownArrow:
                Console.SetCursorPosition(0, i);
                Console.Write(empty);
                if (i == count + startPos - 1)
                    i = startPos - 1;
                Console.SetCursorPosition(0, ++i);
                Console.Write(arrow);
                break;
            case ConsoleKey.UpArrow:
                Console.SetCursorPosition(0, i);
                Console.Write(empty);
                if (i == startPos)
                    i = count + startPos;
                Console.SetCursorPosition(0, --i);
                Console.Write(arrow);
                break;
            case ConsoleKey.Enter:
                Console.Clear();
                numberOfFunction = i - startPos;
                break;
        }
    } while (numberOfFunction == -1);
}