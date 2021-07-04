namespace App.Models
{
    //специальный класс для вывода в таблицу
    public class OrderItem
    {
        //автор
        public string Автор { get; set; }

        //название книги
        public string Название { get; set; }

        //количество таких книг
        public int Количество { get; set; }
    }
}
