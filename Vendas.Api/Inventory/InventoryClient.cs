using System.Net.Http.Json;

namespace Vendas.Api.Inventory;

public interface IInventoryClient
{
    Task<ProdutoDto?> ObterProduto(int id, string bearerToken);
    Task<bool> BaixarEstoque(int id, int quantidade, string bearerToken);
}

public class InventoryClient : IInventoryClient
{
    private readonly HttpClient _http;
    private readonly IConfiguration _cfg;

    public InventoryClient(HttpClient http, IConfiguration cfg)
    {
        _http = http;
        _cfg = cfg;
        _http.BaseAddress = new Uri(_cfg["InventoryApi:BaseUrl"]!);
    }

    public async Task<ProdutoDto?> ObterProduto(int id, string bearerToken)
    {
        using var req = new HttpRequestMessage(HttpMethod.Get, $"api/produtos/{id}");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
        var res = await _http.SendAsync(req);
        if (!res.IsSuccessStatusCode) return null;
        return await res.Content.ReadFromJsonAsync<ProdutoDto>();
    }

    public async Task<bool> BaixarEstoque(int id, int quantidade, string bearerToken)
    {
        using var req = new HttpRequestMessage(HttpMethod.Post, $"api/produtos/{id}/baixar-estoque");
        req.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
        req.Content = JsonContent.Create(quantidade);
        var res = await _http.SendAsync(req);
        return res.IsSuccessStatusCode;
    }
}
