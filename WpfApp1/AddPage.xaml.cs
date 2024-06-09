using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для AddPage.xaml
    /// </summary>
    public partial class AddPage : Page
    {
        User ItsUser;

        public AddPage(User user)
        {
            this.ItsUser = user;
            InitializeComponent();
            DataContext = ItsUser;

            RoleS.Items.Add("admin");
            RoleS.Items.Add("user");
            RoleS.SelectedItem = 0;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (ItsUser.Id != 0)
            {
                ISPr2337_SkvorcovAOzakaziContext.init().Users.Add(ItsUser);
            }
                ISPr2337_SkvorcovAOzakaziContext.init().SaveChanges();
                NavigationService.GoBack();
        }
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog { Filter = "./Images | All Files" };

            if (openFile.ShowDialog() == true) {
                ItsUser.Image = File.ReadAllBytes(openFile.FileName);
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ItsUser.Id != 0) {
                ISPr2337_SkvorcovAOzakaziContext.init().Entry(ItsUser).Reload();
            }
        }
    }
}
