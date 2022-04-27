using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovoApiBarbearia.Data;
using NovoApiBarbearia.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NovoApiBarbearia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            return await _context.Cliente.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetClientes(int id)
        {
            var clientes = await _context.Cliente.FindAsync(id);
            if (clientes == null) {
                return NotFound();
            }
            return clientes;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.Id) {
                return BadRequest();
            }
            _context.Entry(cliente).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Cliente>> PostTodoItem(Cliente cliente)
        {
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetClientes", new { id = cliente.Id }, cliente);
            return CreatedAtAction(nameof(GetClientes), new { id = cliente.Id }, cliente);
        }

    }
}
