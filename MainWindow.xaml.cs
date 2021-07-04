using App.Models;
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

namespace App
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        //нажатие на кнопку авторизации
        private void auth_button_Click(object sender, RoutedEventArgs e)
        {
            //имя пользователя
            string name = name_textbox.Text;

            //пароль пользователя
            string password = password_textBox.Text;

            //выбираем пользователей с таким же именем
            var result = Database.Users.Where(user => user.Login == name);

            //если таких пользователей нет
            if (result.Count() == 0)
            {
                MessageBox.Show("Ошибка: пользователя с таким именем нет.");
            }
            else //если есть пользователь с таким имененм
            {
                //берем пользователя
                User user = result.First();

                //если введений в окне пароль совпадает с паролем пользователя
                if (user.Password == password)
                {
                    Menu menu = new Menu(user);
                    menu.Show();
                    Hide();
                }
                else
                {
                    MessageBox.Show("Ошибка: неправильный пароль.");
                }
            }
        }
    }
}
