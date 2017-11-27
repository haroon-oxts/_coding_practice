using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ProgressBarTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window , INotifyPropertyChanged
    {

        int value;

        private double m_thing;

        public double Thing { get { return m_thing; } set { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Thing")); m_thing = value; } }

        public MainWindow()
        {
            InitializeComponent();

            Bar.Maximum = 30;
            Bar.Minimum = 0;

            value = 30;

            Thing = -2.23425;


            DataContext = this;

            UpdateBar().ContinueWith(t => { }) ;



        }


        async Task UpdateBar()
        {
            for (int i = 0; i <= Bar.Maximum; ++i)
            {
                value--;

                Bar.Value = Bar.Maximum - value;

                Thing++;

                await Task.Delay(100);
            }
            
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
