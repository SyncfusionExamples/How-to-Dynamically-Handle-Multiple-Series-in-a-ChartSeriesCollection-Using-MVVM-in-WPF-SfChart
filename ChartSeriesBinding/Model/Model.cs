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
