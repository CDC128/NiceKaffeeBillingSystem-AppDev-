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

namespace NiceKaffee
{
    /// <summary>
    /// Interaction logic for HomeViewMenu.xaml
    /// </summary>
    public partial class HomeViewMenu : Page
    {
        public HomeViewMenu()
        {
            InitializeComponent();
            FunctionContent.Content = new Admin.ViewMenu();
            ManageMenu.Visibility = Visibility.Visible;
        }

        private void AddMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            FunctionTitleName.Content = "Add Menu";
            FunctionContent.Content = new Admin.AddMenu();
        }

        private void ViewMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            FunctionTitleName.Content = "View Menu";
            FunctionContent.Content = new Admin.ViewMenu();
            
        }

        private void UpdateMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            FunctionTitleName.Content = "Update Menu";
            FunctionContent.Content = new Admin.UpdateMenu();
        }

        private void DeleteMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            FunctionTitleName.Content = "Delete Menu";
            FunctionContent.Content = new Admin.DeleteMenu();
        }

        private void ManageMenuBtn_Click(object sender, RoutedEventArgs e)
        {
            ManageMenu.Visibility = Visibility.Visible;
            ManageUsers.Visibility = Visibility.Collapsed;
            FunctionTitleName.Content = "View Menu";
            FunctionContent.Content = new Admin.ViewMenu();

        }

        private void ManageUsersBtn_Click(object sender, RoutedEventArgs e)
        {
            ManageMenu.Visibility = Visibility.Collapsed;
            ManageUsers.Visibility = Visibility.Visible;
            FunctionTitleName.Content = "View User";
            FunctionContent.Content = FunctionContent.Content = new Admin.ViewUser(); 
        }

        private void AddUserBtn_Click(object sender, RoutedEventArgs e)
        {
            FunctionTitleName.Content = "Add User";
            FunctionContent.Content = new Admin.AddUser();
        }

        private void ViewUserBtn_Click(object sender, RoutedEventArgs e)
        {
            FunctionTitleName.Content = "View User";
            FunctionContent.Content = new Admin.ViewUser();
        }

        private void UpdateUserBtn_Click(object sender, RoutedEventArgs e)
        {
            FunctionTitleName.Content = "Update User";
            FunctionContent.Content = new Admin.UpdateUser();
        }

        private void DeleteUserBtn_Click(object sender, RoutedEventArgs e)
        {
            FunctionTitleName.Content = "Delete User";
            FunctionContent.Content = new Admin.DeleteUser();
        }
    }
}
