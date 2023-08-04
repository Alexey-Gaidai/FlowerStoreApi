using System.Text.Json.Serialization;

namespace FlowerStoreApi.Data.Models
{
    public class Flower
    {
        public int ID { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<BouquetCopmosition> BouquetCopmositions { get; set; }
    }
}
