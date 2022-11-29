using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;

namespace GVD.WebApp.MVC.Models
{
    public class CarrinhoViewModel
    {
        public List<CarrinhoItemViewModel> Itens { get; set; } = new List<CarrinhoItemViewModel>();
        
        [DisplayName("Valor a pagar")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal ValorTotal { get; set; }

        [StringLength(20, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} dígitos", MinimumLength = 1)]
        [Required(ErrorMessage = "O voucher informado é inválido!")]
        public string Voucher { get; set; }

        [HiddenInput]
        public bool PossuiVoucher { get; set; }

        [DisplayName("Desconto")]
        public decimal ValorDesconto { get; set; }

        public int TotalItens { get; set; }
    }
}
