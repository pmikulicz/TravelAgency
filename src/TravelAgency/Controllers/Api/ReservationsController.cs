using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using TravelAgency.Domain.Model;
using TravelAgency.Storage;

namespace TravelAgency.Controllers.Api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public sealed class ReservationsController : CrudController
    {
        private readonly TravelAgencyDbContext _context;

        public ReservationsController(TravelAgencyDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(Reservation reservation)
        {
            if (reservation == null) throw new ArgumentNullException(nameof(reservation));

            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtRoute(new { id = reservation.Id }, reservation);
        }

        [Authorize("Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Reservation reservation)
        {
            if (reservation == null) throw new ArgumentNullException(nameof(reservation));

            _context.Attach(reservation);
            _context.Update(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtRoute(new { id = reservation.Id }, reservation);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) =>
            Single(await _context.Reservations.FindAsync(id));

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Collection(await _context.Reservations.ToListAsync());
    }
}