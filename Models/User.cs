namespace App.Models
{
    //представление модели "Пользователь" из базы данных
    public class User
    {
        //ID
        public int Id { get; set; }

        //имя
        public string Login { get; set; }

        //пароль
        public string Password { get; set; }
    }
}
