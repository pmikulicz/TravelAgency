using System;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models
{
    public class TourViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [Required]
        public int MaxResrvations { get; set; }

        [Required]
        public int CurrentReservations { get; set; }
    }
}