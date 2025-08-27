namespace Vendas.Api.Inventory;

// espelha Produto do Estoque.Api
public class ProdutoDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = "";
    public string Descricao { get; set; } = "";
    public decimal Preco { get; set; }
    public int QuantidadeEmEstoque { get; set; }
}
