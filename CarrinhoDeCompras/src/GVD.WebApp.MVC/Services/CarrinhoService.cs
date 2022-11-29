using GVD.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text.Json;

namespace GVD.WebApp.MVC.Services
{
    public class CarrinhoService : Service, ICarrinhoService
    {
        private readonly HttpClient _httpClient;
        
        public CarrinhoService(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri("https://localhost:7048");

            _httpClient = httpClient;
        }        
        public async Task<CarrinhoViewModel> ObterCarrinho()
        {
            var response = await _httpClient.GetAsync("/api/carrinho");

            return await DeserializarObjetoJSON<CarrinhoViewModel>(response);
        }
        
        public async Task<IActionResult> AdicionarItem(CarrinhoItemViewModel item)
        {
            var conteudo = CastToContent(item);
       
            var response = await _httpClient.PostAsync("/api/carrinho/item", conteudo);

            return null;
        }

        public async Task<IActionResult> AtualizarQuantidade(CarrinhoItemViewModel item)
        {

            var conteudo = CastToContent(item);

            var response = await _httpClient.PostAsync("/api/carrinho/item/atualizar", conteudo);

            HttpContent content = conteudo;
            string jsonContent = content.ReadAsStringAsync().Result;

            return null;
            
        }

        public async Task<IActionResult> RemoverItem(Guid idItem)
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var data = "idItem=" + idItem;
                var result = client.UploadString("https://localhost:7048/api/carrinho/item/remover", "POST", data);
            }
            return null;
        }

        public async Task<IActionResult> AplicarVoucher(string voucherCode)
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var data = "voucher=" + voucherCode;
                var result = client.UploadString("https://localhost:7048/api/carrinho/voucher", "POST", data);
            }
            return null;
        }
        public async Task<IActionResult> RetirarVoucher()
        {
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";
                var result = client.UploadString("https://localhost:7048/api/carrinho/voucher/remover", "POST", "");
            }
            return null;
        }

    }
}
