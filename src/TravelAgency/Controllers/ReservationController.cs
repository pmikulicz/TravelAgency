using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Models;
using TravelAgency.Storage;

namespace TravelAgency.Controllers
{
    [Authorize]
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

        [Authorize("Admin")]
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

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation == null)
                return NotFound();

            return View(new ReservationViewModel
            {
                Id = id,
                User = reservation.User,
                Comment = reservation.Comment,
                TourId = reservation.TourId
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ReservationViewModel reservation)
        {
            var reservationToUpdate = new Reservation
            {
                Id = reservation.Id,
                User = reservation.User,
                Comment = reservation.Comment,
                TourId = reservation.TourId
            };
            _context.Attach(reservationToUpdate);
            _context.Update(reservationToUpdate);
            await _context.SaveChangesAsync();

            return RedirectToAction("ListAll");
        }

        [Authorize("Admin")]
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var reservationToDelete = await _context.Reservations.FindAsync(id);

            if (reservationToDelete == null)
                return NotFound();

            _context.Remove(reservationToDelete);
            await _context.SaveChangesAsync();

            return RedirectToAction("ListAll");
        }

        [HttpGet]
        public async Task<IActionResult> My(string id)
        {
            var reservations = await _context.Reservations.Where(r => r.User.Equals(id)).ToListAsync();

            return View(reservations.Select(r => new ReservationViewModel
            {
                User = r.User,
                Comment = r.Comment,
                CreationDate = r.CreationDate
            }));
        }
    }
}