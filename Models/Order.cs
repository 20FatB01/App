using System.Collections.Generic;

namespace App.Models
{
    //представление заказа пользователя
    public class Order
    {
        //ID
        public int Id { get; set; }

        //покупатель
        public User Customer { get; set; }

        //цена заказа
        public int Price { get; set; }

        //цена заказа
        public double Sale { get; set; }
    }
}
