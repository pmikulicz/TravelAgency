using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Domain.Model
{
    public class Tour : IIdentity
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int MaxResrvations { get; set; }

        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}