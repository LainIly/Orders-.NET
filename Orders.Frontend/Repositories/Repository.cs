using System.Text;
using System.Text.Json;

namespace Orders.Frontend.Repositories
{
    public class Repository : IRepository
    {
        private readonly HttpClient _httpClient; //Atributo privado de solo lectura para realizar solicitudes HTTP. Solo se puede asignar en el constructor.

        private JsonSerializerOptions _jsonSerializerOptions => new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true, //Mapear las propiedades de la respuesta JSON sin importar mayúsculas o minúsculas.
                                                //Esto es útil para evitar problemas de deserialización cuando los nombres de las propiedades en el JSON no coinciden exactamente con los nombres de las propiedades en las clases C#.
        };

        public Repository(HttpClient httpClient) //Inyección de dependencias para recibir una instancia de HttpClient. El constructor asigna el HttpClient al atributo privado.
        {
            _httpClient = httpClient; //Asignación del HttpClient al atributo privado.
        }

        public async Task<HttpResponseWrapper<T>> GetAsync<T>(string url)//T representa cualquier tipo de datos que se espera recibir como respuesta de la solicitud GET. El método es asíncrono y devuelve una tarea que envuelve una respuesta HTTP personalizada.
        {
            var responseHttp = await _httpClient.GetAsync(url); //Realiza una solicitud GET a la URL especificada y espera la respuesta.

            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await UnserializeAnswer<T>(responseHttp);
                return new HttpResponseWrapper<T>(response, false, responseHttp); // Si la respuesta HTTP indica éxito (código de estado 2xx), se deserializa el contenido de la respuesta en un objeto del tipo T utilizando el método UnserializeAnswer.
                                                                                  // Luego, se crea y devuelve una instancia de HttpResponseWrapper con la respuesta deserializada, indicando que no hubo error.
            }

            return new HttpResponseWrapper<T>(default, true, responseHttp); // Si la respuesta HTTP no indica éxito, se devuelve una instancia de HttpResponseWrapper con un valor predeterminado para el tipo T, indicando que hubo un error.
        }

        public async Task<HttpResponseWrapper<object>> PostAsync<T>(string url, T model) //Post que no devuelve respuesta. T representa el tipo de datos que se enviará en la solicitud POST.
                                                                                         //El método es asíncrono y devuelve una tarea que envuelve una respuesta HTTP personalizada con un tipo de respuesta genérico (object).
        {
            var messageJson = JsonSerializer.Serialize(model); //Serializa el modelo de datos en una cadena JSON utilizando el serializador JSON. Lo vuelve string para enviarlo en el cuerpo de la solicitud POST.
            var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, messageContent); // Realiza una solicitud POST a la URL especificada con el contenido JSON y espera la respuesta.

            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp); //Devuelve objecto nulo. El valor de error se determina por el código de estado de la respuesta HTTP, indicando que hubo un error si el código no es exitoso.
        }

        public async Task<HttpResponseWrapper<TActionResponse>> PostAsync<T, TActionResponse>(string url, T model) //Post que devuelve respuesta. T representa el tipo de datos que se enviará en la solicitud POST.
                                                                                                                   //El método es asíncrono y devuelve una tarea que envuelve una respuesta HTTP personalizada con un tipo de respuesta genérico (object).
        {
            var messageJson = JsonSerializer.Serialize(model); //Serializa el modelo de datos en una cadena JSON utilizando el serializador JSON. Lo vuelve string para enviarlo en el cuerpo de la solicitud POST.
            var messageContent = new StringContent(messageJson, Encoding.UTF8, "application/json");

            var responseHttp = await _httpClient.PostAsync(url, messageContent); // Realiza una solicitud POST a la URL especificada con el contenido JSON y espera la respuesta.

            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await UnserializeAnswer<TActionResponse>(responseHttp); // Si la respuesta HTTP indica éxito (código de estado 2xx), se deserializa el contenido de la respuesta en un objeto del tipo TActionResponse utilizando el método UnserializeAnswer.
                                                                                       // Luego, se crea y devuelve una instancia de HttpResponseWrapper con la respuesta deserializada, indicando que no hubo error.   
                return new HttpResponseWrapper<TActionResponse>(response, false, responseHttp); // 
                                                                                  //
            }
            return new HttpResponseWrapper<TActionResponse>(default, true, responseHttp);
        }

        private async Task<T> UnserializeAnswer<T>(HttpResponseMessage responseHttp)
        {
            var response = await responseHttp.Content.ReadAsStringAsync(); //Lee el contenido de la respuesta HTTP como una cadena de texto.
            return JsonSerializer.Deserialize<T>(response, _jsonSerializerOptions)!; //Deserializa la cadena JSON en un objeto del tipo T utilizando las opciones de deserialización definidas.
                                                                                     //El operador de supresión de nulabilidad (!) se utiliza para indicar que se espera que el resultado no sea nulo.
        }
    }
}