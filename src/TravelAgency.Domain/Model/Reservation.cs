using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;

namespace TravelAgency.Domain.Model
{
    public class Reservation : IIdentity
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

        public virtual Tour Tour { get; set; }
    }
}