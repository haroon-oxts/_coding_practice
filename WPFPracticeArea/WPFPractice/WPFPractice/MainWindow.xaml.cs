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

namespace WPFPractice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int number;

        public int Number
        {
            get {return number;}
            set
            {
                number = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Number"));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            Increment();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Increment()
        {
            Number = 0;

            for (int i = 0; i < 10; i++)
            {
                Number++;
                Thread.Sleep(1000);
            }
        }

    }
}
