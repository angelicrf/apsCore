using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlsAndRoutes.Models;

namespace UrlsAndRoutes.Controllers
{
        [Route("api/[controller]")]
        public class ContentController : Controller
        {
            [HttpGet("string")]
            public string GetString() => "This is a string response";

            [HttpGet("object/{format?}")]
            [FormatFilter]
            //[Produces("application/json", "application/xml")]
        public Reservation GetObject() => new Reservation
            {
                ReservationId = 100,
                ClientName = "Joe",
                Location = "Board Room"
            };
        [HttpPost]
        [Consumes("application/json")]
        public Reservation ReceiveJson([FromBody] Reservation reservation)
        {
            return reservation;
        }
        [HttpPost]
        [Consumes("application/xml")]
        public Reservation ReceiveXml([FromBody] Reservation reservation)
        {
            return reservation;
        }
    }
    }

