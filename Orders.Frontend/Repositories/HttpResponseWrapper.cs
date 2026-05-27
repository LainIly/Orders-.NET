using System.Net;

namespace Orders.Frontend.Repositories
{
    public class HttpResponseWrapper<T> //Notacion diamante. Clase emboltorio de respuestas de tipo T.
    {
        public HttpResponseWrapper(T? response, bool error, HttpResponseMessage httpResponseMessage) //Constructor que recibe la respuesta de tipo T, un booleano que indica si hubo un error y el mensaje de respuesta HTTP. El signo de ? indica que la respuesta puede ser nula.
        {
            Response = response;
            Error = error;
            HttpResponseMessage = httpResponseMessage;
        }

        public T? Response { get; } //get son solo lecturas.
        public bool Error { get; }
        public HttpResponseMessage HttpResponseMessage { get; }

        public async Task<string?> GetErrorMessageAsync() // ? indica que el valor de retorno puede ser nulo. Método asíncrono para obtener el mensaje de error de la respuesta HTTP.
        {
            if (!Error)
            {
                return null;
            }

            var statusCode = HttpResponseMessage.StatusCode; //Obtiene el código de estado de la respuesta HTTP.

            if (statusCode == HttpStatusCode.NotFound) //No encontrado.
            {
                return "Recurso no encontrado.";
            }

            if (statusCode == HttpStatusCode.BadRequest) // Solicitud incorrecta.
            {
                return await HttpResponseMessage.Content.ReadAsStringAsync(); ;
            }

            if (statusCode == HttpStatusCode.Unauthorized) // No autorizado.
            {
                return "Debes iniciar sesión.";
            }

            if (statusCode == HttpStatusCode.Forbidden) // Prohibido.
            {
                return "Acceso denegado.";
            }

            return "Error inesperado.";
        }
    }
}