using AutoMapper;
using Contract;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace AearchService;

public class AuctionCreatedConsumers : IConsumer<AuctionCreated>
{
    private IMapper _mapper;
    public AuctionCreatedConsumers(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<AuctionCreated> context)
    {
       Console.WriteLine("---> Consuming auction service "+ context.Message.Id);
       
       var item = _mapper.Map<Item>(context.Message);

       if (item.Model == "Foo") throw new ArgumentException("Cannot sell cars with name of Foo");
       await item.SaveAsync();
    }
}