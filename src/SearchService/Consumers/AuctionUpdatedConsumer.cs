using AutoMapper;
using Contract;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService;


public class AuctionUpdatedConsumers : IConsumer<AuctionUpdated>
{
    private IMapper _mapper;
    public AuctionUpdatedConsumers(IMapper mapper)
    {
        _mapper = mapper;
    }
    public async Task Consume(ConsumeContext<AuctionUpdated> context)
    {
       Console.WriteLine("---> Consuming auction updated service "+ context.Message.Id);
       var item = _mapper.Map<Item>(context.Message);
       var result = await DB.Update<Item>()
                          .Match(a => a.ID == context.Message.Id)
                          .ModifyOnly(x => new
                          {
                            x.Color,
                            x.Make,
                            x.Model,
                            x.Year,
                            x.Mileage
                          }, item)
                          .ExecuteAsync();

        if(!result.IsAcknowledged)
            throw new MessageException(typeof(AuctionCreated), "Problem updating mongodb");
    }
}