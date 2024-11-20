using _221_Rubtsov.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace _221_Rubtsov
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AuthPage());
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Login.Text) || string.IsNullOrEmpty(Pass.Text) || string.IsNullOrEmpty(PassCopy.Text) || string.IsNullOrEmpty(FIO.Text))
            {
                MessageBox.Show("Введены не все данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return ;
            } else if (Pass.Text != PassCopy.Text)
            {
                MessageBox.Show("Пароли не совпадают!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            } else if (Pass.Text.Length < 6)
            {
                MessageBox.Show("Пароли меньше 6 символов!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else if (!CheckEnglishLetters(Pass.Text))
            {
                MessageBox.Show("Используйте латинские буквы!");
                return;
            } else if (!CheckNumber(Pass.Text))
            {
                MessageBox.Show("Используйте хотя бы одну цифру!");
                return;

            }
            else
            {
                using (var db = new Entities())
                {

                    var UserExsists = db.User
                    .AsNoTracking()
                    .FirstOrDefault(u => u.Login == Login.Text);
                    if (UserExsists == null)
                    {
                        User userObject = new User
                        {

                            FIO = FIO.Text,
                            Login = Login.Text,
                            Password = GetHash(Pass.Text),
                            Role = Role.Text,

                        };
                        db.User.Add(userObject);
                        db.SaveChanges();
                        MessageBox.Show("Пользователь успешно зарегистрирован!");
                        NavigationService.Navigate(new RegPage());
                        
                    }
                    else
                    {
                        MessageBox.Show("Пользователь с таким логином уже зарегистрирован.");
                    }


                }
            }

        }
        bool CheckEnglishLetters(String password)
        {
            bool en = true; // английская раскладка

            for (int i = 0; i < password.Length; i++) // перебираем символы
            {
                if (password[i] >= 'а' && password[i] <= 'я' ) en = false;
                if (password[i] >= 'А' && password[i] <= 'Я') en = false; // если русская раскладка
            }
            if (en == false) 
            {
                
                return false;
            }
            return true;

        }

        bool CheckNumber(String password)
        {
            bool number = false; // цифра

            for (int i = 0; i < password.Length; i++) // перебираем символы
            {
                if (password[i] >= '0' && password[i] <= '9') number = true; // если цифры
            }
            
            if (!number)
            {
                return false;
            }
            return true;
        }
        public static string GetHash(string password)

        {

            using (var hash = SHA1.Create())

            {

                return string.Concat(hash.ComputeHash(Encoding.UTF8.GetBytes(password)).Select(x => x.ToString("X2")));

            }

        }

    }
}



