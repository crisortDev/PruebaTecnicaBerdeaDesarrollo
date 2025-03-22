using Microsoft.EntityFrameworkCore;
using PruebaTecnicaBerdea.Core.Domain;
using PruebaTecnicaBerdea.Data;
using System;

var builder = WebApplication.CreateBuilder(args);

// Registrar DbContext (si estás usando Entity Framework)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar Repositorio y Caso de Uso
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddScoped<SearchPokemonUseCase>();

// Registrar Controladores (si no está ya configurado)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar el middleware
app.UseRouting();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();