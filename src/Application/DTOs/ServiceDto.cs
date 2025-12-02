namespace Application.DTOs
{
    public class ServiceDto
    {
        public long ServiceId { get; set; }

        public string RequesterId { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public string ServiceLocation { get; set; } = string.Empty;

        public string ServiceDescription { get; set; } = string.Empty;
    }
}
