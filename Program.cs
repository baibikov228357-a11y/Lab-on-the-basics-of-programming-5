using System;
using System.Diagnostics;

/// <summary>
/// Предоставляет методы для валидации пользовательского ввода
/// </summary>
public static class InputValidator
{
    /// <summary>
    /// Валидирует ввод положительного целого числа
    /// </summary>
    /// <param name="message">Сообщение для пользователя</param>
    /// <param name="maxValue">Максимальное допустимое значение</param>
    /// <returns>Валидное положительное целое число</returns>
    public static int ValidatePositiveInt(string message, int maxValue = int.MaxValue)
    {
        int number;
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out number) && number > 0 && number <= maxValue)
            {
                return number;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Ошибка! Введите положительное число не больше {maxValue}");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Валидирует ввод числа double, не равного нулю
    /// </summary>
    /// <param name="message">Сообщение для пользователя</param>
    /// <returns>Валидное число double, не равное нулю</returns>
    public static double ValidateNonZeroDouble(string message)
    {
        double number;
        while (true)
        {
            Console.Write(message);
            if (double.TryParse(Console.ReadLine(), out number) && number != 0)
            {
                return number;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка! Введите число, не равное нулю");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Валидирует ввод положительного числа double
    /// </summary>
    /// <param name="message">Сообщение для пользователя</param>
    /// <returns>Валидное положительное число double</returns>
    public static double ValidatePositiveDouble(string message)
    {
        double number;
        while (true)
        {
            Console.Write(message);
            if (double.TryParse(Console.ReadLine(), out number) && number > 0)
            {
                return number;
            }
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка! Введите положительное число");
            Console.ResetColor();
        }
    }

    /// <summary>
    /// Валидирует ввод ответа да/нет
    /// </summary>
    /// <param name="message">Сообщение для пользователя</param>
    /// <returns>true для 'y', false для 'n'</returns>
    public static bool ValidateYesNo(string message)
    {
        while (true)
        {
            Console.Write(message);
            string answer = Console.ReadLine()?.ToLower();
            if (answer == "y") return true;
            if (answer == "n") return false;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка! Введите 'y' или 'n'");
            Console.ResetColor();
        }
    }
}

/// <summary>
/// Реализует игру "Угадай ответ" с математической функцией
/// </summary>
public static class GuessGame
{
    /// <summary>
    /// Вычисляет значение функции f = ln^2(b)/cos(a) - 1
    /// </summary>
    /// <param name="a">Параметр A функции</param>
    /// <param name="b">Параметр B функции</param>
    /// <returns>Результат вычисления функции, округленный до 2 знаков</returns>
    public static double CalculateFunction(double a, double b)
    {
        double result = Math.Round(Math.Pow(Math.Log(b), 2) / Math.Cos(a) - 1, 2);
        return result;
    }

    /// <summary>
    /// Запускает игровую сессию "Угадай ответ"
    /// </summary>
    public static void Play()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Дана функция: f = ln^2(b)/cos(a) - 1");
        Console.WriteLine("Введите a и b и угадайте ответ.");
        Console.ResetColor();
        Console.WriteLine();

        double a = InputValidator.ValidateNonZeroDouble("Введите значение A: ");
        double b = InputValidator.ValidatePositiveDouble("Введите значение B: ");

        bool won = CheckAnswer(a, b);

        Console.WriteLine("Нажмите любую клавишу для возврата в меню.");
        Console.ReadKey();
    }

    /// <summary>
    /// Проверяет ответ пользователя и предоставляет ограниченное количество попыток
    /// </summary>
    /// <param name="a">Параметр A функции</param>
    /// <param name="b">Параметр B функции</param>
    /// <returns>true если ответ верный, false если все попытки исчерпаны</returns>
    private static bool CheckAnswer(double a, double b)
    {
        const int maxAttempts = 3;
        int attemptsLeft = maxAttempts;
        double correctAnswer = CalculateFunction(a, b);

        while (attemptsLeft > 0)
        {
            Console.Write($"Введите вашу догадку (осталось попыток: {attemptsLeft}): ");
            string input = Console.ReadLine();

            if (!double.TryParse(input, out double userAnswer))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Неверный ввод. Пожалуйста, введите числовое значение.");
                Console.ResetColor();
                attemptsLeft++;
            }

            userAnswer = Math.Round(userAnswer, 2);

            if (userAnswer == correctAnswer)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Ответ верный! Вы выиграли!");
                Console.ResetColor();
                return true;
            }
            else
            {
                attemptsLeft--;
                Console.ForegroundColor = ConsoleColor.Red;
                if (attemptsLeft > 0)
                {
                    Console.WriteLine("Ответ неверный. Попробуйте снова.");
                }
                Console.ResetColor();
            }
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Вы проиграли. Правильный ответ: {correctAnswer}");
        Console.ResetColor();
        return false;
    }
}

/// <summary>
/// Обрабатывает массивы, выполняет сортировку и сравнивает время выполнения алгоритмов
/// </summary>
public class ArrayProcessor
{
    private int length;
    private int[] array;

    /// <summary>
    /// Конструктор по умолчанию, создает массив длиной 10 элементов
    /// </summary>
    public ArrayProcessor()
    {
        length = 10;
        array = CreateArray(length);
    }

    /// <summary>
    /// Конструктор с параметром для создания массива заданной длины
    /// </summary>
    /// <param name="arrayLength">Длина создаваемого массива</param>
    public ArrayProcessor(int arrayLength)
    {
        length = arrayLength;
        array = CreateArray(length);
    }

    /// <summary>
    /// Выполняет обработку массива: сортировку пузырьком и выбором с измерением времени
    /// </summary>
    public void ProcessArrays()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        Console.Write("Массив до сортировки:\t");
        PrintArray(array);

        int[] bubbleArray = CopyArray(array);
        int[] selectionArray = CopyArray(array);

        Stopwatch timerBubble = Stopwatch.StartNew();
        BubbleSort(bubbleArray);
        timerBubble.Stop();

        Console.Write("\nМассив после сортировки пузырьком:\t");
        PrintArray(bubbleArray);

        Stopwatch timerSelection = Stopwatch.StartNew();
        SelectionSort(selectionArray);
        timerSelection.Stop();

        Console.Write("Массив после сортировки выбором:\t");
        PrintArray(selectionArray);

        Console.WriteLine($"\nВремя сортировки пузырьком: {timerBubble.Elapsed.TotalMilliseconds:F4} мс");
        Console.WriteLine($"Время сортировки выбором: {timerSelection.Elapsed.TotalMilliseconds:F4} мс");
        Console.ResetColor();

        Console.WriteLine("\n-------------------------------------------------------");
        if (timerBubble.Elapsed.TotalMilliseconds < timerSelection.Elapsed.TotalMilliseconds)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Скорость сортировки пузырьком быстрее чем выбором.");
            Console.ResetColor();
        }
        else if (timerSelection.Elapsed.TotalMilliseconds < timerBubble.Elapsed.TotalMilliseconds)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Скорость сортировки выбором быстрее чем пузырьком.");
            Console.ResetColor();
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Скорость выполнения двух сортировок одинакова");
            Console.ResetColor();
        }
        Console.WriteLine("-------------------------------------------------------");

        Console.WriteLine("\nДля выхода в меню нажмите Enter");
        Console.ReadLine();
    }

    private int[] CreateArray(int length)
    {
        int[] arr = new int[length];
        Random rd = new Random();
        for (int i = 0; i < length; i++)
        {
            arr[i] = rd.Next(100);
        }
        return arr;
    }

    private void PrintArray(int[] arr)
    {
        Console.WriteLine("{" + string.Join(", ", arr) + "}");
    }

    private int[] BubbleSort(int[] arr)
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int j = 0; j < arr.Length - 1 - i; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
        return arr;
    }

    private int[] SelectionSort(int[] arr)
    {
        for (int i = 0; i < arr.Length; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[j] < arr[minIndex])
                {
                    minIndex = j;
                }
            }
            if (minIndex != i)
            {
                int temp = arr[i];
                arr[i] = arr[minIndex];
                arr[minIndex] = temp;
            }
        }
        return arr;
    }

    private int[] CopyArray(int[] arr)
    {
        int[] newArray = new int[arr.Length];
        for (int i = 0; i < arr.Length; i++)
        {
            newArray[i] = arr[i];
        }
        return newArray;
    }
}

