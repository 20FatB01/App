using App.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;

namespace App
{
    //класс работы с базой данных
    public static class Database
    {
        //соединение с базой данных
        public static SqlConnection Connection { get; private set; }

        //свойство для получения списка заказов
        public static List<Order> Orders
        {
            get
            {
                return GetOrders();
            }
        }

        //свойство для получения списка авторов
        public static List<Author> Authors 
        {
            get
            {
                return GetAuthors();
            }
        }

        //локальное хранилище книг
        private static List<Book> books = null;

        //свойство для получения списка книг
        public static List<Book> Books
        {
            get
            {
                if (books == null)
                {
                    books = GetBooks();
                }
                else
                {
                    List<Book> currentList = GetBooks();
                    if (books.Count != currentList.Count)
                    {
                        books = currentList;
                    }
                }
                return books;
            }
        }

        //свойство для получения списка пользователей
        public static List<User> Users
        {
            get
            {
                return GetUsers();
            }
        }

        //метод, вызываемый при первом обращении
        static Database()
        {
            Connection = new SqlConnection("Data Source=ngknn.ru;Initial Catalog=32В-Книжный;User ID=32В;Password=444444");
            Connection.Open();
        }

        //метод получения из базы данных результатов по SQL запросу (query)
        private static DataTable SendQuery(string query)
        {
            //пустая таблица
            DataTable result = new DataTable();

            //пытаемся выполнить кол
            try
            {
                //создаем SQL команду по тексту
                SqlCommand command = new SqlCommand(query, Connection);

                //Создаем считывающий элемент
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                //запроняем таблицу
                adapter.Fill(result);
            }

            //если словаили ошибку
            catch (Exception ex)
            {
                //выводим сообщение
                MessageBox.Show("При попытке отправить запрос в базу данных было получено исключение: " + ex.Message);
            }

            //возвращаем результат - таблицу
            return result;
        }

        //получение всех авторов
        private static List<Author> GetAuthors()
        {
            //пустой список авторов
            List<Author> authors = new List<Author>();

            //получаем таблицу с авторами
            DataTable table = SendQuery("Select * from Авторы");

            //преобразуем таблицу в список
            foreach (DataRow row in table.Rows)
            {
                //пробуем выполнить код
                try
                {
                    //создаем автора по данным в столбце
                    Author author = new Author()
                    {
                        //ID преобразуем из столбца 0
                        Id = int.Parse(row.ItemArray[0].ToString()),

                        //ID преобразуем из столбца 1
                        Name = row.ItemArray[1].ToString()
                    };

                    //добавляем автора в список
                    authors.Add(author);
                }
                //если словаили ошибку
                catch (Exception ex)
                {
                    //выводим сообщение
                    MessageBox.Show("Не удалось преобразовать автора по следующей причине: " + ex.Message);
                }
            }

            //возвращаем список
            return authors;
        }

        //получение всех пользователей
        private static List<User> GetUsers()
        {
            //пустой список авторов
            List<User> users = new List<User>();

            //получаем таблицу с авторами
            DataTable table = SendQuery("Select * from Пользователи");

            //преобразуем таблицу в список
            foreach (DataRow row in table.Rows)
            {
                //пробуем выполнить код
                try
                {
                    //создаем автора по данным в столбце
                    User user = new User()
                    {
                        //Id преобразуем из столбца 1
                        Id = int.Parse(row.ItemArray[0].ToString()),

                        //Логин преобразуем из столбца 2
                        Login = row.ItemArray[1].ToString().TrimEnd(' '),

                        //Пароль преобразуем из столбца 3
                        Password = row.ItemArray[2].ToString().TrimEnd(' ')
                    };

                    //добавляем автора в список
                    users.Add(user);
                }
                //если словаили ошибку
                catch (Exception ex)
                {
                    //выводим сообщение
                    MessageBox.Show("Не удалось преобразовать автора по следующей причине: " + ex.Message);
                }
            }

            //возвращаем список
            return users;
        }

        //получение всех книг
        private static List<Book> GetBooks()
        {
            //пустой список авторов
            List<Book> books = new List<Book>();

            //получаем таблицу с авторами
            DataTable table = SendQuery("Select * from Книги");

            //преобразуем таблицу в список
            foreach (DataRow row in table.Rows)
            {
                //пробуем выполнить код
                try
                {
                    //создаем автора по данным в столбце
                    Book book = new Book()
                    {
                        //ID преобразуем из столбца 0
                        Id = int.Parse(row.ItemArray[0].ToString()),

                        //Название преобразуем из столбца 1
                        Name = row.ItemArray[1].ToString(),

                        //Цену преобразуем из столбца 3
                        Price = int.Parse(row.ItemArray[3].ToString()),

                        //Количество в магазинах преобразуем из столбца 4
                        CountInShops = int.Parse(row.ItemArray[4].ToString()),

                        //Количество на складе преобразуем из столбца 5
                        CountInWarehouse = int.Parse(row.ItemArray[5].ToString()),

                        //Путь до картинки преобразуем из столбца 6
                        ImagePath = row.ItemArray[6].ToString(),
                    };

                    //берем id автора из столбца 2
                    int authorId = int.Parse(row.ItemArray[2].ToString());

                    //берем того автора, у которого id совпадает с authorId
                    Author author = Authors.Where(temp => temp.Id == authorId).First();

                    //задаем автора книге
                    book.Author = author;

                    //добавляем автора в список
                    books.Add(book);
                }
                //если словаили ошибку
                catch (Exception ex)
                {
                    //выводим сообщение
                    MessageBox.Show("Не удалось преобразовать автора по следующей причине: " + ex.Message);
                }
            }

            //возвращаем список
            return books;
        }

        //получение всех заказов
        private static List<Order> GetOrders()
        {
            //пустой список авторов
            List<Order> orders = new List<Order>();

            //получаем таблицу с авторами
            DataTable table = SendQuery("Select * from Заказы");

            //преобразуем таблицу в список
            foreach (DataRow row in table.Rows)
            {
                //пробуем выполнить код
                try
                {
                    //создаем автора по данным в столбце
                    Order order = new Order()
                    {
                        Id = int.Parse(row.ItemArray[0].ToString()),
                        Price = int.Parse(row.ItemArray[2].ToString()),
                        Sale = double.Parse(row.ItemArray[3].ToString()),
                    };

                    //берем id автора из столбца 2
                    int userId = int.Parse(row.ItemArray[1].ToString());

                    //берем того автора, у которого id совпадает с authorId
                    User user = Users.Where(temp => temp.Id == userId).First();

                    //задаем автора книгеuse
                    order.Customer = user;

                    //добавляем автора в список
                    orders.Add(order);
                }
                //если словаили ошибку
                catch (Exception ex)
                {
                    //выводим сообщение
                    MessageBox.Show("Не удалось преобразовать заказ по следующей причине: " + ex.Message);
                }
            }

            //возвращаем список
            return orders;
        }

        //добавление заказа в базу данных
        public static void AddOrder(double price, double sale, User user)
        {
            var list = Orders;
            int id = 0;
            if (Orders.Count != 0)
            {
                id = Orders.OrderBy(x => x.Id).Last().Id + 1;
            }
            SendQuery($"INSERT INTO Заказы VALUES ({id}, {user.Id}, {price}, '{sale}');");
        }
    }

}
