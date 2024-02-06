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

namespace krestiki_noliki
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Random random = new Random();
        Button[] buttons;
        string playerSymbol = "O";
        string robotSymbol = "X";
        bool isRobotMove = true;

        public MainWindow()
        {
            InitializeComponent();
            buttons = new Button[9] { _1, _2, _3, _4, _5, _6, _7, _8, _9 };
        }

        private void _1_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).Content = playerSymbol;
            (sender as Button).IsEnabled = false;

            if (!CheckWin(playerSymbol))
            {
                robotMove();
                CheckWin(robotSymbol);
            }
        }

        private void robotMove()
        {
            int knopka = 0;
            if (buttons.Any(x => x.IsEnabled))
                while (!buttons[knopka].IsEnabled)
                    knopka = random.Next(0, 9);

            buttons[knopka].Content = robotSymbol;
            buttons[knopka].IsEnabled = false;
        }

        private bool CheckWin(string cnt)
        {
            // если res будет true, значит, кто-то выиграл или ничья, то есть нет ходов
            bool res = false;

            // Проверка на выигрыш
            if (
                (_1.Content == _2.Content && _2.Content == _3.Content && _1.Content != "") ||
                (_4.Content == _5.Content && _5.Content == _6.Content && _4.Content != "") ||
                (_7.Content == _8.Content && _8.Content == _9.Content && _7.Content != "") ||
                (_1.Content == _4.Content && _4.Content == _7.Content && _1.Content != "") ||
                (_2.Content == _5.Content && _5.Content == _8.Content && _2.Content != "") ||
                (_3.Content == _6.Content && _6.Content == _9.Content && _3.Content != "") ||
                (_1.Content == _5.Content && _5.Content == _9.Content && _1.Content != "") ||
                (_3.Content == _5.Content && _5.Content == _7.Content && _3.Content != "")
               )
            {
                res = true;
                MessageBox.Show(cnt + " Wins!", "Игра окончена", MessageBoxButton.OK, MessageBoxImage.Information);
                foreach (Button b in buttons) b.IsEnabled = false;
            }

            // Проверка на "ничью"
            else
            {
                res = !buttons.Any(x => x.IsEnabled);
                if (res)
                    MessageBox.Show("Ничья!", "Игра окончена", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            return res;
        }

        private void _start_Click(object sender, RoutedEventArgs e)
        {
            string tmp;
            tmp = playerSymbol;
            playerSymbol = robotSymbol;
            robotSymbol = tmp;

            _start.Content = "Заново";
            lbPlayerSymbol.Content = "Вы играете за: " + playerSymbol;
            foreach (Button b in buttons)
            {
                b.Content = "";
                b.IsEnabled = true;
            }

            isRobotMove = !isRobotMove;
            if (isRobotMove) robotMove();
        }
    }
}
