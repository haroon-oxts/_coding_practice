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

namespace PropertyChangedTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Dude DudeOne { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;

            DudeOne = new Dude {
                Name = "Blart",
                AssSize = 0,
                Pizza = new Pizza
                {
                    NumberOfPepperoni = 4,
                    Name = "UltimateRoniPizza"
                }
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DudeOne.AssSize++;
            DudeOne.Name = String.Format("Duder" + DudeOne.AssSize*1000);

            DudeOne.Pizza.NumberOfPepperoni++;
            DudeOne.Pizza.Name = String.Format("Duder" + DudeOne.AssSize * 1000 + "'s Pizza");

            if (DudeOne.AssSize > 10)
            {
                DudeOne.Pizza = new Pizza
                {
                    NumberOfPepperoni = 9999,
                    Name = "GodsPizza"
                };
            }
        }
    }
}
