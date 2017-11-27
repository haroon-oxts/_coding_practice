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
        int min = 1;
        int max = 250;


        double value = -32.123113;

        public MainWindow()
        {
            InitializeComponent();

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<double>
                    {
                        1.3,5.0,8,9,2,5,3,8

                    },

                }
            };

            //SeriesCollection = new SeriesCollection
            //{
            //    new ScatterSeries
            //    {
            //        Values = new ChartValues<ScatterPoint>
            //        {
            //            new ScatterPoint(5, 3, 15),
            //        },
            //        Title                 = "MaxPlaceholder",
            //        MinPointShapeDiameter = min,
            //        MaxPointShapeDiameter = max,
            //        Fill                  = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFf4f4f4")),
            //    },
            //    new ScatterSeries
            //    {
            //        Values = new ChartValues<ScatterPoint>
            //        {
            //            new ScatterPoint(5, 3, 0),
            //        },
            //        Title                 = "MinPlaceholder",
            //        MinPointShapeDiameter = min,
            //        MaxPointShapeDiameter = max,
            //    },
            //    new ScatterSeries
            //    {
            //        Values = new ChartValues<ScatterPoint>
            //        {
            //            new ScatterPoint(5, 3, 5),
            //        },
            //        Title                 = "Satellites Tracked",
            //        MinPointShapeDiameter = min,
            //        MaxPointShapeDiameter = max,
            //        Fill                  = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFff0000")),
            //    },
            //    new ScatterSeries
            //    {
            //        Values = new ChartValues<ScatterPoint>
            //        {
            //            new ScatterPoint(5, 3, 10),
            //        },
            //        Title                 = "Satellites Used",
            //        MinPointShapeDiameter = min,
            //        MaxPointShapeDiameter = max,
            //        Fill                  = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF11d831")),
            //    },
            //};

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }

        private void UpdateAllOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();

            foreach (var series in SeriesCollection)
            {
                foreach (var bubble in series.Values.Cast<ScatterPoint>())
                {
                    if(bubble.Weight < 15)
                    {
                        if (series.Title == "Satellites Tracked" ) 
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
