using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contract;
using MassTransit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentService.Data;
using StudentService.DTO;
using StudentService.Entities;

namespace StudentService.Contoller;


[ApiController]
[Route("api/auctions")]
public class AuctionController :  ControllerBase {
    private readonly AuctionDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;

    public AuctionController(AuctionDbContext context, IMapper mapper, IPublishEndpoint publishEndpoint){
        this._context = context;
        this._mapper = mapper;
        this._publishEndpoint = publishEndpoint;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuction(string date){
        var query = _context.Auctions.OrderBy(x =>x.Item.Make).AsQueryable();

        if(!string.IsNullOrEmpty(date)){
            query = query.Where(x => x.UpdatedAt.Value.CompareTo(DateTime.Parse(date).ToUniversalTime()) > 0);
        }
        var auction = await _context.Auctions
                           .Include(x => x.Item)
                           .OrderBy(x => x.Item.Make)
                           .ToListAsync();
        
        return await query.ProjectTo<AuctionDto>(_mapper.ConfigurationProvider).ToListAsync();
        // return _mapper.Map<List<AuctionDto>>(auction);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAllAuctionById(Guid id){
        var auction = await _context.Auctions
                           .Include(x => x.Item)
                            .FirstOrDefaultAsync(x => x.Id == id);
        if(auction == null) return null;
        
        return _mapper.Map<AuctionDto>(auction);
    }


    [Authorize]
    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto){
        var auction = _mapper.Map<Auction>(auctionDto);

        auction.SellerId = User.Identity.Name;

        _context.Auctions.Add(auction);
        var newAuction = _mapper.Map<AuctionDto>(auction);
        await _publishEndpoint.Publish(_mapper.Map<AuctionCreated>(newAuction));
        var result = await _context.SaveChangesAsync() > 0;
    
        if(!result) return BadRequest("Could not create request");
        return CreatedAtAction(nameof(GetAllAuctionById), new{auction.Id}, newAuction);
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult<AuctionDto>> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto){
        var auction = await _context.Auctions
                           .Include(x => x.Item)
                            .FirstOrDefaultAsync(x => x.Id == id);
        if(auction == null) return NotFound();

        if(auction.SellerId !=User.Identity.Name) return Forbid();
        // check seller is user, TODO
        auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
        auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
        auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
        auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
        auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;

        await _publishEndpoint.Publish(_mapper.Map<AuctionUpdated>(auction));

        var result = await _context.SaveChangesAsync() > 0;
        if(result) return Ok();
        return BadRequest("Problem");
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult<AuctionDto>> DeleteUction(Guid id, UpdateAuctionDto updateAuctionDto){
        var auction = await _context.Auctions
                           .Include(x => x.Item)
                            .FirstOrDefaultAsync(x => x.Id == id);
        if(auction == null) return NotFound();

        if(auction.SellerId !=User.Identity.Name) return Forbid();
        // check seller is user, TODO
        _context.Auctions.Remove(auction);
        await _publishEndpoint.Publish<AuctionDeleted>(new {Id = auction.Id.ToString()});
        var result = await _context.SaveChangesAsync() > 0;
        if(!result) return BadRequest("Could not delete");
        return Ok();
    }
}