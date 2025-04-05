using System.Net;
using AearchService;
using MassTransit;
using Polly;
using Polly.Extensions.Http;
using SearchService;
using SearchService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient<AuctionSvcHttpClient>().AddPolicyHandler(GetPolicy());
builder.Services.AddMassTransit(x =>{
  x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumers>();
  x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search", false));
  x.UsingRabbitMq((context, cfg) => {
    cfg.Host(builder.Configuration["RabbitMq:Host"], "/", host=> {
      host.Username(builder.Configuration.GetValue("RabbitMq:Username","guest"));
      host.Password(builder.Configuration.GetValue("RabbitMq:Password","guest"));
    });
    cfg.ReceiveEndpoint("search-auction-created", e =>{
      e.UseMessageRetry(r =>r.Interval(5,5));
      e.ConfigureConsumer<AuctionCreatedConsumers>(context);
    });
     cfg.ConfigureEndpoints(context);
  });
});
var app = builder.Build();

app.UseAuthorization();
app.MapControllers();
app.Lifetime.ApplicationStarted.Register(async () => {
try{
  await DbInitializer.InitDb(app);
}catch(Exception e){
 Console.WriteLine("Error processing files", e.Message);
}
});


app.Run();

static IAsyncPolicy<HttpResponseMessage> GetPolicy()
  =>HttpPolicyExtensions
  .HandleTransientHttpError()
  .OrResult(msg => msg.StatusCode == HttpStatusCode.NotFound)
  .WaitAndRetryForeverAsync(_ =>TimeSpan.FromSeconds(3));