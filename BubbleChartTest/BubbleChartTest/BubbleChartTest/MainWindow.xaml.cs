using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BubbleChartTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ChartSource ChartSource { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            ChartSource = new ChartSource();

            DataContext = ChartSource;
        }

        private void UpdateAllOnClick(object sender, RoutedEventArgs e)
        {
            ChartSource.Update();
        }
    }

    public class ChartSource
    {
        private int min = 1;
        private int max = 200;
        private int chart_center_x = 5;
        private int chart_center_y = 5;

        public ChartSource()
        {
            SeriesCollection = new SeriesCollection
            {
                new ScatterSeries
                {
                    Values = new ChartValues<ScatterPoint>
                    {
                        new ScatterPoint(chart_center_x, chart_center_y, 15),
                    },
                    Title                 = "GPS Max Satellites",
                    MinPointShapeDiameter = min,
                    MaxPointShapeDiameter = max,
                    Fill                  = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFf4f4f4")),
                },
                new ScatterSeries
                {
                    Values = new ChartValues<ScatterPoint>
                    {
                        new ScatterPoint(chart_center_x, chart_center_x, 0),
                    },
                    Title                 = "MinPlaceholder",
                    MinPointShapeDiameter = min,
                    MaxPointShapeDiameter = max,
                },
                new ScatterSeries
                {
                    Values = new ChartValues<ScatterPoint>
                    {
                        new ScatterPoint(chart_center_x, chart_center_y, 5),
                    },
                    Title                 = "Satellites Tracked",
                    MinPointShapeDiameter = min,
                    MaxPointShapeDiameter = max,
                    Fill                  = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFff0000")),
                },
                new ScatterSeries
                {
                    Values = new ChartValues<ScatterPoint>
                    {
                        new ScatterPoint(chart_center_x, chart_center_y, 10),
                    },
                    Title                 = "Satellites Used",
                    MinPointShapeDiameter = min,
                    MaxPointShapeDiameter = max,
                    Fill                  = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF11d831")),
                },
            };

        }

        public SeriesCollection SeriesCollection { get; set; }

        public void Update()
        {
            var r = new Random();

            foreach (var series in SeriesCollection)
            {
                foreach (var bubble in series.Values.Cast<ScatterPoint>())
                {
                    if (bubble.Weight < 15)
                    {
                        if (series.Title == "Satellites Tracked")
                        {
                            bubble.Weight = r.Next(10, 15);
                        }
                        if (series.Title == "Satellites Used")
                        {
                            bubble.Weight = r.Next(0, 10);
                        }
                        else
                        {
                            //bubble.Weight = r.NextDouble() * 10;
                        }
                    }
                    Console.Write(" Weight : " + bubble.Weight + " , ");
                }
                Console.WriteLine("");
            }
        }

    }


    
}