/// <summary>
/// Реализует классическую игру "Сапёр" с полем 5x5 и 5 минами
/// </summary>
public class MinesweeperGame
{
    private const int FIELD_SIZE = 5;
    private const int MINES_COUNT = 5;
    private const char HIDDEN_CELL = '#';
    private const char MINE_CELL = '*';
    private const char EMPTY_CELL = ' ';

    private char[,] gameField;
    private bool[,] mines;
    private bool[,] revealed;
    private int revealedSafeCells;
    private int safeCellsCount;

    /// <summary>
    /// Инициализирует новую игровую сессию Сапёра
    /// </summary>
    public MinesweeperGame()
    {
        gameField = InitializeGameField();
        mines = PlaceMines();
        revealed = new bool[FIELD_SIZE, FIELD_SIZE];
        safeCellsCount = FIELD_SIZE * FIELD_SIZE - MINES_COUNT;
        revealedSafeCells = 0;
    }

    /// <summary>
    /// Запускает игровую сессию Сапёра
    /// </summary>
    /// <returns>true если пользователь хочет играть снова, false если нет</returns>
    public bool RunGameSession()
    {
        bool gameOver = false;
        bool playerWon = false;

        DisplayGameRules();

        while (!gameOver && !playerWon)
        {
            Console.Clear();
            DisplayGameField(false);
            Console.WriteLine($"\nОткрыто безопасных клеток: {revealedSafeCells}/{safeCellsCount}");

            string input = GetPlayerInput();
            if (input == "q")
            {
                Console.WriteLine("Игра завершена!");
                return false;
            }

            int number = int.Parse(input);
            int row = number / 10 - 1;
            int col = number % 10 - 1;

            if (revealed[row, col])
            {
                Console.WriteLine("Эта клетка уже открыта! Нажмите любую клавишу...");
                Console.ReadKey();
                continue;
            }

            if (mines[row, col])
            {
                gameOver = true;
                Console.Clear();
                DisplayGameField(true);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nБАБАХ! ВЫ ПРОИГРАЛИ!");
                Console.ResetColor();
            }
            else
            {
                RevealCell(row, col);

                if (revealedSafeCells == safeCellsCount)
                {
                    playerWon = true;
                    Console.Clear();
                    DisplayGameField(true);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nУРА! ВЫ ВЫИГРАЛИ!");
                    Console.ResetColor();
                }
            }
        }

        return AskToPlayAgain();
    }

