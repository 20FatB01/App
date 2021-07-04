using App.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace App
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public List<OrderItem> Order { get; set; }
        public User CurrentUser { get; private set; }
        public Menu(User user)
        {
            Order = new List<OrderItem>();
            CurrentUser = user;
            InitializeComponent();
        }

        //добавление книги в заказ
        public void AddBookInOrder(Book book)
        {
            //проверяем, есть ли уже элемент с таким названием в заказе
            var result = Order.Where(item => item.Название == book.Name);

            //если такого элемента нет
            if (result.Count() == 0)
            {
                //добавляем строку с количеством 1
                Order.Add(new OrderItem { Количество = 1, Название = book.Name, Автор = book.Author.Name});
            }
            else
            {
                //прибавляем единицу
                result.First().Количество += 1;
            }
        }

        //нажатие на кнопку каталог
        private void catalog_button_Click(object sender, RoutedEventArgs e)
        {
            Catalog catalog = new Catalog(this);
            catalog.Show();
            Hide();
        }

        //нажатие на кнопку корзина
        private void bucket_button_Click(object sender, RoutedEventArgs e)
        {
            Bucket bucket = new Bucket(this);
            bucket.Show();
            Hide();
        }

        //нажатие на кнопку ваши заказы
        //private void orders_button_Click(object sender, RoutedEventArgs e)
        //{

        //}
    }
}
