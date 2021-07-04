using App.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace App
{

    /// <summary>
    /// Логика взаимодействия для Bucket.xaml
    /// </summary>
    public partial class Bucket : Window
    {        
        //родительское окно Меню
        private Menu parentMenu;

        //скидка
        private double sale;

        //сумма
        private double sum;

        //итоговая цена
        private double price;

        //стартовая функция
        public Bucket(Menu menu)
        {
            InitializeComponent();
            parentMenu = menu;
            table.ItemsSource = parentMenu.Order;
            GetNums();
        }

        //расчет численных значений
        private void GetNums()
        {
            int count = parentMenu.Order.Sum(x => x.Количество);

            GetSum();
            GetSale(count);

            //считаем итоговую цену
            price = sum * (1 - sale);

            //считаем количество
            count_label.Content = $"Количество книг: {count}";

            //выводим данные
            sale_label.Content = $"Скидка составила: {sale * 100}%";

            sum_label.Text = $"{sum} р.";
            if (sale > 0)
            {
                sum_label.TextDecorations = TextDecorations.Strikethrough;
                price_label.Content = $"{price} р.";
            }
            else
            {
                sum_label.TextDecorations = null;
                price_label.Content = string.Empty;
            }
        }

        //получаем изначальную сумму
        private void GetSum()
        {

            //считаем сумму заказа
            sum = 0;
            foreach (OrderItem item in parentMenu.Order)
            {
                //ищем книгу с таким же названием
                Book book = Database.Books.Where(x => x.Name == item.Название).First();

                //добавляем к сумме
                sum += book.Price * item.Количество;
            }
        }

        //получаем скилку
        private void GetSale(int count)
        {
            //расчет скидки
            sale = 0;
            if (count > 10)
            {
                sale = 0.15;
            }
            else if (count > 5)
            {
                sale = 0.1;
            }
            else if (count > 3)
            {
                sale = 0.05;
            }

            //считаем скидку с каждых 500 р.
            sale += ((int)sum / 500) * 0.01;
        }

        //кнопка В меню
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            parentMenu.Show();
            Close();
        }

        //подтверждение заказа
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            GetNums();
            int count = parentMenu.Order.Sum(x => x.Количество);
            if (count > 0)
            {
                Database.AddOrder(sum, sale, parentMenu.CurrentUser);
                MessageBox.Show("Ваша заявка успешно добавлена!");
                parentMenu.Order = new List<OrderItem>();
                Button_Click(sender, e);
            }
        }
    }
}
