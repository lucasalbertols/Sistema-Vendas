using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vendas.Api.Data;
using Vendas.Api.Inventory;
using Vendas.Api.Models;

namespace Vendas.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // exige JWT
public class PedidosController : ControllerBase
{
    private readonly VendasDbContext _db;
    private readonly IInventoryClient _inventory;

    public PedidosController(VendasDbContext db, IInventoryClient inventory)
    {
        _db = db;
        _inventory = inventory;
    }

    // POST api/pedidos
    [HttpPost]
    public async Task<ActionResult<Pedido>> CriarPedido([FromBody] CriarPedidoRequest req)
    {
        if (req.Itens is null || !req.Itens.Any())
            return BadRequest("Pedido sem itens");

        // 1) Obter token do header (vamos reutilizar para chamar o Estoque.Api)
        var bearer = HttpContext.Request.Headers.Authorization.ToString().Replace("Bearer ", "", StringComparison.OrdinalIgnoreCase);
        if (string.IsNullOrWhiteSpace(bearer)) return Unauthorized();

        // 2) Validar estoque de cada item
        foreach (var item in req.Itens)
        {
            var produto = await _inventory.ObterProduto(item.ProdutoId, bearer);
            if (produto is null)
                return BadRequest($"Produto {item.ProdutoId} não encontrado");

            if (produto.QuantidadeEmEstoque < item.Quantidade)
                return BadRequest($"Estoque insuficiente para o produto {item.ProdutoId}");
        }

        // 3) Persistir pedido como Confirmado
        var pedido = new Pedido
        {
            Status = "Confirmado",
            Itens = req.Itens.Select(i => new ItemPedido { ProdutoId = i.ProdutoId, Quantidade = i.Quantidade }).ToList()
        };

        _db.Pedidos.Add(pedido);
        await _db.SaveChangesAsync();

        // 4) Baixar estoque (sincronamente por enquanto)
        foreach (var item in pedido.Itens)
        {
            var ok = await _inventory.BaixarEstoque(item.ProdutoId, item.Quantidade, bearer);
            if (!ok)
            {
                // Em um sistema real, você faria compensação/saga aqui.
                pedido.Status = "Rejeitado";
                await _db.SaveChangesAsync();
                return StatusCode(502, $"Falha ao baixar estoque para o produto {item.ProdutoId}");
            }
        }

        return CreatedAtAction(nameof(ObterPedido), new { id = pedido.Id }, pedido);
    }

    // GET api/pedidos
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pedido>>> ListarPedidos()
    {
        var pedidos = await _db.Pedidos.Include(p => p.Itens).AsNoTracking().ToListAsync();
        return Ok(pedidos);
    }

    // GET api/pedidos/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Pedido>> ObterPedido(int id)
    {
        var pedido = await _db.Pedidos.Include(p => p.Itens).FirstOrDefaultAsync(p => p.Id == id);
        if (pedido is null) return NotFound();
        return Ok(pedido);
    }
}

public record CriarPedidoRequest(List<ItemPedidoRequest> Itens);
public record ItemPedidoRequest(int ProdutoId, int Quantidade);
