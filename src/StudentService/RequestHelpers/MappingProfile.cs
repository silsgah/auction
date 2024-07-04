using AutoMapper;
using StudentService.DTO;
using StudentService.Entities;

namespace StudentService.RequestHelpers;

public class MappingProfile: Profile{

public MappingProfile()
{
  CreateMap<Auction, AuctionDto>().IncludeMembers(x =>x.Item);
  CreateMap<Item, AuctionDto>();
  CreateMap<CreateAuctionDto, Auction>()
  .ForMember(d =>d.Item, o =>o.MapFrom(s =>s));
  CreateMap<CreateAuctionDto, Item>();
}
}