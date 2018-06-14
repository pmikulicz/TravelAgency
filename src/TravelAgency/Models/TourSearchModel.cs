using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models
{
    public sealed class TourSearchModel
    {
        [Required]
        public string Country { get; set; }
    }
}