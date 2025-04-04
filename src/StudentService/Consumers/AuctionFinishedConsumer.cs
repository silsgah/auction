using Contract;
using MassTransit;
using StudentService.Data;
using StudentService.Entities;

namespace StudentService;

public class AuctionFinishedConsumer : IConsumer<AuctionFinished>
{
    private readonly AuctionDbContext _context;

    public AuctionFinishedConsumer(AuctionDbContext auctionDbContext){
        _context = auctionDbContext;
    }

    public async Task Consume(ConsumeContext<AuctionFinished> context)
    {
       var auction = await _context.Auctions.FindAsync(context.Message.AuctionId);
       if(context.Message.ItemSold){
        auction.WinnerId = context.Message.Winner;
        auction.SoldAmount = context.Message.Amount;
       }

       auction.Status= auction.SoldAmount > auction.ReservePrice ? Status.Finished : Status.ReserveNotMet;

       await _context.SaveChangesAsync();

    }
}