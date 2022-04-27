using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NovoApiBarbearia.Data;
using NovoApiBarbearia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovoApiBarbearia.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProduto()
        {
            return await _context.Produto.ToListAsync();
        }

        // GET: api/Produtos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _context.Produto.FindAsync(id);
            if (produto == null) {
                return NotFound();
            }
            return produto;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduto(int id, Produto produto)
        {
            if (id != produto.Id) {
                return BadRequest();
            }
            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            _context.Produto.Add(produto);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetProduto", new { id = produto.Id }, produto);
            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Produto>> RemoveProduto(long id, Produto produto)
        {
            _context.Produto.Remove(produto);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("RemoveProduto", new { id = produto.Id }, produto);
            return CreatedAtAction(nameof(RemoveProduto), new { id = produto.Id }, produto);

        }

    }
}
