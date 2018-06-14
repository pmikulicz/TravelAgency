using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models
{
    public class ReservationAddModel
    {
        [Required]
        public int TourId { get; set; }

        [Required]
        public string User { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}