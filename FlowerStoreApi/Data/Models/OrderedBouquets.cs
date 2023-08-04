using System.ComponentModel.DataAnnotations.Schema;

namespace FlowerStoreApi.Data.Models
{
    public class OrderedBouquets
    {
        public int ID { get; set; }
        public int OrderID { get; set; }     // Идентификатор заказа (внешний ключ)
        public int BouquetID { get; set; }   // Идентификатор товара (внешний ключ)
        public int Quantity { get; set; }    // Количество заказанного товара
        public Order Order { get; set; }
        public Bouquet Bouquet { get; set; }
    }
}
