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
    /// Логика взаимодействия для AddUserPage.xaml
    /// </summary>
    public partial class AddUserPage : Page
    {
        private User _currentUser = new User();
        public AddUserPage(User selectedUser)
        {
            InitializeComponent();
            if (selectedUser != null)
            {
                _currentUser = selectedUser;    
                cmbRole.Text = selectedUser.Role;
            }
            

            DataContext = _currentUser;

        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new AdminPage());
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();


            if (string.IsNullOrWhiteSpace(_currentUser.Login))

                errors.AppendLine("Укажите логин!");

            if (string.IsNullOrWhiteSpace(_currentUser.Password))

                errors.AppendLine("Укажите пароль!");

            if ((_currentUser.Role == null) || (cmbRole.Text == ""))

                errors.AppendLine("Выберите роль!");

           
            if (string.IsNullOrWhiteSpace(_currentUser.FIO))

                errors.AppendLine("Укажите Ф.И.О.!");

            if (errors.Length > 0)

            {

                MessageBox.Show(errors.ToString());

                return;

            } else
            {
                if (_currentUser.ID == 0)
                {
                    _currentUser.Role = cmbRole.Text;
                    Entities.GetContext().User.Add(_currentUser);
                
                }

                try

                {

                    Entities.GetContext().SaveChanges();
                    MessageBox.Show("Данные успешно сохранены!");
                    NavigationService.Navigate(new AdminPage());

                }

                catch (Exception ex)

                {

                    MessageBox.Show(ex.Message.ToString());

                }

            }

            
        }
    }
}
