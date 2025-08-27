using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Estoque.Api.Data;

namespace Estoque.Api
{
    public class EstoqueDbContextFactory : IDesignTimeDbContextFactory<EstoqueDbContext>
    {
        public EstoqueDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<EstoqueDbContext>();
            builder.UseSqlServer("Server=localhost,1433;Database=AvanadeDB;User Id=sa;Password=123;TrustServerCertificate=True;");
            return new EstoqueDbContext(builder.Options);
        }
    }
}
