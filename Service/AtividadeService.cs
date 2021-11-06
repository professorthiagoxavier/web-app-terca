using Domain;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    public class AtividadeService : IAtividadeService
    {
        protected static readonly MediaTypeHeaderValue CONTENT_TYPE = new MediaTypeHeaderValue("application/json");
        private readonly IConfiguration configuration;
        private HttpClient httpClient;

        public AtividadeService(IConfiguration configuration)
        {
            this.configuration = configuration;
            httpClient = GetHttp();
        }

        private HttpClient GetHttp()
        {
            HttpClient httpCliente = new HttpClient();
            httpCliente.BaseAddress = new Uri(configuration["ClienteService:BaseUrl"]);
            return httpCliente;
        }

        public async Task<List<AtividadeModel>> BuscarAtividadesAsync()
        {
            var response = await httpClient.GetAsync("atividade");

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<List<AtividadeModel>>(content);

            return result;
        }

        public async Task<AtividadeModel> SalvarAtividade(AtividadeModel model)
        {
            using var content = new ByteArrayContent(GetByteData(model));
            content.Headers.ContentType = CONTENT_TYPE;
            var response = await httpClient.PostAsync("atividade", content);

            if (response.StatusCode != System.Net.HttpStatusCode.Created)
                return null;

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<AtividadeModel>(responseContent);
            return result;

        }

        protected virtual byte[] GetByteData<TRequest>(TRequest requestBody)
        {
            var settings = new JsonSerializerOptions { IgnoreNullValues = true, PropertyNameCaseInsensitive = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping };
            var body = JsonSerializer.Serialize(requestBody, settings);
            return Encoding.UTF8.GetBytes(body);
        }

    }
}
