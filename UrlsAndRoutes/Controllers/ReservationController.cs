using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using System;
using System.Collections.Generic;
using UrlsAndRoutes.Models;

namespace UrlsAndRoutes.Controllers
{
    [Route("api/[controller]")]
    public class ReservationController : Controller
    {
        private IRepository repository;
        public ReservationController(IRepository repo) {repository = repo; }
        [HttpGet]
        public IEnumerable<Reservation> Get() => repository.Reservations;

        [HttpGet("{id}")]
        public Reservation Get(int id) {

            Reservation result = repository[id];
            if (result == null)
            {
                throw new ArgumentNullException("The id cannot be null");
            }
            else
            {
                return                    
                    result;
            }
        }

        [HttpPost]
        public Reservation Post([FromBody] Reservation res) =>
                repository.AddReservation(new Reservation
                {
                    ClientName = res.ClientName,
                    Location = res.Location
                });

        [HttpPut]
        public Reservation Put([FromBody] Reservation res) => repository.UpdateReservation(res);

        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id,[FromBody] JsonPatchDocument<Reservation> patch)
        {
            Reservation res = Get(id);
            if (res != null)
            {
                patch.ApplyTo(res);
                return Ok();
            }
            return NotFound();
        }

        [HttpDelete("{id}")]
        public void Delete(int id) => repository.DeleteReservation(id);
    }
}
