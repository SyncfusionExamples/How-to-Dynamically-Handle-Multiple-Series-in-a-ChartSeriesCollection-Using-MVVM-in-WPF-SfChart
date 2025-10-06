using System.Windows;

namespace ChartSeriesBinding
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Add_Button_Click(object sender, RoutedEventArgs e)
        {
            if(viewModel != null)
            {
                viewModel.AddSeries();
            }
        }

        private void Remove_Button_Click(object sender, RoutedEventArgs e)
        {
            if (viewModel != null)
            {
                viewModel.RemoveSeries();
            }
        }
    }
}