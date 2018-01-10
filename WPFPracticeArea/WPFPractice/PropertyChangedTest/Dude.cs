using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PropertyChangedTest
{
    public class Dude : ObservableObject
    {
        private int m_ass_size;
        public int AssSize
        {
            get { return m_ass_size; }
            set
            {
                OnPropertyChanged("AssSize");
                m_ass_size = value;
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

        private Pizza m_pizza;
        public Pizza Pizza
        {
            get { return m_pizza; }
            set
            {
                OnPropertyChanged("Pizza");
                m_pizza = value;
            }
        }

    }
}