    private char[,] InitializeGameField()
    {
        char[,] field = new char[FIELD_SIZE, FIELD_SIZE];
        for (int i = 0; i < FIELD_SIZE; i++)
        {
            for (int j = 0; j < FIELD_SIZE; j++)
            {
                field[i, j] = HIDDEN_CELL;
            }
        }
        return field;
    }

    private bool[,] PlaceMines()
    {
        bool[,] mines = new bool[FIELD_SIZE, FIELD_SIZE];
        Random rnd = new Random();
        int minesPlaced = 0;

        while (minesPlaced < MINES_COUNT)
        {
            int row = rnd.Next(FIELD_SIZE);
            int col = rnd.Next(FIELD_SIZE);

            if (!mines[row, col])
            {
                mines[row, col] = true;
                minesPlaced++;
            }
        }

        return mines;
    }

    private void DisplayGameField(bool showMines)
    {
        Console.Write("   ");
        for (int j = 1; j <= FIELD_SIZE; j++)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(j + " ");
            Console.ResetColor();
        }
        Console.WriteLine();

        for (int i = 0; i < FIELD_SIZE; i++)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"{i + 1,2} ");
            Console.ResetColor();

            for (int j = 0; j < FIELD_SIZE; j++)
            {
                if (showMines && mines[i, j])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(MINE_CELL + " ");
                    Console.ResetColor();
                }
                else if (revealed[i, j])
                {
                    if (gameField[i, j] == EMPTY_CELL)
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(gameField[i, j] + " ");
                        Console.ResetColor();
                    }
                }
                else
                {
                    Console.Write(HIDDEN_CELL + " ");
                }
            }
            Console.WriteLine();
        }
    }

    private string GetPlayerInput()
    {
        while (true)
        {
            Console.Write("\nВведите координаты (например, 11) или 'q' если хотите сдаться: ");
            string input = Console.ReadLine();

            if (input == "q") return "q";

            if (IsValidInput(input))
            {
                return input;
            }
            else
            {
                Console.WriteLine($"Неверный формат! Используйте две цифры (1-{FIELD_SIZE}).");
            }
        }
    }

    private bool IsValidInput(string input)
    {
        if (input == null || input.Length != 2)
            return false;

        if (!int.TryParse(input, out int number))
            return false;

        int row = number / 10;
        int col = number % 10;

        return row >= 1 && row <= FIELD_SIZE && col >= 1 && col <= FIELD_SIZE;
    }

    private int CountAdjacentMines(int row, int col)
    {
        int count = 0;
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                int newRow = row + i;
                int newCol = col + j;

                if (newRow >= 0 && newRow < FIELD_SIZE && newCol >= 0 && newCol < FIELD_SIZE)
                {
                    if (mines[newRow, newCol])
                        count++;
                }
            }
        }
        return count;
    }

    private void RevealCell(int row, int col)
    {
        if (row < 0 || row >= FIELD_SIZE || col < 0 || col >= FIELD_SIZE || revealed[row, col])
            return;

        revealed[row, col] = true;
        revealedSafeCells++;

        int adjacentMines = CountAdjacentMines(row, col);

        if (adjacentMines > 0)
        {
            gameField[row, col] = (char)('0' + adjacentMines);
        }
        else
        {
            gameField[row, col] = EMPTY_CELL;

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    if (i == 0 && j == 0) continue;
                    RevealCell(row + i, col + j);
                }
            }
        }
    }

    private void DisplayGameRules()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("==> ПРАВИЛА ИГРЫ 'САПЁР' <==");
        Console.ResetColor();
        Console.WriteLine($"Размер поля: {FIELD_SIZE}x{FIELD_SIZE}");
        Console.WriteLine($"Количество мин: {MINES_COUNT}");
        Console.WriteLine("Формат ввода координат: ДВЕ ЦИФРЫ (например: 11, 23, 55)");
        Console.WriteLine("Первая цифра: номер строки");
        Console.WriteLine("Вторая цифра: номер столбца");
        Console.WriteLine("Цель: открыть все безопасные клетки");
        Console.WriteLine("\nНажмите любую клавишу для начала игры...");
        Console.ReadKey();
    }

    private bool AskToPlayAgain()
    {
        return InputValidator.ValidateYesNo("\nХотите сыграть еще раз? (y/n): ");
    }
}

