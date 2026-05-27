using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Orders.Frontend;
using Orders.Frontend.Repositories;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7095/") });
builder.Services.AddScoped<IRepository, Repository>(); //Inyección de dependencias para el repositorio.
                                                       //Esto permite que cualquier componente que necesite una instancia de IRepository reciba una instancia de Repository automáticamente.
                                                       //Solo obtiene respuesta del Repositorio, no de la API directamente.
                                                       //El repositorio se encarga de manejar las solicitudes HTTP y las respuestas, proporcionando una capa de abstracción entre los componentes y la API.

await builder.Build().RunAsync();
