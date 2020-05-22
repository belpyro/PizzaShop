using RestSample.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestSample.Data.Contexts
{
    public sealed class PizzaShopContext : DbContext
    {
        public DbSet<PizzaDb> Pizzas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var entity = modelBuilder.Entity<PizzaDb>();

            entity.HasKey(x => x.Id).ToTable("ShopPizzas");
            entity.Property(x => x.Name).IsRequired().HasMaxLength(150).IsUnicode().IsVariableLength();
            entity.Property(x => x.Price).IsRequired();            
        }
    }
}
