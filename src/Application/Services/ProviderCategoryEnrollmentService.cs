using Application.DTOs;
using Application.Interfaces;
using ManoaAmigas.Domain.Entities;

namespace Application.Services
{
    public class ProviderCategoryEnrollmentService
    {
        private readonly IUserRepository _userRepository;

        private readonly IProviderCategoryEnrollmentRepository _providerCategoryEnrollmentRepository;

        public ProviderCategoryEnrollmentService(IUserRepository userRepository, IProviderCategoryEnrollmentRepository providerCategoryEnrollmentRepository)
        {
            _userRepository = userRepository;
            _providerCategoryEnrollmentRepository = providerCategoryEnrollmentRepository;
        }

        public async Task<IEnumerable<ProviderCategoryEnrollmentDto>> GetAllEnrollments()
        {
            var enrollments = await _providerCategoryEnrollmentRepository.GetAllAsync();

            if (enrollments == null || !enrollments.Any())
            {
                throw new KeyNotFoundException("No se encontraron inscripciones.");
            }

            var enrollmentsDto = enrollments.Select(e => new ProviderCategoryEnrollmentDto
            {
                EnrollmentId = e.enrollment_id,
                PersonId = e.person_id,
                CategoryId = e.category_id,
                EnrollmentDate = e.enrollment_date,
                IsActive = e.is_active,
                MeetsRequirements = e.meets_requirements
            });

            return enrollmentsDto;
        }

        public async Task<bool> HasEnrollmentForCategoryAsync(string personId, int categoryId)
        {
            return await _providerCategoryEnrollmentRepository.ExistsByPersonAndCategoryAsync(personId, categoryId);
        }

        public async Task CreateAsync(ProviderCategoryEnrollmentDto providerCategoryEnrollmentDto)
        {
            var person = await _userRepository.GetByPersonIdAsync(providerCategoryEnrollmentDto.PersonId);

            if (person == null)
            {
                throw new KeyNotFoundException("No existe la persona de la isncripción");
            }

            var providerInfo = new ProviderCategoryEnrollment
            {
                person_id = providerCategoryEnrollmentDto.PersonId,
                category_id = providerCategoryEnrollmentDto.CategoryId,
                enrollment_date = DateTime.UtcNow,
                is_active = false,
                meets_requirements = providerCategoryEnrollmentDto.PersonDocuments.Count > 0 ? true : false
            };

            await _providerCategoryEnrollmentRepository.CreateAsync(providerInfo);

            if (providerCategoryEnrollmentDto.PersonDocuments?.Any() == true) //Creo la inscripción y le agrego los documentos a la persona para la inscripción, según los que requiera por categoría
            {
                var tasks = providerCategoryEnrollmentDto.PersonDocuments.Select(document =>
                {
                    var personDocument = new PersonDocument
                    {
                        person_id = person.person_id,
                        doctype_id = document.doctype_id,
                        issue_date = DateTime.UtcNow,
                        issue_place = document.issue_place
                    };

                    return _userRepository.CreateAsync(personDocument);
                });

                await Task.WhenAll(tasks);
            }
        }
    }
}
