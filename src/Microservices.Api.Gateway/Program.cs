using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

//ocelot.json DI ekliyoruz
builder.Configuration.AddJsonFile("ocelot.json");
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

await app.UseOcelot();
app.UseHttpsRedirection();

app.Run();
