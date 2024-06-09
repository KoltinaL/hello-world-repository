using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
    /// Логика взаимодействия для Admin.xaml
    /// </summary>
    public partial class Admin : Window
    {
        public Admin()
        {
            InitializeComponent();

            for (int i = 1; i <= 5; i++) {
                Button btn = new Button();
                btn.Height = 50;
                btn.Width = 50;
                btn.Content = i.ToString();
                btn.Background = Brushes.Black;
                btn.Foreground = Brushes.White;
                btn.Click += Btn_Click;

                OffsetContainer.Children.Add(btn);
            }

            Sorting.Items.Add("Vozrastanie");
            Sorting.Items.Add("Ubivanie");
            Sorting.SelectedIndex = 0;

            List<Role> roles = ISPr2337_SkvorcovAOzakaziContext.init().Roles.ToList();
            roles.Insert(0, new Role { Title = "All" });

            Filtering.ItemsSource = roles;
            Filtering.SelectedIndex = 0;

            update();
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            int pageNumber = int.Parse(((Button)sender).Content.ToString());
            int pageSize = 5;

            var user =  GetEntitiesPerPage(pageNumber, pageSize).ToList();
            MyListView.ItemsSource = user;
        }

        public void update() {
            IEnumerable<User> user = ISPr2337_SkvorcovAOzakaziContext.init().Users.Include(u => u.Role).Include(u => u.UsersZakazis).ThenInclude(u => u.Zakazi).Where(u => u.Username.Contains(Search.Text.Trim())).ToList();

            switch (Sorting.SelectedIndex) {
                case 0:
                    user = user.OrderBy(u => u.Id);
                    break;
                case 1:
                    user = user.OrderByDescending(u => u.Id);
                    break;
            }

            if (Filtering.SelectedIndex > 0) {
                user = user.Where(u => u.RoleId == (Filtering.SelectedItem as Role).Id);
            }

            MyListView.ItemsSource = user;
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            update();
        }

        private void Sorting_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update();
        }
        
        private IQueryable<User> GetEntitiesPerPage(int PageNumber, int offset) {
            return ISPr2337_SkvorcovAOzakaziContext.init().Users.Skip((PageNumber - 1) * offset).Take(offset);
        }

        private void Filtering_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            update();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService navigate = NavigationService.GetNavigationService(this);
            navigate.Navigate(new AddPage(new User()));            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string directory = Directory.GetCurrentDirectory();

            OpenFileDialog openFile = new OpenFileDialog { DefaultDirectory = directory, Filter = "All Files | *.*" };
            if (openFile.ShowDialog() == true) {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(openFile.FileName);
                image.EndInit();

                Sources.Source = image;
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (MyListView.SelectedItems.Count < 0) {
                return;
            }

            User r = MyListView.SelectedItem as User;

            ISPr2337_SkvorcovAOzakaziContext.init().Users.Remove(r);
            ISPr2337_SkvorcovAOzakaziContext.init().SaveChanges();

            update();
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            update();
        }
    }
}
