using AutoMapper;
using Contract;
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
  CreateMap<AuctionDto, AuctionCreated>();
  CreateMap<Auction, AuctionUpdated>().IncludeMembers(a =>a.Item);
  CreateMap<Item, AuctionUpdated>();
  }
}