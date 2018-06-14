using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Models;
using TravelAgency.Storage;

namespace TravelAgency.Controllers
{
    [Authorize]
    public sealed class TourController : Controller
    {
        private readonly TravelAgencyDbContext _context;

        public TourController(TravelAgencyDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            Tour tour = await _context.Tours.FindAsync(id);

            if (tour == null)
                return NotFound();

            int currentReservations = _context.Reservations.Count(r => r.TourId == tour.Id);

            return View(new TourViewModel
            {
                Id = id,
                Country = tour.Country,
                Description = tour.Description,
                EndDate = tour.EndDate,
                MaxResrvations = tour.MaxResrvations,
                StartDate = tour.StartDate,
                CurrentReservations = currentReservations
            });
        }

        [Authorize("Admin")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Authorize("Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(TourViewModel tour)
        {
            EntityEntry<Tour> entityTour = await _context.Tours.AddAsync(new Tour
            {
                Country = tour.Country,
                Description = tour.Description,
                EndDate = tour.EndDate,
                MaxResrvations = tour.MaxResrvations,
                StartDate = tour.StartDate
            });

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { id = entityTour.Entity.Id });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(TourSearchModel searchCriteria)
        {
            List<Tour> tours = await _context.Tours.Where(t => t.Country == searchCriteria.Country).ToListAsync();

            return View("List", tours.Select(t => new TourViewModel
            {
                Country = t.Country,
                StartDate = t.StartDate,
                EndDate = t.EndDate,
                Description = t.Description,
                Id = t.Id,
                MaxResrvations = t.MaxResrvations
            }));
        }

        [HttpGet]
        public IActionResult Search()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListAll()
        {
            var tours = await _context.Tours.ToListAsync();

            return View(tours.Select(t => new TourViewModel
            {
                Id = t.Id,
                StartDate = t.StartDate,
                Description = t.Description,
                Country = t.Country,
                MaxResrvations = t.MaxResrvations,
                EndDate = t.EndDate
            }));
        }

        [Authorize("Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Tour tour = await _context.Tours.FindAsync(id);

            if (tour == null)
                return NotFound();

            int currentReservations = _context.Reservations.Count(r => r.TourId == tour.Id);

            return View(new TourViewModel
            {
                Id = id,
                Country = tour.Country,
                Description = tour.Description,
                EndDate = tour.EndDate,
                MaxResrvations = tour.MaxResrvations,
                StartDate = tour.StartDate,
                CurrentReservations = currentReservations
            });
        }

        [Authorize("Admin")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(TourViewModel tour)
        {
            var tourToUpdate = new Tour
            {
                Id = tour.Id,
                Description = tour.Description,
                Country = tour.Country,
                EndDate = tour.EndDate,
                StartDate = tour.StartDate,
                MaxResrvations = tour.MaxResrvations
            };
            _context.Attach(tourToUpdate);
            _context.Update(tourToUpdate);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", new { id = tourToUpdate.Id });
        }
    }
}