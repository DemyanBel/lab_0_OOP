using System;
using System.Collections.Generic;
using Model;

namespace ConsoleLoader
{
    /// <summary>
    /// Добавление фигур с консоли
    /// </summary>
    public static class AddConsoleFigure
    {
        /// <summary>
        /// Ввод данных о размерах параллелепипеда
        /// </summary>
        /// <returns>Экземпляр класса параллелепипед</returns>
        public static Parallelepiped GetNewParallelepipedFromKeyboard()
        {
            var parallelepiped = new Parallelepiped();
            var actions = new List<Action>()
            {
                new Action(() =>
                {
                    string parameter = "Длина";
                    Console.WriteLine($"{parameter} параллелепипеда, м: ");
                    parallelepiped.Length = ReadFromConsoleAndParse();
                }),
                new Action(() =>
                {
                    string parameter = "Ширина";
                    Console.WriteLine($"{parameter} параллелепипеда, м: ");
                    parallelepiped.Width = ReadFromConsoleAndParse();
                }),
                new Action(() =>
                {
                    string parameter = "Высота";
                    Console.WriteLine($"{parameter} параллелепипеда, м: ");
                    parallelepiped.Height = ReadFromConsoleAndParse();
                })
            };
            actions.ForEach(SetValue);
            return parallelepiped;
        }

        /// <summary>
        /// Ввод данных о пирамиде
        /// </summary>
        /// <returns>Экземпляр класса пирамида</returns>
        public static Pyramid GetNewPyramidFromKeyboard()
        {
            var pyramid = new Pyramid();
            var actions = new List<Action>()
            {
                new Action(() =>
                {
                    string parameter = "Площадь основания";
                    Console.WriteLine($"{parameter} пирамиды, м: ");
                    pyramid.BaseArea =
                        ReadFromConsoleAndParse();
                }),
                new Action(() =>
                {
                    string parameter = "Высота";
                    Console.WriteLine($"{parameter} пирамиды, м: ");
                    pyramid.Height =
                        ReadFromConsoleAndParse();
                }),
            };
            actions.ForEach(SetValue);
            return pyramid;
        }

        /// <summary>
        /// Ввод данных о шаре
        /// </summary>
        /// <returns>Экземпляр класса шар</returns>
        public static Ball GetNewBallFromKeyboard()
        {
            var ball = new Ball();
            var actions = new List<Action>()
            {
                new Action(() =>
                {
                    Console.WriteLine("Радиус шара, м: ");
                    ball.Radius =
                        ReadFromConsoleAndParse();
                }),
            };
            actions.ForEach(SetValue);
            return ball;
        }

        /// <summary>
        /// Чтение с консоли и преобразование в double
        /// </summary>
        public static double ReadFromConsoleAndParse()
        {
            return double.Parse(Console.ReadLine().Replace('.', ','));
        }

        // <summary>
        /// Получение пользовательского ввода
        /// и задание параметра
        /// </summary>
        public static void SetValue(Action action)
        {
            while (true)
            {
                try
                {
                    action.Invoke();
                    return;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"\n{e.Message}\n");
                }
            }
        }


    }
}
