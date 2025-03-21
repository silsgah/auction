using AutoMapper;
using Contract;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService;


public class AuctionDeletedConsumers : IConsumer<AuctionDeleted>
{
     private IMapper _mapper;
    public AuctionDeletedConsumers(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<AuctionDeleted> context)
    {
       Console.WriteLine("---> Consuming auction deleted service "+ context.Message.Id);

        var result = await DB.DeleteAsync<Item>(context.Message.Id);
        if(!result.IsAcknowledged)
            throw new MessageException(typeof(AuctionCreated), "Problem deleting mongodb");
    }
}