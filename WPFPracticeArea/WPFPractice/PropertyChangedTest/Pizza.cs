using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyChangedTest 
{
    public class Pizza : ObservableObject
    {
        private int m_number_of_pepperoni;
        public int NumberOfPepperoni
        {
            get { return m_number_of_pepperoni; }
            set
            {
                OnPropertyChanged("AssSize");
                m_number_of_pepperoni = value;
            }
        }


        private string m_name;
        public string Name
        {
            get { return m_name; }
            set
            {
                OnPropertyChanged("Name");
                m_name = value;
            }
        }
    }
}
