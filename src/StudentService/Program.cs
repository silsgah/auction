using Microsoft.EntityFrameworkCore;
using StudentService.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AuctionDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();


app.UseAuthorization();

app.MapControllers();
try{
  DbInitializer.InitDb(app);
}catch(Exception e){
    Console.WriteLine(e.Message);
}
app.Run();
