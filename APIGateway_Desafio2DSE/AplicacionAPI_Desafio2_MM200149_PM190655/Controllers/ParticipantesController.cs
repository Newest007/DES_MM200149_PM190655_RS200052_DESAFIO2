using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AplicacionAPI_Desafio2_MM200149_PM190655.Models;
using System.CodeDom;
using StackExchange.Redis;
using System.Text.Json;

namespace AplicacionAPI_Desafio2_MM200149_PM190655.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantesController : ControllerBase
    {
        private readonly ConexionContext _context;
        private readonly IConnectionMultiplexer _redis;
        
        public ParticipantesController(ConexionContext context, IConnectionMultiplexer redis)
        {
            _context = context;
            _redis = redis;
        }

        // GET: api/Participantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Participante>>> GetParticipantes()
        {
            var db = _redis.GetDatabase();
            string cacheKey = "participanteList";
            var participantesCache = await db.StringGetAsync(cacheKey);
            if (!participantesCache.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<List<Participante>>(participantesCache);
            }
            var participantes = await _context.Participantes.ToListAsync();
            await db.StringSetAsync(cacheKey, JsonSerializer.Serialize(participantes), TimeSpan.FromMinutes(10));

            return participantes;
        }

        // GET: api/Participantes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Participante>> GetParticipante(int id)
        {
            var db = _redis.GetDatabase();
            string cacheKey = "participante_" + id.ToString();
            var participanteCache = await db.StringGetAsync(cacheKey);
            if (!participanteCache.IsNullOrEmpty)
            {
                return JsonSerializer.Deserialize<Participante>(participanteCache);
            }

            var participante = await _context.Participantes.FindAsync(id);
            if (participante == null)
            {
                return NotFound();
            }
            await db.StringSetAsync(cacheKey, JsonSerializer.Serialize(participante), TimeSpan.FromMinutes(10));
            return participante;
        }

        // PUT: api/Participantes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutParticipante(int id, Participante participante)
        {
            if (id != participante.ParticipanteId)
            {
                return BadRequest();
            }

            _context.Entry(participante).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                var db = _redis.GetDatabase();
                string cacheKeyParticipante = "participante_" + id.ToString();
                string cacheKeyList = "participanteList";
                await db.KeyDeleteAsync(cacheKeyParticipante);
                await db.KeyDeleteAsync(cacheKeyList);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipanteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Participantes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Participante>> PostParticipante(Participante participante)
        {
            _context.Participantes.Add(participante);
            await _context.SaveChangesAsync();
            var db = _redis.GetDatabase();
            string cacheKeyList = "participanteList";
            await db.KeyDeleteAsync(cacheKeyList);
            return CreatedAtAction("GetParticipante", new { id = participante.ParticipanteId }, participante);
        }

        // DELETE: api/Participantes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParticipante(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);
            if (participante == null)
            {
                return NotFound();
            }

            _context.Participantes.Remove(participante);
            await _context.SaveChangesAsync();
            var db = _redis.GetDatabase();
            string cacheKeyParticipante = "participante_" + id.ToString();
            string cacheKeyList = "participanteList";
            await db.KeyDeleteAsync(cacheKeyParticipante);
            await db.KeyDeleteAsync(cacheKeyList);
            return NoContent();
        }

        private bool ParticipanteExists(int id)
        {
            return _context.Participantes.Any(e => e.ParticipanteId == id);
        }
    }
}
