using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace CalculatorUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        Connector myConn;
        public LoginPage()
        {
            this.InitializeComponent();
            myConn = new Connector();
        }
     
        //register a new user
        private void Click_Register(object sender, RoutedEventArgs e)
        {
            if (userNameTextBox.Text == "" && passwordTextBox.Text == "") return;
            messageBox.Text = myConn.RegisterNewUser(userNameTextBox.Text, passwordTextBox.Text);
        }

        //login user. if it is successful, back to main page else display error
        private void Click_Login(object sender, RoutedEventArgs e)
        {
            string msg = myConn.LoginUser(userNameTextBox.Text, passwordTextBox.Text);
            if (msg == "Success")
            {
                this.Frame.Navigate(typeof(MainPage),userNameTextBox.Text);
                return;
            }
            messageBox.Text = msg;
        }

        private void Click_back(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
