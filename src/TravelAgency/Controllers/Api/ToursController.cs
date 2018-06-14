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
    public class ToursController : CrudController
    {
        private readonly TravelAgencyDbContext _context;

        public ToursController(TravelAgencyDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [Authorize("Admin")]
        [HttpPost]
        public async Task<IActionResult> Post(Tour tour)
        {
            if (tour == null) throw new ArgumentNullException(nameof(tour));

            await _context.Tours.AddAsync(tour);
            await _context.SaveChangesAsync();

            return CreatedAtRoute(new { id = tour.Id }, tour);
        }

        [Authorize("Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Tour tour)
        {
            if (tour == null) throw new ArgumentNullException(nameof(tour));

            _context.Attach(tour);
            _context.Update(tour);
            await _context.SaveChangesAsync();

            return CreatedAtRoute(new { id = tour.Id }, tour);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) =>
            Single(await _context.Tours.FindAsync(id));

        [HttpGet]
        public async Task<IActionResult> Get() =>
            Collection(await _context.Tours.ToListAsync());
    }
}