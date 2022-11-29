using GVD.ShoppingCart.API.Models;

namespace GVD.ShoppingCart.API.Data
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private Carrinho _carrinho;

        public CarrinhoRepository()
        {
            _carrinho = new Carrinho()
            {
                Id = Guid.NewGuid(),
                Itens = {},
                ValorTotal = 0,
                Voucher = null,
                PossuiVoucher = false,
                ValorDesconto = 0,
                TotalItens = 0
            };
        }

        public Carrinho ObterCarrinho()
        {
            return _carrinho;
        }
    }

    public interface ICarrinhoRepository
    {
        Carrinho ObterCarrinho();
    }
}
