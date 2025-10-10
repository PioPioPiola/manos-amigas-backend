using Application.Interfaces;
using ManoaAmigas.Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Person person)
        {
            _db.person.Add(person);
            await _db.SaveChangesAsync();
        }

        public async Task<Person?> GetByEmailAsync(string email)
        {
            return await _db.person.FirstOrDefaultAsync(u => u.email == email);
        }
    }
}
