using MongoDB.Entities;
using SearchService.Models;

namespace SearchService;


public class AuctionSvcHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public AuctionSvcHttpClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    public async Task<List<Item>> GetItemForSearchDb(){
        var lastUpdated = await DB.Find<Item, string>()
                                .Sort(x => x.Descending(x => x.UpdatedAt))
                                .Project(x => x.UpdatedAt.ToString())
                                .ExecuteFirstAsync();

        Console.WriteLine("Updated date value" + lastUpdated);                        
        return await _httpClient.GetFromJsonAsync<List<Item>>(_config["AuctionServiceUrl"] + "/api/auctions?date=" + lastUpdated);
    }
}