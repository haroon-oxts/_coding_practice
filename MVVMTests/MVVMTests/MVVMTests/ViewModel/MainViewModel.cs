using GalaSoft.MvvmLight;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace MVVMTests.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            MyChar = Convert.ToChar("G");


            Collection = new ObservableCollection<Tomato>
            {
                new Tomato { Grossness = 0, Squishiness = 4},
                new Tomato { Grossness = 10000, Squishiness = 24},
                new Tomato { Grossness = 2, Squishiness = -3},
                new Tomato { Grossness = 8, Squishiness = 6},
                new Tomato { Grossness = 5, Squishiness = 9},
            };

            BindingOperations.EnableCollectionSynchronization(Collection, m_lock);

            UpdateChar();

            Console.WriteLine("Main thread: " + Thread.CurrentThread.ManagedThreadId);
            Console.WriteLine("finished contsructing mainview model");
        }

        public char MyChar { get { return m_my_char; } set { m_my_char = value; PropertyChangedHandler?.Invoke(this, new PropertyChangedEventArgs("MyChar")); } }
        private char m_my_char ;

        public ObservableCollection<Tomato> Collection { get { return m_collection; } set { Set(ref m_collection, value); } }
        private ObservableCollection<Tomato> m_collection;
        private object m_lock = new object();
        
        private async void UpdateChar()
        {
            await Task.Run(() =>
            {
                for (int i = 0; i < 20; ++i)
                {
                    MyChar++;
                    
                    //lock the collection before modification
                    lock (m_lock){Collection.Add(new Tomato { Squishiness = i, Grossness = i * 3 });}

                    Console.WriteLine("UpdateChar thread ID: " + Thread.CurrentThread.ManagedThreadId);
                    Thread.Sleep(500);
                }
            });
        }

        private async Task UpdateCharTask()
        {
            while (true)
            {
                for (int i = 0; i < 20; ++i)
                {
                    Collection.Add(new Tomato { Squishiness = i, Grossness = i * 3 });
                    Console.WriteLine("UpdateCharTask thread ID: " + Thread.CurrentThread.ManagedThreadId);
                }
                await Task.Delay(TimeSpan.FromMilliseconds(500));
            }
        }

    }

    public class Tomato
    {
        public int Squishiness  { get; set; }
        public int Grossness    { get; set; }
    }

}