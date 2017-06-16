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
using System.Diagnostics;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CalculatorUWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private string numStr;
        private double firstOperand;
        private double secondOperand;
        private string currOperator;
        private bool finishedAround;

        public MainPage()
        {
            this.InitializeComponent();

            numStr = "0";
            finishedAround = true;
        }

        //when numbers are clicked
        private void Click_Num(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            //Just boot up or just finished around of calculation
            if (finishedAround)
            {
                numStr = btn.Content.ToString();
                finishedAround = false;
            }
            else if (numStr.Length >= 14) return;
            else numStr += btn.Content.ToString();
            Display.Text = numStr;
        }

        //when operator are clicked
        private void Click_Operator(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            firstOperand = Convert.ToDouble(numStr);
            currOperator = btn.Content.ToString();
            numStr = "";
            Display.Text = currOperator;
        }

        //when equal sign is clicked
        private void Click_Calculate(object sender, RoutedEventArgs e)
        {
            secondOperand = Convert.ToDouble(numStr);
            if (currOperator == "+")
            {
                firstOperand += secondOperand;
            }
            else if (currOperator == "-")
            {
                firstOperand -= secondOperand;
            }
            else if (currOperator == "/")
            {
                firstOperand = firstOperand / secondOperand;
            }
            else if (currOperator == "X")
            {
                firstOperand = firstOperand * secondOperand;
            }
            //reset operator
            currOperator = "";
            numStr = firstOperand.ToString();
            Display.Text = numStr;
            finishedAround = true;
        }

        //clear everything
        private void Click_Clear(object sender, RoutedEventArgs e)
        {
            numStr = "0";
            firstOperand = 0.0;
            secondOperand = 0.0;
            currOperator = "";
            Display.Text = numStr;
            finishedAround = true;
        }


        private void GotoLogin(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }
    }
}
