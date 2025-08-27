using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Vendas.Api.Data;

namespace Vendas.Api
{
    public class VendasDbContextFactory : IDesignTimeDbContextFactory<VendasDbContext>
    {
        public VendasDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<VendasDbContext>();
            builder.UseSqlServer("Server=localhost,1433;Database=AvanadeDB;User Id=sa;Password=123;TrustServerCertificate=True;");
            return new VendasDbContext(builder.Options);
        }
    }
}
