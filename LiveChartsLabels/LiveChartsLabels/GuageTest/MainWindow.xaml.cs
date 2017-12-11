using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace GuageTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public int InputValue
        {
            get { return m_input_value; }
            set { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("InputValue")); m_input_value = value; }
        }

        private int m_input_value;

        public MainWindow()
        {
            InputValue = 0;
            InitializeComponent();
            DataContext = this;
         }

        private void HandleGuageUpdated(object sender, DataTransferEventArgs e)
        {
            LiveCharts.Wpf.Gauge current_guage = (LiveCharts.Wpf.Gauge)sender;

            if (current_guage.Value >= current_guage.To)
            {
                current_guage.ToColor = Color.FromRgb(0x5b, 0xc6, 0x43);

            }
            else
            {
                current_guage.ToColor = Color.FromRgb(0x1F, 0x86, 0xE0);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InputValue += 10;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            InputValue -= 10;

        }
    }
}
