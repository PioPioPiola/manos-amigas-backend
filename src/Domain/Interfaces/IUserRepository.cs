using ManoaAmigas.Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(Person person);

        Task<Person?> GetByEmailAsync(string email);

        Task CreateAsync(PersonDocument personDocument);
    }
}
