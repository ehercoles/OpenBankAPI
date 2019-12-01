using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Conta.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conta.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovimentacaoController : ControllerBase
    {
        private readonly MovimentacaoContext _context;

        public MovimentacaoController(MovimentacaoContext context)
        {
            _context = context;
        }

        // GET: api/Movimentacao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movimentacao>>> GetMovimentacao()
        {
            return await _context.Movimentacoes.ToListAsync();
        }

        // GET: api/Movimentacao/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Movimentacao>> GetMovimentacao(int id)
        {
            var movimentacao = await _context.Movimentacoes.FindAsync(id);

            if (movimentacao == null)
            {
                return NotFound();
            }

            return movimentacao;
        }

        // PUT: api/Movimentacao/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovimentacao(int id, Movimentacao movimentacao)
        {
            if (id != movimentacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(movimentacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovimentacaoExists(id))
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

        // POST: api/Movimentacao
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Movimentacao>> PostMovimentacao(Movimentacao movimentacao)
        {
            _context.Movimentacoes.Add(movimentacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovimentacao), new { id = movimentacao.Id }, movimentacao);
        }

        // DELETE: api/Movimentacao/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Movimentacao>> DeleteMovimentacao(int id)
        {
            var movimentacao = await _context.Movimentacoes.FindAsync(id);
            if (movimentacao == null)
            {
                return NotFound();
            }

            _context.Movimentacoes.Remove(movimentacao);
            await _context.SaveChangesAsync();

            return movimentacao;
        }

        private bool MovimentacaoExists(int id)
        {
            return _context.Movimentacoes.Any(e => e.Id == id);
        }
    }
}
