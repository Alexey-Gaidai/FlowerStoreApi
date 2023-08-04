using System.ComponentModel.DataAnnotations.Schema;

namespace FlowerStoreApi.Data.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public DateTime OrderDate { get; set; }
        public string OrderStatus { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Apartment { get; set; }
        public User User { get; set; }
        public ICollection<OrderedBouquets> OrderedBouquets { get; set; }
    }
}

