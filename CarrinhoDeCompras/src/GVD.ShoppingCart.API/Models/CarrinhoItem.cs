using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace GVD.ShoppingCart.API.Models
{
    public class CarrinhoItem
    {
        public Guid Id { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public Guid IdItem { get; set; }


        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(150, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Imagem { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal ValorUnitario { get; set; }

        public int Quantidade { get; set; }

        internal decimal CalcularValor()
        {
            return Quantidade * ValorUnitario;
        }
        internal void AdicionarUnidades(int qtd)
        {
            Quantidade += qtd;
        }
        internal void AtualizarQuantidade(int qtd)
        {
            Quantidade = qtd;
        }
    }
}
