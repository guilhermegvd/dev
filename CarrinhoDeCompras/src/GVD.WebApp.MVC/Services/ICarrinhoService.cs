using GVD.WebApp.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace GVD.WebApp.MVC.Services
{
    public interface ICarrinhoService
    {
        Task<CarrinhoViewModel> ObterCarrinho();
        Task<IActionResult> AdicionarItem(CarrinhoItemViewModel item);
        Task<IActionResult> RemoverItem(Guid idItem);
        Task<IActionResult> AtualizarQuantidade(CarrinhoItemViewModel item);
        Task<IActionResult> AplicarVoucher(string voucherCode);
        Task<IActionResult> RetirarVoucher();
    }
}
