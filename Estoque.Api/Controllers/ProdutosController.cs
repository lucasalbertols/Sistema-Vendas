using Estoque.Api.Data;
using Estoque.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estoque.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // <- exige JWT em todos os endpoints deste controller
    public class ProdutosController : ControllerBase
    {
        private readonly EstoqueDbContext _context;

        public ProdutosController(EstoqueDbContext context)
        {
            _context = context;
        }

        // GET: api/produtos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Produto>>> GetProdutos()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }

        // GET: api/produtos/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Produto>> GetProduto(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound();
            return produto;
        }

        // POST: api/produtos
        [HttpPost]
        public async Task<ActionResult<Produto>> PostProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProduto), new { id = produto.Id }, produto);
        }

        // PUT: api/produtos/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutProduto(int id, Produto produto)
        {
            if (id != produto.Id) return BadRequest();

            _context.Entry(produto).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // POST: api/produtos/{id}/baixar-estoque
        [HttpPost("{id:int}/baixar-estoque")]
        public async Task<IActionResult> BaixarEstoque(int id, [FromBody] int quantidade)
        {
            if (quantidade <= 0) return BadRequest("Quantidade deve ser > 0");

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null) return NotFound();

            if (produto.QuantidadeEmEstoque < quantidade)
                return BadRequest("Estoque insuficiente");

            produto.QuantidadeEmEstoque -= quantidade;
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
