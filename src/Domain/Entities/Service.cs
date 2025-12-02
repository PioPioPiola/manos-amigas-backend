using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ManoaAmigas.Domain.Entities
{
    public class Service
    {
        [Key]
        public long service_id { get; set; }

        public string requester_id { get; set; } = string.Empty;

        public string? provider_id { get; set; } 

        public int category_id { get; set; }

        public long enrollment_id { get; set; }

        public DateTime request_date { get; set; }

        public DateTime? scheduled_date { get; set; }

        public TimeSpan? provider_proposed_time { get; set; }

        public TimeSpan? requester_proposed_time { get; set; }

        public TimeSpan? final_agreed_time { get; set; }

        public string service_location { get; set; } = string.Empty;

        public string service_description { get; set; } = string.Empty;

        public char service_status { get; set; } = 'C';

        public int? requester_rating { get; set; }

        public string? requester_comment { get; set; }

        public int? provider_rating { get; set; }

        public string? provider_comment { get; set; }

        public string? notes { get; set; }
    }
}