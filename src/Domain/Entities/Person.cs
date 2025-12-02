using System;
using System.ComponentModel.DataAnnotations;

namespace ManoaAmigas.Domain.Entities
{
    public class Person
    {
        [Key]
        public string person_id { get; set; } = null!;
        public string? name { get; set; }
        public string first_names { get; set; } = null!;
        public string last_names { get; set; } = null!;
        public char identification_type { get; set; }
        public string identification_number { get; set; } = null!;
        public string email { get; set; } = null!;
        public long? phone_number { get; set; }
        public DateTime date_of_birth { get; set; }
        public char? role { get; set; }
        public char account_status { get; set; } 
        public bool? meets_requirements { get; set; }
        public DateTime? verification_date { get; set; }
        public DateTime registration_date { get; set; } = DateTime.Now;
        public DateTime? last_updated { get; set; }
        public string password_hash { get; set; } = null!;
        public char? gender { get; set; }
        public string? address { get; set; }
        public string? city { get; set; }
        public string? department { get; set; }
        public string? postal_code { get; set; }
        public char? security_question { get; set; }
        public string? security_answer { get; set; }
        public bool accept_terms { get; set; }
        public bool accept_data_policy { get; set; }
        public bool wants_notifications { get; set; }
    }
}