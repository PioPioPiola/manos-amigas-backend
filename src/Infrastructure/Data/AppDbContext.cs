using ManoaAmigas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Person> person { get; set; } = null!;
        public DbSet<PersonDocument> persondocument { get; set; } = null!;
    }
}
