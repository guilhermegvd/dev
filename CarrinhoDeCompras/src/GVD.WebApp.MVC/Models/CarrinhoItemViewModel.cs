using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GVD.WebApp.MVC.Models
{
    public class CarrinhoItemViewModel
    {
        [HiddenInput]
        public Guid IdItem { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(150, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Imagem { get; set; }

        [DisplayName("Valor unitário")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public decimal ValorUnitario { get; set; }

        [DisplayName("Qtd.")]
        public int Quantidade { get; set; }
    }
}
