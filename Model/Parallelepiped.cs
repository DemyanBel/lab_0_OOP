using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Figures
{
    /// <summary>
    /// Параллелепипед
    /// </summary>
    public class Parallelepiped : FigureBase
    {
        /// <summary>
        /// Длина
        /// </summary>
        private double _length;

        /// <summary>
        /// Ширина
        /// </summary>
        private double _width;

        /// <summary>
        /// Высота
        /// </summary>
        private double _height;

        /// <summary>
        /// Длина
        /// </summary>
        public double Length
        {
            get
            {
                return _length;
            }
            set
            {
                CheckNumber(value);
                _length = value;
            }
        }

        /// <summary>
        /// Ширина
        /// </summary>
        public double Width
        {
            get
            {
                return _width;
            }
            set
            {
                CheckNumber(value);
                _width = value;
            }
        }

        /// <summary>
        /// Высота
        /// </summary>
        public double Height
        {
            get
            {
                return _height;
            }
            set
            {
                CheckNumber(value);
                _height = value;
            }
        }

        /// <summary>
        /// Вычисление объёма параллелепипеда
        /// </summary>
        /// <retutns>Объём ящика</retutns>
        public override double Volume
        {
            get
            {
                return Length * Width * Height;
            }
        }
    }
}
