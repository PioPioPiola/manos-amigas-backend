using Application.Interfaces;
using ManoaAmigas.Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Domain.Enums;

namespace Infrastructure.Repositories
{
    public class ProviderCategoryEnrollmentRepository : IProviderCategoryEnrollmentRepository
    {
        private readonly AppDbContext _db;

        public ProviderCategoryEnrollmentRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(ProviderCategoryEnrollment providerCategoryEnrollment)
        {            
            _db.providercategoryenrollment.Add(providerCategoryEnrollment);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProviderCategoryEnrollment>> GetAllAsync()
        {
            return await _db.providercategoryenrollment.ToListAsync();
        }

        public async Task<bool> ExistsByPersonAndCategoryAsync(string personId, int categoryId)
        {
            return await _db.providercategoryenrollment.AnyAsync(e =>
                e.person_id == personId &&
                e.category_id == categoryId
            );
        }
    }
}
