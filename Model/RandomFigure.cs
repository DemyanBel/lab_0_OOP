using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Figures;
using Model;

namespace Model
{
    /// <summary>
    /// Класс для генерации случайной фигуры
    /// </summary>
    public static class RandomFigure
    {
        /// <summary>
        /// Рандом
        /// </summary>
        private static Random _random = new Random();

        /// <summary>
        /// Максимальное значение параметра
        /// </summary>
        private const int MAXVALUE = 10000;

        /// <summary>
        /// Минимальное значение параметра
        /// </summary>
        private const int MINVALUE = 0;

        /// <summary>
        /// Значение делителя 
        /// </summary>
        private const double DIVIDER = 10000.0;

        /// <summary>
        /// Генерация случайного числа double через int
        /// </summary>
        /// <param name="minValue">Минимальное значение</param>
        /// <param name="maxValue">Максимальное значение</param>
        /// <param name="divider">Делитель</param>
        public static double GetRandomDouble(int minValue, int maxValue,
            double divider)
        {
            var randomValue = Convert.ToDouble(_random.Next(minValue, maxValue));
            return randomValue / divider;
        }

        /// <summary>
        /// Генерация случайной фигуры
        /// </summary>
        /// <returns>Сгенерированный объект класса FigureBase</returns>
        public static FigureBase GetRandomFigure()
        {
            var figureType = _random.Next(0, 3);

            switch (figureType)
            {
                case 0:
                    {
                        return GetRandomParallelepiped();
                    }
                case 1:
                    {
                        return GetRandomPyramid();
                    }
                case 2:
                    {
                        return GetRandomBall();
                    }
                default:
                    {
                        throw new ArgumentException("Тип фигуры отсутствует.");
                    }
            }
        }

        /// <summary>
        /// Генерация случайного параллелепипеда
        /// </summary>
        /// <returns>Случайный параллелепипед</returns>
        public static FigureBase GetRandomParallelepiped()
        {
            var parallelepiped = new Parallelepiped
            {
                Length = GetRandomDouble(MINVALUE, MAXVALUE, DIVIDER),
                Width = GetRandomDouble(MINVALUE, MAXVALUE, DIVIDER),
                Height = GetRandomDouble(MINVALUE, MAXVALUE, DIVIDER)
            };
            return parallelepiped;
        }

        /// <summary>
        /// Генерация случайной пирамиды
        /// </summary>
        /// <returns>Случайная пирамида</returns>
        public static FigureBase GetRandomPyramid()
        {
            var pyramid = new Pyramid
            {
                Length = GetRandomDouble(MINVALUE, MAXVALUE, DIVIDER),
                Width = GetRandomDouble(MINVALUE, MAXVALUE, DIVIDER),
                Height = GetRandomDouble(MINVALUE, MAXVALUE, DIVIDER)
            };
            return pyramid;
        }

        /// <summary>
        /// Генерация случайного шара
        /// </summary>
        /// <returns>Случайный шар</returns>
        public static FigureBase GetRandomBall()
        {
            var ball = new Ball
            {
                Radius = GetRandomDouble(MINVALUE, MAXVALUE, DIVIDER),
            };
            return ball;
        }
    }
}
