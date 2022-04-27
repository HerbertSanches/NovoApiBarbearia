using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovoApiBarbearia.Data;
using NovoApiBarbearia.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NovoApiBarbearia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FuncionariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            return await _context.Funcionario.ToListAsync();
        }

        // GET: api/Funcionarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> GetFuncionario(int id)
        {
            var funcionario = await _context.Funcionario.FindAsync(id);
            if (funcionario == null) {
                return NotFound();
            }
            return funcionario;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutFuncionario(int id, Funcionario funcionario)
        {
            if (id != funcionario.Id) {
                return BadRequest();
            }
            _context.Entry(funcionario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostFuncionario(Funcionario funcionario)
        {
            _context.Funcionario.Add(funcionario);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetFuncionario", new { id = funcionario.Id }, funcionario);
            return CreatedAtAction(nameof(GetFuncionario), new { id = funcionario.Id }, funcionario);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Funcionario>> RemoveFuncionario(long id, Funcionario funcionario)
        {
            _context.Funcionario.Remove(funcionario);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetFuncionario", new { id = funcionario.Id }, funcionario);
            return CreatedAtAction(nameof(RemoveFuncionario), new { id = funcionario.Id }, funcionario);

        }
    }
}
