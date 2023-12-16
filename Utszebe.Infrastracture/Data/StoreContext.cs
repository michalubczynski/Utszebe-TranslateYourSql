using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuiler)
        {
            base.OnModelCreating(modelBuiler);
            modelBuiler.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
