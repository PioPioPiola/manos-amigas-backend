using ManoaAmigas.Domain.Entities;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUserRepository
    {
        Task CreateAsync(Person person);

        Task<Person?> GetByEmailAsync(string email);

        Task<Person?> GetByPersonIdAsync(string personId);

        Task CreateAsync(PersonDocument personDocument);

        Task<IEnumerable<Person>> GetAllPersonByAccountStatusAsync(char? accountStatus);

        Task<IEnumerable<Person>> GetAllPersonAsync();

        Task UpdatePersonAsync(Person person);
    }
}
