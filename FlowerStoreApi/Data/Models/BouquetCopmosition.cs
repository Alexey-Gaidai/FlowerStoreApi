namespace FlowerStoreApi.Data.Models
{
    public class BouquetCopmosition
    {
        public int ID { get; set; }
        public int FlowerID { get; set; }
        public int BouquetID { get; set; }
        public int Quantity { get; set; }

        public Flower Flower { get; set; }
        public Bouquet Bouquet { get; set; }
    }
}
