using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChartSeriesBinding
{
    public class Model
    {
        public string XValue { get; set; }

        public double YValue { get; set; }

        public Model(string xValue, double yValue)
        {
            XValue = xValue;
            YValue = yValue;
        }
    }
}
