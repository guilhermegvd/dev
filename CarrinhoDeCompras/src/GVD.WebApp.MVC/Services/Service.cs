using GVD.WebApp.MVC.Models;
using System.Text;
using System.Text.Json;

namespace GVD.WebApp.MVC.Services
{
    public abstract class Service
    {
        protected StringContent CastToContent(object dado)
        {
            return new StringContent(
                JsonSerializer.Serialize(dado),
                Encoding.UTF8,
                "application/json");
        }

        protected  async Task<T> DeserializarObjetoJSON<T>(HttpResponseMessage httpResponseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await httpResponseMessage.Content.ReadAsStringAsync(), options);
        }
    }
}
