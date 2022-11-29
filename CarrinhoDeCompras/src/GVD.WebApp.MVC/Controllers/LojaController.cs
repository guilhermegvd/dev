using GVD.WebApp.MVC.Models;
using GVD.WebApp.MVC.Services;
using GVD.WebApp.MVC.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace GVD.WebApp.MVC.Controllers
{
    public class LojaController : Controller
    {
        private readonly ILogger<LojaController> _logger;
        private readonly ICarrinhoService _carrinhoService;

        public LojaController(ILogger<LojaController> logger, ICarrinhoService carrinhoService)
        {
            _logger = logger;
            _carrinhoService = carrinhoService;
        }

        public async Task<IActionResult> Index()
        {
            var dados = await _carrinhoService.ObterCarrinho();

            return View(dados);
        }

        [HttpGet]
        public async Task<IActionResult> Carrinho()
        {
            var dados = await _carrinhoService.ObterCarrinho();

            return View(dados);
        }

        [HttpPost]
        [Route("Loja/AdicionarItem")]
        public async Task<IActionResult> AdicionarItem(Guid itemId)
        {
            Itens.structItem item = Itens.tabelaItens.FirstOrDefault(i => i.id == itemId);

            await _carrinhoService.AdicionarItem(new CarrinhoItemViewModel
            {
                IdItem = item.id,
                Nome = item.nome,
                Imagem = item.imagem,
                ValorUnitario = item.valor,
                Quantidade = 1
            });

            var dados = await _carrinhoService.ObterCarrinho();

            return View("Carrinho", dados);
        }

        [HttpPost]
        [Route("Loja/RemoverItem")]
        public async Task<IActionResult> RemoverItem(Guid idItem)
        {
            await _carrinhoService.RemoverItem(idItem);

            var dados = await _carrinhoService.ObterCarrinho();

            return View("Carrinho", dados );
        }

        [HttpPost]
        [Route("Loja/AtualizarQuantidade")]
        public async Task<IActionResult> AtualizarQuantidade(CarrinhoItemViewModel item)
            {
            await _carrinhoService.AtualizarQuantidade(item);

            var dados = await _carrinhoService.ObterCarrinho();

            return View("Carrinho", dados);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        [HttpPost]
        [Route("Loja/AplicarVoucher")]
        public async Task<IActionResult> AplicarVoucher([FromForm] string? voucher)
        {
            if (voucher != null)
            {
                await _carrinhoService.AplicarVoucher(voucher);
            }

            var dados = await _carrinhoService.ObterCarrinho();

            return View("Carrinho", dados);
        }

        [HttpPost]
        [Route("Loja/RetirarVoucher")]
        public async Task<IActionResult> RetirarVoucher()
        {
            await _carrinhoService.RetirarVoucher();

            var dados = await _carrinhoService.ObterCarrinho();

            return View("Carrinho", dados);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}