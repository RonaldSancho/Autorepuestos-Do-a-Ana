using Autorepuestos.Interfaces;
using Autorepuestos.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
//builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUsuariosModel, UsuariosModel>();
builder.Services.AddScoped<IPedidosModel, PedidosModel>();
builder.Services.AddScoped<ICatalogosModel, CatalogosModel>();
builder.Services.AddScoped<IEntregasModel, EntregasModel>();
builder.Services.AddScoped<ICarritoModel, CarritoModel>();
builder.Services.AddScoped<IProductosModel, ProductosModel>();
builder.Services.AddScoped<IFacturaModel, FacturaModel>();
builder.Services.AddScoped<IErroresModel, ErroresModel>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

IWebHostEnvironment env = app.Environment;
Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, "../Rotativa");

app.Run();
