using ManoaAmigas.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManoaAmigas.Domain.Entities
{
    public class PersonDocument
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int document_id { get; set; }

        [Required]
        public string person_id { get; set; } = null!;

        [Required]
        public int doctype_id { get; set; }

        [Required]
        [MaxLength(50)]
        public string document_number { get; set; } = null!;

        public DateTime? issue_date { get; set; }

        public string issue_place { get; set; } = null!;

        public DateTime? expiration_date { get; set; }

        [MaxLength(255)]
        public string? file_url { get; set; }

        [MaxLength(20)] 
        public string? validation_status { get; set; }

        public DateTime? validation_date { get; set; }

        public string? notes { get; set; }
    }
}