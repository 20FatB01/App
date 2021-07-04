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
using System.Windows.Shapes;

namespace App
{
    /// <summary>
    /// Логика взаимодействия для Catalog.xaml
    /// </summary>
    public partial class Catalog : Window
    {
        //родительское окно Меню
        private Menu parentMenu;

        //текущая выбранная книга
        private Book currentBook;

        //стартовая функция
        public Catalog(Menu menu)
        {
            parentMenu = menu;

            //первоначальная книга - самая первая книга в списке
            currentBook = Database.Books.First();

            InitializeComponent();

            //отображаем данные книги
            ShowBook();
        }

        //кнопка "В меню"
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //отображаем родительское окно
            parentMenu.Show();

            //закрываем текущее окно
            Close();
        }

        //кнопка "В корзину"
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //добавляем книгу в список книг заказа
            parentMenu.AddBookInOrder(currentBook);

            //выводим сообщение
            MessageBox.Show("Книга успешно добавлена в корзину");
        }

        //кнопка "Предыдущая"
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //получаем позицию текущей книги в списке книг
            int currentIndex = Database.Books.IndexOf(currentBook);

            //если это не самая первая книга
            if (currentIndex > 0)
            {
                //отнимаем единицу у номера текущей книги
                currentIndex--;
            }
            else
            {
                //переходим к последнему элементу
                currentIndex = Database.Books.Count - 1;
            }

            //получаем текущую книгу
            currentBook = Database.Books[currentIndex];

            //отображаем данные книги
            ShowBook();
        }

        //кнопка "Следующая"
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //получаем позицию текущей книги в списке книг
            int currentIndex = Database.Books.IndexOf(currentBook);

            //если это не самая первая книга
            if (currentIndex < Database.Books.Count - 1)
            {
                //добавляем единицу у номера текущей книги
                currentIndex++;
            }
            else
            {
                //переходим к нулевому элементу
                currentIndex = 0;
            }

            //получаем текущую книгу
            currentBook = Database.Books[currentIndex];

            //отображаем данные книги
            ShowBook();
        }

        //функция отображения данных книги
        private void ShowBook()
        {
            //если текущая книга существует (на всякий случай)
            if (currentBook != null)
            {
                //задаем свойства
                name_textBox.Text = currentBook.Name;
                author_textBox.Text = currentBook.Author.Name;
                price_textBox.Text = currentBook.Price.ToString();
                shopCount_textBox.Text = currentBook.CountInShops.ToString();
                wareCount_textBox.Text = currentBook.CountInWarehouse.ToString();

                //загружаем картинку по пути
                Uri path = new Uri(AppDomain.CurrentDomain.BaseDirectory + currentBook.ImagePath, UriKind.Absolute);
                BitmapImage image = new BitmapImage(path);
                mainImage.Source = image;

                //считаем общее количество экземпляров
                int allCount = currentBook.CountInWarehouse + currentBook.CountInShops;

                //если общее количество больше пяти
                if (allCount > 5)
                {
                    //пишем много
                    allCount_textBox.Text = "Много";
                }
                //иначе
                else
                {
                    //пишем количество
                    allCount_textBox.Text = allCount.ToString();
                }
            }
        }
    }
}
