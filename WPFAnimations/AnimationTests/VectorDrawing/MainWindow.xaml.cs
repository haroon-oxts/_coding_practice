using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace VectorDrawing
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Line SpinnyLine { get; set; }
        public List<Vector> Vectors { get; set; }
        public ObservableCollection<Line> Lines {get; set;}
 
        public double Center_X { get; set; }
        public double Center_Y { get; set; }


        public MainWindow()
        {
            InitializeComponent();

            Center_X = 0;
            Center_Y = 0;

            //DrawLine();
            //UpdateLine();


            GenerateVectors();
            DrawVectors();
                    
        }

        public void DrawLine()
        {
            double Point_X  = Center_X + 50;
            double Point_Y  = Center_Y;

            // Add a Line Element
            SpinnyLine = new Line
            {
                Stroke = System.Windows.Media.Brushes.LightSteelBlue,
                X1 = Center_X,
                Y1 = Center_Y,
                X2 = Point_X,
                Y2 = Point_Y,
                StrokeThickness = 2
            };

            MainWindowCanvas.Children.Add(SpinnyLine);
        }

        public void UpdateLine()
        {
            Task.Run(() =>
            {
                Vector initial_position = new Vector();

                while (true)
                {
                    Application.Current.Dispatcher.Invoke(() => {

                        initial_position = new Vector
                        {
                            X = SpinnyLine.X2,
                            Y = SpinnyLine.Y2
                        };

                    });


                    Matrix rotation_matrix = new Matrix(
                        Math.Cos(10 * Math.PI / 180),
                       -Math.Sin(10 * Math.PI / 180),
                        Math.Sin(10 * Math.PI / 180),
                        Math.Cos(10 * Math.PI / 180),
                        200, 
                        200);



                    Vector rotated_vector =  Vector.Multiply(initial_position, rotation_matrix);


                    Application.Current.Dispatcher.Invoke(() => {


                        SpinnyLine.X2 = rotated_vector.X;
                        SpinnyLine.Y2 = rotated_vector.Y;



                    });

                    Thread.Sleep(500);
                }
            });
            
        }

        public void GenerateVectors()
        {
            Vectors = new List<Vector>();
            Random r = new Random();

            for(int vector_count = 0; vector_count < 3; vector_count++) 
            {
                Vectors.Add(new Vector(r.Next(400,500), r.Next(400, 500)));
            } 
        }

        public void DrawVectors()
        {
            Random r = new Random();
            byte[] bytes = new byte[3];
            Lines = new ObservableCollection<Line>();

            foreach(Vector v in Vectors)
            {
                r.NextBytes(bytes);

                Lines.Add(new Line
                {
                    X1 = Center_X,
                    Y1 = Center_Y,
                    X2 = v.X,
                    Y2 = v.Y,
                    Stroke =  new SolidColorBrush(Color.FromRgb(bytes[0], bytes[1], bytes[2])),
                });

                MainWindowCanvas.Children.Add(Lines[Lines.Count - 1]);

            }

            Vector average_vector = CalculateAverageVector();

            Line average_line = new Line
            {
                X1 = Center_X,
                Y1 = Center_Y,
                X2 = average_vector.X,
                Y2 = average_vector.Y,
                Stroke = Brushes.Red,
                StrokeThickness = 3,
            };

            MainWindowCanvas.Children.Add(average_line);

        }

        public Vector CalculateAverageVector()
        {
            Vector average_vector = new Vector();

            foreach(Vector v in Vectors)
            {
                average_vector.X += v.X;
                average_vector.Y += v.Y;
            }

            average_vector.X = average_vector.X / Vectors.Count;
            average_vector.Y = average_vector.Y / Vectors.Count;

            return average_vector;
        }
    }
}
