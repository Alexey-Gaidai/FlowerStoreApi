namespace FlowerStoreApi.Data.Models.DTO
{
    public class CreateOrderDTO
    {
        public int UserID { get; set; }
        public List<CreateOrderedBouquetDTO> OrderedBouquets { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Apartment { get; set; }
    }
}
