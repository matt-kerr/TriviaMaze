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
using System.Windows.Shapes;

namespace TriviaMaze
{
    /// <summary>
    /// Interaction logic for InstructionWindow.xaml
    /// </summary>
    public partial class InstructionWindow : Window
    {
        private int num = 0;
        private Image[] images;
        public InstructionWindow()
        {
            InitializeComponent();
            icon_two.Visibility = Visibility.Hidden;
            icon_three.Visibility = Visibility.Hidden;
            icon_four.Visibility = Visibility.Hidden;
            icon_five.Visibility = Visibility.Hidden;
            icon_six.Visibility = Visibility.Hidden;
            icon_seven.Visibility = Visibility.Hidden;
            icon_eight.Visibility = Visibility.Hidden;
            icon_nine.Visibility = Visibility.Hidden;
            images = new Image[] {icon_one,icon_two,icon_three,icon_four,icon_five,icon_six,icon_seven,icon_eight,icon_nine};
        }

        private void button_return_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_continue_Click(object sender, RoutedEventArgs e)
        {
            images[num].Visibility = Visibility.Hidden;
            num++;
            if (num == 8)
            {
                button_continue.Visibility = Visibility.Hidden;
            }
            images[num].Visibility =Visibility.Visible;

        }

    }
}
