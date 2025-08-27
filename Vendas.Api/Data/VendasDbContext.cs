using Microsoft.EntityFrameworkCore;
using Vendas.Api.Models;

namespace Vendas.Api.Data
{
    public class VendasDbContext : DbContext
    {
        public VendasDbContext(DbContextOptions<VendasDbContext> options) : base(options) { }

        public DbSet<Pedido> Pedidos => Set<Pedido>();
        public DbSet<ItemPedido> Itens => Set<ItemPedido>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pedido>(e =>
            {
                e.HasKey(p => p.Id);
                e.HasMany(p => p.Itens)
                 .WithOne(i => i.Pedido)
                 .HasForeignKey(i => i.PedidoId)
                 .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<ItemPedido>(e =>
            {
                e.HasKey(i => i.Id);
            });
        }
    }
}
