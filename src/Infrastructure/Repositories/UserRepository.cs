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

        public async Task CreateAsync(PersonDocument personDocument)
        {
            _db.persondocument.Add(personDocument);
            await _db.SaveChangesAsync();
        }

        public async Task<Person?> GetByPersonIdAsync(string personId)
        {
            return await _db.person
                .FirstOrDefaultAsync(u => u.person_id == personId);
        }

        public async Task<IEnumerable<Person>> GetAllPersonByAccountStatusAsync(char? accountStatus)
        {
            return await _db.person
                .Where(p => p.account_status == accountStatus)
                .ToListAsync();
        }

        public async Task<IEnumerable<Person>> GetAllPersonAsync()
        {
            return await _db.person.ToListAsync();
        }

        public async Task UpdatePersonAsync(Person person)
        {
            _db.person.Update(person);
            await _db.SaveChangesAsync();
        }
    }
}
