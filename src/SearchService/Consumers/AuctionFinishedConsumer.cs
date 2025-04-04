
using Contract;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService;

public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
{
    public async Task Consume(ConsumeContext<AuctionFinished> context)
    {
       Console.WriteLine("----->Consuming bid placed");
        var auction = await DB.Find<Item>().OneAsync(context.Message.AuctionId);
        if(context.Message.ItemSold){
         auction.winnerId = context.Message.Winner;
         auction.SoldAmount = context.Message.Amount;
        }
        auction.Status = "Finished";
        await auction.SaveAsync();
    }
}