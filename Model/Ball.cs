using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    /// <summary>
    /// Шар
    /// </summary>
    public class Ball : FigureBase
    {

        /// <summary>
        /// Радиус шара
        /// </summary>
        private double _radiusBall;

        /// <summary>
        /// Свойство - радиус шара
        /// </summary>
        public double Radius
        {
            get
            {
                return _radiusBall;
            }
            set
            {
                CheckNumber(value);
                _radiusBall = value;
            }
        }

        /// <summary>
        /// Свойство - вычисление объёма шара
        /// </summary>
        /// <retutns>Объём шара</retutns>

        public override double Volume
        {
            get
            {
                return (4.0 / 3.0) * Math.PI * Math.Pow(Radius, 3);
            }
        }
    }
}
