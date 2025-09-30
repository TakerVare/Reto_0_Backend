using Microsoft.AspNetCore.Mvc;
using Reto_0_Backend.Models;

namespace Reto_0_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class EventosController : ControllerBase
    {

        private static readonly List<Evento> Eventos = new()
        {
            new Evento { Id = 1, Nombre = "Concierto de Rock" },
            new Evento { Id = 2, Nombre = "Feria de Tecnología" },
            new Evento { Id = 3, Nombre = "Exposición de Arte" }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Evento>> GetEventos()
        {
            return Ok(Eventos);
        }

        [HttpGet("/eventos/{id}")]
        public ActionResult<Evento> GetEvento(int id)
        {
            var evento = Eventos.FirstOrDefault(e => e.Id == id);
            if (evento == null)
            {
                return NotFound();
            }
            return Ok(evento);
        }

    }

}