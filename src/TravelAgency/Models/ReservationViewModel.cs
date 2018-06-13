using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models
{
    public class ReservationViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string User { get; set; }

        public string Comment { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }

        [Required]
        public int TourId { get; set; }
    }
}