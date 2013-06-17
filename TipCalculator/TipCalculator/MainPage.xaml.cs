using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using TipCalculator.Resources;
using System.ComponentModel;

namespace TipCalculator
{
    public partial class MainPage : PhoneApplicationPage, INotifyPropertyChanged
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            DataContext = this;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();

            MainScreenVisible = Visibility.Visible ;
            AboutScreenVisible = Visibility.Collapsed;

            txtNumberOfPeople.Text = "1";
            txtTippercent.Text = "18.0";
            txtBillAmmount.Text = "0.0";

        }

        private void ValuesChanged(object sender, TextChangedEventArgs e)
        {
            RunCalculations();
        }

        private void RunCalculations()
        {
            try
            {
                double billAmmount = 0;
                double tip = 0;
                int numberOfPeople = 0;
                double totalBill = 0;
                double totalTax = 0;
                double totalTip = 0;
                double totalPerPerson = 0;
                bool roundChecked = false;

                if (Convert.ToBoolean(Round.IsChecked))
                {
                    roundChecked = true;
                }

                if (txtBillAmmount.Text != null && txtBillAmmount.Text != string.Empty)
                {
                    billAmmount = Convert.ToDouble(txtBillAmmount.Text);
                }

                if (txtTippercent != null && txtTippercent.Text != string.Empty)
                {
                    tip = Convert.ToDouble(txtTippercent.Text) * .01;
                }

                if (txtNumberOfPeople != null && txtNumberOfPeople.Text != string.Empty)
                {
                    numberOfPeople = Convert.ToInt32(txtNumberOfPeople.Text);
                }

                totalTip = Math.Round((billAmmount * tip), 2);
                totalBill = Math.Round((billAmmount + totalTax + totalTip), 2);
                totalPerPerson = Math.Round((totalBill / numberOfPeople), 2);

                Total = "$" + totalBill.ToString();
                PerPerson = "$" + totalPerPerson.ToString();
                Tip = "$" + totalTip.ToString();

                if (roundChecked)
                {
                    PerPerson = "$" + Math.Ceiling(totalPerPerson).ToString();
                    Total = "$" + Math.Ceiling(totalBill).ToString();
                    Tip = "$" + Math.Ceiling(totalTip).ToString();
                }
            }
            catch (Exception ex)
            {
                txtBillAmmount.Text = "";
                txtNumberOfPeople.Text = "";
                txtTippercent.Text = "";

                MessageBox.Show("Please Enter a valid number!");
            }
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        private string _perPerson;
        public string PerPerson
        {
            get { return _perPerson; }
            set { _perPerson = value; RaisePropertyChagned("PerPerson"); }
        }

        private string _total;
        public string Total
        {
            get { return _total; }
            set { _total = value; RaisePropertyChagned("Total"); }
        }

        private string _tip;
        public string Tip
        {
            get { return _tip; }
            set { _tip = value; RaisePropertyChagned("Tip"); }
        }

        private string _tax;

        public string Tax
        {
            get { return _tax; }
            set { _tax = value; RaisePropertyChagned("Tax"); }
        }

        private Visibility _aboutScreenVisible;
        public Visibility AboutScreenVisible
        {
            get { return _aboutScreenVisible; }
            set { _aboutScreenVisible = value; RaisePropertyChagned("AboutScreenVisible"); }
        }

        private Visibility _mainScreenVisible;
        public Visibility MainScreenVisible
        {
            get { return _mainScreenVisible; }
            set { _mainScreenVisible = value; RaisePropertyChagned("MainScreenVisible"); }
        }
        
        

        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChagned(string prop)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

        private void CheckChanged(object sender, RoutedEventArgs e)
        {
            RunCalculations();
        }

        bool _aboutScreenInFocus;
        private void AboutClicked(object sender, EventArgs e)
        {
            if (!_aboutScreenInFocus)
            {
                _aboutScreenInFocus = true;
                AboutScreenVisible = Visibility.Visible;
                MainScreenVisible = Visibility.Collapsed;
            }
        }

        private void BackPressed(object sender, CancelEventArgs e)
        {
            if (_aboutScreenInFocus)
            {
                AboutScreenVisible = Visibility.Collapsed;
                MainScreenVisible = Visibility.Visible;
                _aboutScreenInFocus = false;
                e.Cancel = true;
            }
        }
    }
}