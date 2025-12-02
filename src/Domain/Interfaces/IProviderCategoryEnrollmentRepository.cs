using ManoaAmigas.Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IProviderCategoryEnrollmentRepository
    {
        Task CreateAsync(ProviderCategoryEnrollment providerCategoryEnrollment);

        Task<IEnumerable<ProviderCategoryEnrollment>> GetAllAsync();

        Task<bool> ExistsByPersonAndCategoryAsync(string personId, int categoryId);
    }
}
