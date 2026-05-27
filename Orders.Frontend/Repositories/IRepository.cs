namespace Orders.Frontend.Repositories
{
    public interface IRepository
    {
        Task<HttpResponseWrapper<T>> GetAsync<T>(string url); // Metodo asinctrono Get. Parametro Url de mi controlador. Devuelve un HttpResponseWrapper con el tipo de dato que se le indique al llamar el metodo.
        Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model); //Post que no devuelve respuestas.
        Task<HttpResponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model); //Post que devuelve respuestas.
    }
}