/// <summary>
/// Главный класс программы, содержащий точку входа и меню приложения
/// </summary>
public class Program
{
    /// <summary>
    /// Точка входа в приложение
    /// </summary>
    /// <param name="args">Аргументы командной строки</param>
    public static void Main(string[] args)
    {
        bool exitProgram = false;

        while (!exitProgram)
        {
            DisplayMenu();
            string input = Console.ReadLine();
            exitProgram = ProcessMenuInput(input);
        }
    }

    /// <summary>
    /// Отображает главное меню приложения
    /// </summary>
    private static void DisplayMenu()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("~-~-~-~-~ МЕНЮ ~-~-~-~-~-");
        Console.ResetColor();
        Console.WriteLine("| 1. Угадай ответ       |");
        Console.WriteLine("| 2. Об авторе          |");
        Console.WriteLine("| 3. Сортировка массивов|");
        Console.WriteLine("| 4. Сапёр              |");
        Console.WriteLine("| 5. Выход              |");
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("~-~-~-~-~-~-~-~-~-~-~-~-~");
        Console.ResetColor();
        Console.Write("Выберите опцию (1-5): ");
    }

    /// <summary>
    /// Обрабатывает ввод пользователя в главном меню
    /// </summary>
    /// <param name="input">Введенная пользователем опция</param>
    /// <returns>true если программа должна завершиться, false если продолжать работу</returns>
    private static bool ProcessMenuInput(string input)
    {
        switch (input)
        {
            case "1":
                GuessGame.Play();
                break;

            case "2":
                AboutTheAuthor();
                break;

            case "3":
                SortArrays();
                break;

            case "4":
                PlayMinesweeper();
                break;

            case "5":
                return ExitProgram();

            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Неверный ввод. Нажмите любую клавишу для продолжения.");
                Console.ResetColor();
                Console.ReadKey();
                break;
        }
        return false;
    }

    /// <summary>
    /// Отображает информацию об авторе программы
    /// </summary>
    static void AboutTheAuthor()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.BackgroundColor = ConsoleColor.White;
        Console.WriteLine("--------- Об авторе ---------");
        Console.WriteLine("Имя: Байбиков Ринат Рамилевич");
        Console.WriteLine("Группа: 6104-090301D         ");
        Console.WriteLine("-----------------------------");
        Console.ResetColor();
        Console.WriteLine("\nНажмите любую клавишу для выхода из меню.");
        Console.ReadKey();
    }

    /// <summary>
    /// Запускает демонстрацию работы с массивами и сортировками
    /// </summary>
    static void SortArrays()
    {
        Console.Clear();

        Console.WriteLine("-~-~-Сортировка массива-~-~-");
        Console.WriteLine("1. Конструктор с параметрами");
        Console.WriteLine("2. Конструктор без параметров");
        Console.Write("Выберите тип конструктора (1 или 2): ");

        string choice = Console.ReadLine();
        ArrayProcessor processor;

        switch (choice)
        {
            case "1":
                int length = InputValidator.ValidatePositiveInt("\nВведите длину массива (1-10): ", 10);
                processor = new ArrayProcessor(length);
                processor.ProcessArrays();
                break;

            case "2":
                processor = new ArrayProcessor();
                processor.ProcessArrays();
                break;

            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Неверный ввод. Нажмите любую клавишу для продолжения.");
                Console.ResetColor();
                Console.ReadKey();
                break;

        }
    }

    /// <summary>
    /// Запускает игру Сапёр
    /// </summary>
    static void PlayMinesweeper()
    {
        bool playAgain;
        do
        {
            MinesweeperGame game = new MinesweeperGame();
            playAgain = game.RunGameSession();
        }
        while (playAgain);
    }

    /// <summary>
    /// Обрабатывает выход из программы
    /// </summary>
    /// <returns>true если пользователь подтвердил выход, false если отменил</returns>
    static bool ExitProgram()
    {
        return InputValidator.ValidateYesNo("Вы действительно хотите выйти из программы? (y/n): ");
    }
}