namespace SearchService;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;

[ApiController]
[Route("api/search")]
public class SearchController: ControllerBase
{

 [HttpGet]
 public async Task<ActionResult<List<Item>>> SearchItem([FromQuery] SearchParams searchParams)
 {
    var query = DB.PagedSearch<Item, Item>();

    if(!string.IsNullOrEmpty(searchParams.SearchItem))
    {
        query.Match(Search.Full, searchParams.SearchItem).SortByTextScore();
    }
    
    query = searchParams.OrderBy switch {
      "make" => query.Sort(x => x.Ascending(a =>a.Make)),
      "new" => query.Sort(x => x.Descending(a =>a.CreatedAt)),
      _ => query.Sort(x => x.Ascending(a => a.AuctionEnd))
    };

    query = searchParams.Filterby switch {
      "finished" => query.Match(x => x.AuctionEnd < DateTime.UtcNow),
      "endingSoon" => query.Match(x =>x.AuctionEnd < DateTime.UtcNow.AddHours(6) && x.AuctionEnd > DateTime.UtcNow),
      _ => query.Match(x => x.AuctionEnd > DateTime.UtcNow)
    };

    if(!string.IsNullOrEmpty(searchParams.SellerId)){
      Console.WriteLine("Seller value " + searchParams.SellerId);
      query.Match(x => x.sellerId.ToLower() == searchParams.SellerId.ToLower());
    }
    if(!string.IsNullOrEmpty(searchParams.WinnerId)){
      query.Match(x => x.winnerId == searchParams.WinnerId);
    }

    query.PageNumber(searchParams.PageNumber);
    query.PageSize(searchParams.PageSize);

    var result = await query.ExecuteAsync();

    return Ok(new
        {
          results = result.Results,
          pageCount = result.PageCount,
          totalCount = result.TotalCount
        });
  }
}