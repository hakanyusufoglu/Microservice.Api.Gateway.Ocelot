using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthorization(options=>options.AddPolicy("AdminPolicy",policy=>policy.RequireClaim("Role","Admin")));
//jwt 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.TokenValidationParameters = new()
        {

            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Token:Issuer"],
            ValidAudience = builder.Configuration["Token:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
            ClockSkew = TimeSpan.Zero

        };
    });


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

//LoadBalancer denemek için yorum satýrýna alýndý.
//app.MapGet("/", () => "Hello World! - Api 1 " + args[0])
//    .RequireAuthorization("AdminPolicy");

app.MapGet("/", () => "Hello World! - Api 1 " + args[0]);

app.Run();
