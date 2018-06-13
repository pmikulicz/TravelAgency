using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Domain.Model;

namespace TravelAgency.Controllers.Api
{
    public abstract class CrudController : ControllerBase
    {
        protected IActionResult Single<T>(T entity)
        {
            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        protected IActionResult Collection<T>(IList<T> entities)
        {
            if (entities == null)
                return NotFound();

            return Ok(!entities.Any() ? Enumerable.Empty<T>() : entities);
        }

        protected async Task<IActionResult> Add<T>(T entity, Func<T, Task> addAction)
            where T : IIdentity
        {
            await addAction(entity);

            return CreatedAtRoute(new { id = entity.Id }, entity);
        }

        protected async Task<IActionResult> Update<T>(int id, T entity, Func<int, T, Task> updateAction)
            where T : IIdentity
        {
            await updateAction(id, entity);

            return CreatedAtRoute(new { id = entity.Id }, entity);
        }
    }
}