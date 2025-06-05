using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Пирамида
    /// </summary>
    public class Pyramid : FigureBase
    {
        /// <summary>
        /// Площадь основания
        /// </summary>
        private double _baseArea;

        /// <summary>
        /// Высота
        /// </summary>
        private double _height;

        /// <summary>
        /// Свойство - площадь основания пирамиды
        /// </summary>
        public double BaseArea
        {
            get
            {
                return _baseArea;
            }
            set
            {
                CheckNumber(value);
                _baseArea = value;
            }
        }

        /// <summary>
        /// Свойство - площадь основания пирамиды
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
        /// Свойство - вычисление объема пирамиды
        /// </summary>
        /// <retutns>Объём пирамиды</retutns>

        public override double Volume
        {
            get
            {
                return BaseArea * Height * 1 / 3;
            }
        }
    }
}
