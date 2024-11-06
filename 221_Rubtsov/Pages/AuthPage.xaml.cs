using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(loginTextBox.Text) || string.IsNullOrEmpty(passwordBox.Password))
            {
                MessageBox.Show("Введите логин или пароль!");
                return;
            }

            using (var db = new Entities())
            {
                String HashPass = GetHash(passwordBox.Password);
                var user = db.User.AsNoTracking().FirstOrDefault(u => u.Login == loginTextBox.Text && u.Password == HashPass);

                if (user == null)
                {
                    MessageBox.Show("Пользователь с такими данными не найден!");
                    return;
                }

                switch(user.Role)
                {
                    case "Администратор":
                        NavigationService.Navigate(new AdminPage());
                        break;
                    case "Пользователь":
                        NavigationService.Navigate(new UserPage());
                        break;
                }
            }
        }
        public static string GetHash(string password)

        {

            using (var hash = SHA1.Create())

            {

                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));

            }

        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegPage());

        }
    }
}
