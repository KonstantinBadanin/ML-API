using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DataBase
{
    public class Context : DbContext
    {
        public Context() : base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=409;Integrated Security=True;MultipleActiveResultSets=True") { }
        public void Clear()
        {
            foreach (var item in Items)
            {
                Items.Remove(item);
            }
            foreach (var item in BlobsClasses)
            {
                BlobsClasses.Remove(item);
            }
            SaveChanges();
            foreach (var item in Counters)
            {
                Counters.Remove(item);
            }
            SaveChanges();
        }
        public DbSet<Item> Items { get; set; } = null!;
        public DbSet<Counter> Counters { get; set; } = null!;
        public DbSet<BlobClass> BlobsClasses { get; set; } = null!;
    }
}
