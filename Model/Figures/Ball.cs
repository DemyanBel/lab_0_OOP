using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Figures
{
    [Serializable]
    /// <summary>
    /// Класс шар
    /// </summary>
    public class Ball : FigureBase
    {
        /// <summary>
        /// Радиус шара
        /// </summary>
        private double _radiusOfFigure;

        /// <summary>
        /// Свойство - радиус шара
        /// </summary>
        public double Radius
        {
            get
            {
                return _radiusOfFigure;
            }
            set
            {
                CheckingNumber(value);
                _radiusOfFigure = value;
            }
        }

        /// <summary>
        /// Тип фигуры
        /// </summary>
        public override string FigureType => "Шар";


        /// <summary>
        /// Вычисление объёма шара
        /// </summary>
        /// <retutns>Объём шара</retutns>

        public override double Volume
        {
            get
            {
                return (4.0 / 3) * Math.PI * Math.Pow(Radius, 3);
            }
        }
    }
}
