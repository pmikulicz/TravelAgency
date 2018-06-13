using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Domain.Model;
using TravelAgency.Models;
using TravelAgency.Storage;

namespace TravelAgency.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ReservationController : Controller
    {
        private readonly TravelAgencyDbContext _context;

        public ReservationController(TravelAgencyDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ReservationAddModel reservation)
        {
            await _context.Reservations.AddAsync(new Reservation
            {
                CreationDate = DateTime.Now,
                TourId = reservation.TourId,
                User = reservation.User,
                Comment = reservation.Comment
            });

            await _context.SaveChangesAsync();

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            var reservations = await _context.Reservations.ToListAsync();

            return View(reservations.Select(r => new ReservationViewModel
            {
                Id = r.Id,
                Comment = r.Comment,
                User = r.User,
                TourId = r.TourId,
                CreationDate = r.CreationDate
            }));
        }
    }
}