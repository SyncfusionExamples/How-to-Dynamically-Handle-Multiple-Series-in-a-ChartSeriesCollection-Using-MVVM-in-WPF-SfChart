# How to Dynamically Handle Multiple Series in a ChartSeriesCollection Using MVVM in WPF SfChart
Learn how to dynamically manage multiple chart series in Syncfusion's WPF SfChart using the MVVM. Learn to bind ChartSeriesCollection, update series at runtime, and maintain clean separation of concerns for scalable, maintainable apps.

Follow these steps to implement MVVM data binding for WPF Chart series:

**Step 1:** Create a `DataModel` to represent the points in your chart series. This model should contain properties for your `X` and `Y` values.

**C#**
 ```csharp
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
 ```
 
**Step 2:** Define your `ViewModel` to hold data and manage chart series. The ViewModel will hold the data as observable collections of Model objects and the `ChartSeriesCollection` that the `SfChart` will bind to, adhering to the MVVM pattern.

 **C#**
 ```csharp
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
             ...
         };

         DataSource2 = new ObservableCollection<Model>
         {
             new Model("Benz", 12),
             ...
         };

         DataSource3 = new ObservableCollection<Model>
         {
             new Model("Benz", 25),
             ...
         };

         DataSource4 = new ObservableCollection<Model>
         {
             new Model("Benz", 35),
             ...
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
            ...
         };

         SplineSeries3 = new SplineSeries
         {
            ...
         };

         SplineSeries4 = new SplineSeries
         {
             ...
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
 ```
 
**Step 3:** For Chart control initialization, you can refer this [UG documentation](https://help.syncfusion.com/wpf/charts/getting-started). Then, bind the SfChart's Series property to the `Series` property.

 **XAML**
 ```
 <chart:SfChart Series="{Binding Series}">
 
     <chart:SfChart.PrimaryAxis>
         <chart:CategoryAxis/>
     </chart:SfChart.PrimaryAxis>
     
     <chart:SfChart.SecondaryAxis>
         <chart:NumericalAxis/>
     </chart:SfChart.SecondaryAxis>
 </chart:SfChart>
 ```
 
 **Step 4:** We can add and remove series at runtime. There are two buttons on the UI that trigger the corresponding methods in the ViewModel. The Click events in the code-behind call the AddSeries() and RemoveSeries() methods, which are responsible for adding and removing series from the Series collection.
  **XAML**
 ```
<Grid>
        <!-- ... Chart codes ... -->
        <Grid Grid.Row="2">
            <Grid.ColumnDefinitions>
                <ColumnDefinition Width="Auto"/>
                <ColumnDefinition Width="Auto"/>
            </Grid.ColumnDefinitions>
            <Button Content="Add Series" 
                    Padding="4"
                    Margin="4"
                    Click="Add_Button_Click" 
                    Height="45"
                    FontWeight="Bold"
                    Width="100"/>
            <Button Grid.Column="1" 
                    Click="Remove_Button_Click" 
                    Height="45"
                    Width="100"
                    Margin="4"
                    Padding="4"
                    FontWeight="Bold"
                    Content="Remove Series"/>
        </Grid>
    </Grid> 
 ```

**C#**
 ```csharp
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
 ```
 
**Output:**

![Output.gif](https://support.syncfusion.com/kb/agent/attachment/article/21460/inline?token=eyJhbGciOiJodHRwOi8vd3d3LnczLm9yZy8yMDAxLzA0L3htbGRzaWctbW9yZSNobWFjLXNoYTI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjUwNDY1Iiwib3JnaWQiOiIzIiwiaXNzIjoic3VwcG9ydC5zeW5jZnVzaW9uLmNvbSJ9.twCs9LSjXNacquyO-XilDKHTDBd45AOqsjsBbJE9FzQ)

For more details, you can refer this [How to Dynamically Handle Multiple Series in a ChartSeriesCollection Using MVVM in WPF SfChart](https://support.syncfusion.com/kb/article/21460/how-to-dynamically-handle-multiple-series-in-a-chartseriescollection-using-mvvm-in-wpf-sfchart) KB article
