using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace WPF_Keyboard_Trainer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ButtonName = "";
        Brush prevColor;
        DateTime begin;
        int fails = 0;
        int enteredchars = 0;

        public MainWindow()
        {
            InitializeComponent();
            FailsLab.Content = 0;
            SpeedLab.Content = 0;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.KeyDown += new KeyEventHandler(Label_KeyDown);
            this.KeyUp += new KeyEventHandler(Label_KeyUp);
        }

        private void Label_KeyDown(object sender, KeyEventArgs e)
        {
            if (StartButton.IsEnabled) return;

            if(ButtonName == "")
            {
                begin = DateTime.Now;
            }

            string tmp;
            if (e.Key == Key.Space)
            {
                tmp = " ";
            }
            else
            {
                tmp = e.Key.ToString().Last().ToString();
            }

            if (e.Key.ToString().Length<=2 || tmp==" ")
            {
                if(ButtonName != "" && StopButton.IsEnabled) (FindName(ButtonName) as Button).Background = prevColor;

                if(tmp==" ")
                {
                    ButtonName = "KeySpace";
                }
                else
                {
                    ButtonName = "Key" + tmp;
                }
                prevColor = (FindName(ButtonName) as Button).Background;
                (FindName(ButtonName) as Button).Background = Brushes.Aqua;

                if (MyLab.Content.ToString()[0] == tmp.ToLower().Last())
                {
                    enteredchars++;
                    DateTime then = DateTime.Now;
                    int myspeed = (int)(enteredchars/(then.Subtract(begin).TotalMilliseconds)*60000);
                    SpeedLab.Content = myspeed;
                    MyLab.Content = MyLab.Content.ToString().Remove(0, 1);
                    if(MyLab.Content.ToString().Length==0)
                    {
                        MessageBox.Show("Congratulations!");
                        StartButton.IsEnabled = true;
                        StopButton.IsEnabled = false;
                        (FindName(ButtonName) as Button).Background = prevColor;
                        ButtonName = "";
                        fails = 0;
                        enteredchars = 0;
                        SpeedLab.Content = 0;
                        FailsLab.Content = 0;
                    }
                }
                else
                {
                    fails++;
                    FailsLab.Content = fails;
                }
            }
        }

        private void Label_KeyUp(object sender, KeyEventArgs e)
        {
            if(StopButton.IsEnabled) (FindName(ButtonName) as Button).Background = prevColor;
        }

        private void DiffSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            DiffLab.Content = (int)(DiffSlider.Value);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;
            StopButton.IsEnabled = true;
            int difflevel = int.Parse(DiffLab.Content.ToString());
            switch (difflevel)
            {
                case 1:
                    MyLab.Content = "asta lavista baby";
                    break;
                case 2:
                    MyLab.Content = "my name is george washington";
                    break;
                case 3:
                    MyLab.Content = "london is a capital of great britain";
                    break;
                case 4:
                    MyLab.Content = "we dont negotiate with the terrorists";
                    break;
                case 5:
                    MyLab.Content = "bad boys whatcha want watcha want whatcha gonna do when sheriff john brown come for you tell me whatcha wanna do whatcha gonna do";
                    break;
                default:
                    break;
            }
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = true;
            StopButton.IsEnabled = false;
            ButtonName = "";
            fails = 0;
            enteredchars = 0;
            SpeedLab.Content = 0;
            FailsLab.Content = 0;
            MyLab.Content = "";
        }
    }
}
