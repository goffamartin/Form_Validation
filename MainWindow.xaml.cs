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

namespace Formulář_s_validací
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        #region LostFocus
        private void EducationBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as ComboBox).SelectedValue == null)
                EducationAlert.Visibility = Visibility.Visible;
            else
                EducationAlert.Visibility = Visibility.Hidden;
        }

        private void LastNameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.Length < 1)
                LastNameAlert.Visibility = Visibility.Visible;
            else
                LastNameAlert.Visibility = Visibility.Hidden;
        }

        private void NameBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.Length < 1)
                NameAlert.Visibility = Visibility.Visible;
            else
                NameAlert.Visibility = Visibility.Hidden;
        }

        private void BirthYearBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.Length < 1)
                YearAlert.Visibility = Visibility.Visible;
            else
            {
                try
                {
                    Convert.ToDouble((sender as TextBox).Text);
                    YearAlert.Visibility = Visibility.Hidden;
                }
                catch
                {
                    YearAlert.Visibility = Visibility.Visible;
                }
            }
        }

        private void JobBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.Length < 1)
                JobAlert.Visibility = Visibility.Visible;
            else
                JobAlert.Visibility = Visibility.Hidden;
        }

        private void CashBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((sender as TextBox).Text.Length < 1)
                CashAlert.Visibility = Visibility.Visible;
            else
            {
                try
                {
                    Convert.ToDouble((sender as TextBox).Text);
                    CashAlert.Visibility = Visibility.Hidden;
                }
                catch
                {
                    CashAlert.Visibility = Visibility.Visible;
                }
            }
        }
        #endregion
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Employee emp = new Employee(NameBox.Text, LastNameBox.Text, Convert.ToInt32(BirthYearBox.Text), EducationBox.Text, JobBox.Text, Convert.ToDouble(CashBox.Text));
                MessageBox.Show($@"Rekapitulace informací:
       Jméno: {emp.Name}
    Příjmení: {emp.LastName}
Rok narození: {emp.BirthYear}
    Vzdělání: {emp.Education}
Prac. pozice: {emp.Job}
  hrubý plat: {emp.Cash} Kč");
            }
            catch
            {
                NameBox_LostFocus(NameBox, e);
                LastNameBox_LostFocus(LastNameBox, e);
                BirthYearBox_LostFocus(BirthYearBox, e);
                EducationBox_LostFocus(EducationBox, e);
                JobBox_LostFocus(JobBox, e);
                CashBox_LostFocus(CashBox, e);

                MessageBox.Show("Formulář nebyl vyplněn správně.");

            }
        }
    }

    public class Person
    {
        //private string _Name;
        //private string _LastName;
        private int _BirthYear;
        public string Name { get; set; }
        public string LastName { get; set; }
        public int BirthYear
        {
            get {return _BirthYear;}
            set
            {
                if (value < DateTime.Now.Year)
                    _BirthYear = value;
                else
                    throw new ArgumentOutOfRangeException();
            }
        }
        public Person ( string name, string lastname, int birthyear)
        {
            Name = name;
            LastName = lastname;
            BirthYear = birthyear;
        }
    }
    public class Employee : Person
    {
        private double _Cash;
        public string Education { get; set; }
        public string Job { get; set; }
        public double Cash
        {
            get { return _Cash;}
            set
            {
                if (value > 0)
                    _Cash = value;
                else
                    throw new ArgumentOutOfRangeException();
            }
        }
        public Employee( string name, string lastname, int birthyear, string education, string job, double cash) : base(name,lastname,birthyear)
        {
            Education = education;
            Job = job;
            Cash = cash;
        }
    }
}
