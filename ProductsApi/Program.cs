using ProductsApi.Data;
// Removed invalid using directive
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("ProductsDb"));
    
var googleClientId = "your-google-client-id"; // Replace with your actual Google Client ID

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://accounts.google.com";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // Varmistaa, että token on Googlen myöntämä
            ValidIssuer = "https://accounts.google.com",
            ValidateAudience = true, // Varmistaa, että token on tarkoitettu tälle API:lle
            ValidAudience = googleClientId, // Aseta Google Client ID tähän
            ValidateLifetime = true // Varmistaa, että token ei ole vanhentunut
        };
    });

app.UseAuthentication(); // Käynnistää autentikointijärjestelmän
app.UseAuthorization(); // Tarkistaa, onko käyttäjällä oikeudet