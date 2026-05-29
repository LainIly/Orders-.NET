using Microsoft.AspNetCore.Components;
using Orders.Frontend.Repositories;
using Orders.Shared.Entities;

namespace Orders.Frontend.Pages.Countries
{
    public partial class CountriesIndex
    {
        [Inject] private IRepository Repository { get; set; } = null!; //Inyectamos el repositorio para poder usarlo en esta pagina. Este null indica que no puede llegar nullo ese dato.

        public List<Country>? Countries { get; set; } //Puede ser nulos porque puede que no hayan paises en la base de datos.

        protected async override Task OnInitializedAsync() //inicializacion por defecto. Despues obtenemos respuesta.
        {
            await base.OnInitializedAsync(); //Solo se usa para cuando necesitas que la pagina que cargue haga algo.

            var responseHttp = await Repository.GetAsync<List<Country>>("api/countries");
            Countries = responseHttp.Response;
        }
    }
}
