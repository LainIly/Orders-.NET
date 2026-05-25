using Orders.Backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRazorPages();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(
    builder.Configuration.GetConnectionString("LocalConnection")
    )); //Conexion a la base de datos.

var app = builder.Build(); //Inyectar todo antes del build para que se configure correctamente.
app.UseCors(x => x
    .AllowAnyMethod()
    .SetIsOriginAllowed(origin => true)
    .AllowCredentials()
);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllers(); //Esto para que se puedan usar los controladores. Es necesario para que funcione la API.

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
