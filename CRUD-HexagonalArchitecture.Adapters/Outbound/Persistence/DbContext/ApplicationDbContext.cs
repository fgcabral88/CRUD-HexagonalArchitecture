using CRUD_HexagonalArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CRUD_HexagonalArchitecture.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
            : base(options) { }

        public DbSet<TaxEntity> Taxes => Set<TaxEntity>();
    }
}
