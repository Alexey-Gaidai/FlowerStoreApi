using FlowerStoreApi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerStoreApi.Data
{
    public class FlowerStoreDBContext : DbContext
    {
        public FlowerStoreDBContext(DbContextOptions<FlowerStoreDBContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Bouquet> Bouquets { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Flower> Flowers { get; set; }
        public DbSet<BouquetCopmosition> BouquetCopmosition { get; set;}
        public DbSet<OrderedBouquets> OrderedBouquets { get; set; }
    }
}
