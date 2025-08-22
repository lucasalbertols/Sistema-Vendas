using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Vendas.Api.Data;

namespace Vendas.Api
{
    // Auxilia as ferramentas do EF a criar o contexto em tempo de design
    public class VendasDbContextFactory : IDesignTimeDbContextFactory<VendasDbContext>
    {
        public VendasDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<VendasDbContext>();
            // mesma connection string usada em appsettings (ou uma para design-time)
            builder.UseSqlite("Data Source=VendasDb.db");
            return new VendasDbContext(builder.Options);
        }
    }
}
