using System;
using System.ComponentModel.DataAnnotations;

namespace ManoaAmigas.Domain.Entities
{
    public class ProviderCategoryEnrollment
    {
        [Key]
        public long enrollment_id { get; set; }

        public string? person_id { get; set; }

        public int? category_id { get; set; }

        public DateTime? enrollment_date { get; set; }

        public char? enrollment_status { get; set; }

        public decimal? minimum_fee { get; set; }

        public bool? meets_requirements { get; set; }

        public string? rejection_reason { get; set; }

        public string? suspension_reason { get; set; }

        public bool? is_active { get; set; }

        public List<PersonDocument> person_documents { get; set; } = new List<PersonDocument>();
    }
}