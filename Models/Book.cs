namespace App.Models
{
    //представление модели "Книга" из базы данных
    public class Book
    {
        //номер в базе данных
        public int Id { get; set; }

        //название
        public string Name { get; set; }

        //автор
        public Author Author { get; set; }

        //цена
        public int Price { get; set; }

        //количество в магазинах
        public int CountInShops { get; set; }

        //количество на складе
        public int CountInWarehouse { get; set; }

        //путь до картинки
        public string ImagePath { get; set; }
    }
}
