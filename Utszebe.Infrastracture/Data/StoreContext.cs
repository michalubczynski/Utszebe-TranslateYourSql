using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<SqlQuery> SqlQuery { get; set; }


        ////Method required to be overriden to apply ProductConfiguration changes
        protected override void OnModelCreating(ModelBuilder modelBuiler)
        {
            base.OnModelCreating(modelBuiler);
            modelBuiler.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
