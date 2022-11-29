using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GVD.ShoppingCart.API.Models
{
    public class Carrinho
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid Id { get; set; }

        public List<CarrinhoItem> Itens { get; set; } = new List<CarrinhoItem>();

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal ValorTotal { get; set; }

        public string Voucher { get; set; }

        public bool PossuiVoucher { get; set; }

        public decimal ValorDesconto { get; set; }

        public int TotalItens { get; set; }

        internal int ContarItens()
        {
            return Itens.Sum(i => i.Quantidade);
        }
    }
}