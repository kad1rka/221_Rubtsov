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

namespace _221_Rubtsov.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public UserPage()
        {
            InitializeComponent();
            var currentUsers = Entities.GetContext().User.ToList();
            ListUser.ItemsSource = currentUsers;
            OrderComboBox.SelectedIndex = 0;
            userCheckBox.IsChecked = false;
            UpdateUsers();
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateUsers();
        }

        private void OrderComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUsers();
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Clear();
            OrderComboBox.SelectedIndex = 0;
            userCheckBox.IsChecked = false;

        }

        private void userCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            UpdateUsers();
        }
        private void userCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            UpdateUsers();
        }
        private void UpdateUsers()

        {


            var currentUsers = Entities.GetContext().User.ToList();


            //осуществляем поиск по Ф.И.О. без учета регистра букв

            currentUsers = currentUsers.Where(x => x.FIO.ToLower().Contains(SearchTextBox.Text.ToLower())).ToList();



            if (userCheckBox.IsChecked.Value)

                currentUsers = currentUsers.Where(x => x.Role.Contains("Пользователь")).ToList();



            if (OrderComboBox.SelectedIndex == 0)

                ListUser.ItemsSource = currentUsers.OrderBy(x => x.FIO).ToList();

            else ListUser.ItemsSource = currentUsers.OrderByDescending(x => x.FIO).ToList();
            }

        }


}
