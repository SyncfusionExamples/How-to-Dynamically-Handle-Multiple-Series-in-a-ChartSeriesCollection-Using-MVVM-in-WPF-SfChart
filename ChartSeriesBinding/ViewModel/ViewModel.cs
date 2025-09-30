using Syncfusion.UI.Xaml.Charts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChartSeriesBinding
{
    public class ViewModel : INotifyPropertyChanged
    {
        private ChartSeriesCollection? series;

        private List<SplineSeries> allSplineSeries;

        public ChartSeriesCollection? Series
        {
            get => series;
            set
            {
                series = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Series)));
            }
        }

        public ObservableCollection<Model> DataSource1 { get; set; }
        public ObservableCollection<Model> DataSource2 { get; set; }
        public ObservableCollection<Model> DataSource3 { get; set; }
        public ObservableCollection<Model> DataSource4 { get; set; }
        public SplineSeries? SplineSeries1 { get; set; }
        public SplineSeries? SplineSeries2 { get; set; }
        public SplineSeries? SplineSeries3 { get; set; }
        public SplineSeries? SplineSeries4 { get; set; }

        public ViewModel()
        {
            DataSource1 = new ObservableCollection<Model>
            {
                new Model("Benz", 2),
                new Model("Volvo", 35),
                new Model("Jaguar", 30),
                new Model("BMW", 15),
                new Model("Toyota",10),
                new Model("Audi", 5)
            };

            DataSource2 = new ObservableCollection<Model>
            {
                new Model("Benz", 12),
                new Model("Volvo", 30),
                new Model("Jaguar", 28),
                new Model("BMW", 35),
                new Model("Toyota", 15),
                new Model("Audi", 10)
            };

            DataSource3 = new ObservableCollection<Model>
            {
                new Model("Benz", 25),
                new Model("Volvo", 10),
                new Model("Jaguar", 20),
                new Model("BMW", 10),
                new Model("Toyota", 35),
                new Model("Audi", 30)
            };

            DataSource4 = new ObservableCollection<Model>
            {
                new Model("Benz", 35),
                new Model("Volvo",44),
                new Model("Jaguar", 40),
                new Model("BMW", 44),
                new Model("Toyota", 30),
                new Model("Audi", 40)
            };

            SplineSeries1 = new SplineSeries
            {
                ItemsSource = DataSource1,
                XBindingPath = "XValue",
                YBindingPath = "YValue",
                LegendIcon= ChartLegendIcon.Circle,
                Label = "Q1"
            };

            SplineSeries2 = new SplineSeries
            {
                ItemsSource = DataSource2,
                XBindingPath = "XValue",
                YBindingPath = "YValue",
                LegendIcon = ChartLegendIcon.Circle,
                Label = "Q2"

            };

            SplineSeries3 = new SplineSeries
            {
                ItemsSource = DataSource3,
                XBindingPath = "XValue",
                YBindingPath = "YValue",
                LegendIcon = ChartLegendIcon.Circle,
                Label = "Q3"
            };

            SplineSeries4 = new SplineSeries
            {
                ItemsSource = DataSource4,
                XBindingPath = "XValue",
                YBindingPath = "YValue",
                LegendIcon = ChartLegendIcon.Circle,
                Label = "Q4"
            };
            
            Series = new ChartSeriesCollection()
            {
                SplineSeries1,
                SplineSeries2,
            };

            allSplineSeries = new List<SplineSeries>
            {
                SplineSeries1,
                SplineSeries2,
                SplineSeries3,
                SplineSeries4
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddSeries()
        {
            foreach (var spline in allSplineSeries)
            {
                if (!Series!.Any(s => s.Label == spline.Label))
                {
                    Series?.Add(spline);
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Series)));
                    break;
                }
            }
        }

        public void RemoveSeries()
        {
            if (Series != null && Series.Count > 1)
            {
                Series.RemoveAt(Series.Count - 1);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Series)));
            }
        }
    }
}
