using Application.DTOs;
using Application.Interfaces;
using ManoaAmigas.Domain.Entities;

namespace Application.Services
{
    public class PersonService
    {
        private readonly IUserRepository _userRepository;

        public PersonService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Person>> GetAllPersonAsync(char? accountStatus)
        {
            var persons = accountStatus.HasValue ? await _userRepository.GetAllPersonByAccountStatusAsync(accountStatus) : await _userRepository.GetAllPersonAsync();

            if (persons == null)
            {
                throw new KeyNotFoundException("No se encontraron personas registradas.");
            }

            return persons;
        }

        public async Task<bool> UpdatePersonVerificationStatusAsync(string personId, char accountStatus)
        {
            var person = await _userRepository.GetByPersonIdAsync(personId);

            if (person == null)
            {
                throw new KeyNotFoundException($"No se encontró la persona con ID: {personId}.");
            }

            person.account_status = accountStatus;
            await _userRepository.UpdatePersonAsync(person);
            return true;
        }
    }
}
