using System.Text.Json.Serialization;

namespace FlowerStoreApi.Data.Models
{
    public class Bouquet
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        [JsonIgnore]
        public ICollection<OrderedBouquets> OrderedBouquets { get; set; }
        [JsonIgnore]
        public ICollection<BouquetCopmosition> BouquetCopmosition { get; set; }
    }
}
