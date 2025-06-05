using System;
using Model;

namespace ConsoleLoader
{
    /// <summary>
    /// Класс для тестирования библиотеки классов Model
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Точка входа в программу
        /// </summary>
        /// <param name="args">Параметры</param>
        public static void Main(string[] args)
        {
            Console.WriteLine("Добро пожаловать в программу" +
                " для вычисления объемов фигур!\n\n" +
        "Нажмите любую кнопку, чтобы начать...");
            Console.ReadKey();

            while (true)
            {
                Console.WriteLine("");
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1 - Вычисление объёма пирамиды");
                Console.WriteLine("2 - Вычисление объёма параллелипипеда");
                Console.WriteLine("3 - Вычисление объёма шара");
                Console.WriteLine("4 - Выход из программы");
                var consoleKey = Console.ReadLine();
                switch (consoleKey)
                {
                    case "1":
                    {
                        GetVolumeInfo(AddConsoleFigure.
                            GetNewPyramidFromKeyboard());
                        break;
                    }
                    case "2":
                    {
                        GetVolumeInfo(AddConsoleFigure.
                            GetNewParallelepipedFromKeyboard());
                        break;
                    }
                    case "3":
                    {
                        GetVolumeInfo(AddConsoleFigure.
                            GetNewBallFromKeyboard());
                        break;
                    }
                    case "4":
                    {
                        Environment.Exit(0);
                        break;
                    }
                    default:
                    {
                        Console.WriteLine("Программа не ожидает такого ответа." +
                            "Ожидается целое число от 1 до 4.");
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Метод вывода информации в консоль
        /// </summary>
        /// <param name="figure">Экземпляр класса Фигура</param>
        public static void GetVolumeInfo(FigureBase figure)
        {
            Console.WriteLine($"Объем фигуры равен " +
                $"{figure.Volume} м^3.");
        }
    }
}
