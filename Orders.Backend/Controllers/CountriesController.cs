using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //Esto para que si decido cambiar el nombre del controlador, la ruta se actualice automáticamente. Esta es la forma correcta.
    public class CountriesController : ControllerBase
    {
        private readonly DataContext _dataContext; //Inyección de dependencias para acceder a la base de datos. Atributo privado de solo lectura.
        public CountriesController(DataContext dataContext) // Parametro.
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(await _dataContext.Countries.ToListAsync());
        }

        [HttpGet("{id}")] //Parametro de ruta para obtener un país específico por su ID., Id debe coincidir con el nombre del parámetro en el método GetAsync(int id).
        public async Task<IActionResult> GetAsync(int id)
        {
            var country = await _dataContext.Countries.FindAsync(id); //Busca un país por su ID en la base de datos.
            if (country == null)
            {
                return NotFound();
            }
            return Ok(country);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync(Country country)
        {
            _dataContext.Add(country); //Agrega el país a la base de datos.
            await _dataContext.SaveChangesAsync(); //Guarda los cambios en la base de datos.
            return Ok(country); //Devuelve una respuesta HTTP 200 OK.
        }

        [HttpPut]
        public async Task<IActionResult> PutAsync(Country country)
        {
            _dataContext.Update(country);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync (int id)
        {
            var country = await _dataContext.Countries.FindAsync(id);

            if (country == null)
            {
                return NotFound();
            }

            _dataContext.Remove(country);
            await _dataContext.SaveChangesAsync();
            return NoContent();
        }
    }
}