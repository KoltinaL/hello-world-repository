using Microsoft.Win32;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
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

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            User user = ISPr2337_SkvorcovAOzakaziContext.init().Users.FirstOrDefault(u => u.Username.Contains(login.Text.Trim()) && (u.Surname.Contains(password.Text.Trim()) || u.Surname.Contains(Pwbox.Password.Trim())));

            if (user != null) {
                if (user.RoleId == 1)
                {
                    Admin adminWindow = new Admin();
                    adminWindow.ShowDialog();
                    this.Close();
                }
                else
                {
                    userWin userWindow = new userWin();
                    userWindow.ShowDialog();
                    this.Close();
                }
            } else {
                MessageBox.Show("That user or password is undefined");
            }
        }

        private void Checkbox_Checked(object sender, RoutedEventArgs e)
        {
            password.Visibility = Visibility.Visible;
            Pwbox.Visibility = Visibility.Hidden;
        }

        private void Checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            password.Visibility = Visibility.Hidden;
            Pwbox.Visibility = Visibility.Visible;
        }
    }
}