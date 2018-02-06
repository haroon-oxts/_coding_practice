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

namespace WPFResourceManager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Page> pages;
        private int current_page_number;

        public MainWindow()
        {
            InitializeComponent();

            pages = new List<Page>
            {
               new Page1(),
               new Page2(),
               new Page3(),
            };

            current_page_number = 0;

            MainWindowFrame.Content = pages[current_page_number];

        }

        private void Next(object sender, RoutedEventArgs e)
        {
            if( current_page_number < (pages.Count - 1))
            {
                current_page_number++;
            }

            MainWindowFrame.Content = pages[current_page_number];
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            if (current_page_number != 0)
            {
                current_page_number--;
            }

            MainWindowFrame.Content = pages[current_page_number];
        }

    }
}
