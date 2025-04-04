
using Contract;
using MassTransit;
using StudentService.Data;

namespace StudentService;

public class BidPlacedConsumer : IConsumer<BidPlaced>
{
     private readonly AuctionDbContext _dbcontext;

    public BidPlacedConsumer(AuctionDbContext auctionDbContext){
        _dbcontext = auctionDbContext;
    }

    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        var auction = await _dbcontext.Auctions.FindAsync(context.Message.AuctionId);
        if(auction.CurrentHighestBid==null || context.Message.BidStatus.Contains("Accepted") && context.Message.Amount >auction.CurrentHighestBid){
            auction.CurrentHighestBid = context.Message.Amount;
            await _dbcontext.SaveChangesAsync();
        }

    }
}