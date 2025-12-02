using ManoaAmigas.Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServiceRepository
    {
        Task CreateAsync(Service service);

        Task<Service?> GetByIdAsync(long serviceId);

        Task UpdateAsync(Service service);
    }
}
