using ManoaAmigas.Domain.Entities;

namespace Application.DTOs
{
    public class ProviderCategoryEnrollmentDto
    {
        public long EnrollmentId { get; set; }

        public string PersonId { get; set; } = null!;

        public int? CategoryId { get; set; }

        public DateTime? EnrollmentDate { get; set; }

        public bool? IsActive { get; set; }

        public bool? MeetsRequirements { get; set; }

        public List<PersonDocument> PersonDocuments { get; set; } = new List<PersonDocument>();
    }
}
 