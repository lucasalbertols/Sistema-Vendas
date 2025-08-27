namespace Vendas.Api.Models;

public class Pedido
{
    public int Id { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public string Status { get; set; } = "Pendente";
    public List<ItemPedido> Itens { get; set; } = new();
}

public class ItemPedido
{
    public int Id { get; set; }
    public int ProdutoId { get; set; }
    public int Quantidade { get; set; }

    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; } = null!;
}
