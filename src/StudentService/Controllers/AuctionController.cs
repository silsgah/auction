using AutoMapper;
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

    public AuctionController(AuctionDbContext context, IMapper mapper){
        this._context = context;
        this._mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<AuctionDto>>> GetAllAuction(){
        var auction = await _context.Auctions
                           .Include(x => x.Item)
                           .OrderBy(x => x.Item.Make)
                           .ToListAsync();

        return _mapper.Map<List<AuctionDto>>(auction);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AuctionDto>> GetAllAuctionById(Guid id){
        var auction = await _context.Auctions
                           .Include(x => x.Item)
                            .FirstOrDefaultAsync(x => x.Id == id);
        if(auction == null) return null;
        
        return _mapper.Map<AuctionDto>(auction);
    }

    [HttpPost]
    public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto){
        var auction = _mapper.Map<Auction>(auctionDto);
        // TODO check current user
        auction.SellerId = "test";

        _context.Auctions.Add(auction);
        var result = await _context.SaveChangesAsync() > 0;
        if(!result) return BadRequest("Could not create request");
        return CreatedAtAction(nameof(GetAllAuctionById), new{auction.Id}, _mapper.Map<AuctionDto>(auction));
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<AuctionDto>> UpdateAuction(Guid id, UpdateAuctionDto updateAuctionDto){
        var auction = await _context.Auctions
                           .Include(x => x.Item)
                            .FirstOrDefaultAsync(x => x.Id == id);
        if(auction == null) return NotFound();
        // check seller is user, TODO
        auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
        auction.Item.Model = updateAuctionDto.Model ?? auction.Item.Model;
        auction.Item.Color = updateAuctionDto.Color ?? auction.Item.Color;
        auction.Item.Make = updateAuctionDto.Make ?? auction.Item.Make;
        auction.Item.Year = updateAuctionDto.Year ?? auction.Item.Year;
        
         var result = await _context.SaveChangesAsync() > 0;
        if(result) return Ok();
        return BadRequest("Problem");
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<AuctionDto>> DeleteUction(Guid id, UpdateAuctionDto updateAuctionDto){
        var auction = await _context.Auctions
                           .Include(x => x.Item)
                            .FirstOrDefaultAsync(x => x.Id == id);
        if(auction == null) return NotFound();
        // check seller is user, TODO
        _context.Auctions.Remove(auction);
        var result = await _context.SaveChangesAsync() > 0;
        if(result) return Ok();
        return BadRequest("Problem");
    }
}