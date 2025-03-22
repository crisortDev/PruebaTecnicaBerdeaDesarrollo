using Microsoft.EntityFrameworkCore;
using PruebaTecnicaBerdea.Core.Domain;
using PruebaTecnicaBerdea.Data;

var builder = WebApplication.CreateBuilder(args);

// Registrar ApplicationDbContext en el contenedor de dependencias.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar HttpClient en el contenedor de dependencias.
builder.Services.AddHttpClient();

// Registrar IPokemonRepository y su implementación concreta.
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();

// Registrar SearchPokemonUseCase en el contenedor de dependencias.
builder.Services.AddScoped<SearchPokemonUseCase>();

// Registrar servicios de controladores con vistas.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuración del pipeline de solicitudes HTTP.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();