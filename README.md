# How-to-Dynamically-Handle-Multiple-Series-in-a-ChartSeriesCollection-Using-MVVM-in-WPF-SfChart
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
 ```
 
**Step 3:** In your `XAML`, instantiate your ViewModel and set it as the `DataContext`. Then, bind the SfChart's Series property to the `Series` property.

 **XAML**
 ```
<Grid>
     <Grid.DataContext>
         <local:ViewModel x:Name="viewModel"/>
     </Grid.DataContext>
     <chart:SfChart Series="{Binding Series}" 
                    Palette="Custom"
                    Grid.Row="1"
                    Margin="20,0">
         <chart:SfChart.Legend>
             <chart:ChartLegend ToggleSeriesVisibility="True"
                                IconHeight="10"
                                IconWidth="10"
                                FontSize="14"
                                ItemMargin="6"/>
         </chart:SfChart.Legend>
         
         <chart:SfChart.ColorModel>
             <chart:ChartColorModel>
                 <chart:ChartColorModel.CustomBrushes>
                     <SolidColorBrush Color="Blue"/>
                     <SolidColorBrush Color="Red"/>
                     <SolidColorBrush Color="Green"/>
                     <SolidColorBrush Color="BlueViolet"/>
                 </chart:ChartColorModel.CustomBrushes>
             </chart:ChartColorModel>
         </chart:SfChart.ColorModel>

         <chart:SfChart.PrimaryAxis>
             <chart:CategoryAxis LabelPlacement="BetweenTicks">
                 <chart:CategoryAxis.LabelStyle>
                     <chart:LabelStyle FontSize="18" />
                 </chart:CategoryAxis.LabelStyle>
             </chart:CategoryAxis>
         </chart:SfChart.PrimaryAxis>
         
         <chart:SfChart.SecondaryAxis>
             <chart:NumericalAxis Interval="5" Maximum="45">
                 <chart:NumericalAxis.LabelStyle>
                     <chart:LabelStyle FontSize="18" />
                 </chart:NumericalAxis.LabelStyle>
             </chart:NumericalAxis>
         </chart:SfChart.SecondaryAxis>
     </chart:SfChart>
 </Grid> 
 ```
 
 **Step 4:** Implement Dynamic Series Handling

To add and remove series at runtime, we have a button on the UI to trigger the corresponding methods in the ViewModel. The Click events in the code-behind will call the `AddSeries()` and `RemoveSeries()` methods that responsible for adding and removing the series from the Series collection.

  **XAML**
 ```
<Grid>
        <!-- ... Chart codes ... -->
        <Grid Grid.Row="2"
              HorizontalAlignment="Center"
              Margin="4">
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

**Code Behind**
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
