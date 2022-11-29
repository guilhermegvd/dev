using GVD.ShoppingCart.API.Models;
using GVD.ShoppingCart.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using static System.Net.Mime.MediaTypeNames;

namespace GVD.ShoppingCart.API.Controllers
{
    [ApiController]
    [Route("api")]
    public class CarrinhoController : ControllerBase
    {        
        private readonly ILogger<CarrinhoController> _logger;
        private readonly ICarrinhoRepository _carrinhoRepository;
        private Carrinho _carrinho;

        public CarrinhoController(ILogger<CarrinhoController> logger, ICarrinhoRepository carrinhoRepository)
        {
            _logger = logger;
            _carrinhoRepository = carrinhoRepository;
            _carrinho = _carrinhoRepository.ObterCarrinho();
        }

        [HttpGet("carrinho")]
        public Carrinho ObterCarrinho()
        {
            return _carrinho;
        }

        [HttpPost("carrinho/item")]
        public IActionResult AdicionarItem(CarrinhoItem item)
        {
            if (item.Quantidade != 1) return ErroBadRequest("A quantidade do item deve ser 1!");

            if (!ModelState.IsValid) return ErroBadRequest("Dados não preenchidos corretamente.");

            // Verifica se item já existe
            if (_carrinho.Itens.Any(i => i.IdItem == item.IdItem))
            {
                // Incrementa quantidade
                _carrinho.Itens.FirstOrDefault(i => i.IdItem == item.IdItem).AdicionarUnidades(1);
            }
            else
            {
                // Adiciona item
                item.Id = Guid.NewGuid();
                _carrinho.Itens.Add(item);
            }

            CalcularTotal();

            return Ok();
        }

        [HttpPost("carrinho/item/remover")]
        public IActionResult RemoverItem([FromForm] Guid idItem)
        {
            // Verifica se item existe de fato
            if (_carrinho.Itens.Any(i => i.IdItem == idItem))
            {
                // Remove o item
                _carrinho.Itens.Remove(_carrinho.Itens.FirstOrDefault(i => i.IdItem == idItem));
            }
            else
            {
                // Erro: solicitado remover item que não está no carrinho
                return ErroBadRequest("Item solicitado não está no carrinho!");
            }

            CalcularTotal();

            return Ok();
        }

        [HttpPost("carrinho/item/atualizar")]
        public IActionResult AtualizarQuantidade(CarrinhoItem item)
        {
            if (!ModelState.IsValid) return ErroBadRequest("Dados não preenchidos corretamente.");

            // Verifica se item existe de fato
            if (_carrinho.Itens.Any(i => i.IdItem == item.IdItem))
            {

                if (item.Quantidade > 0) 
                {
                    // Define novo valor
                    _carrinho.Itens.FirstOrDefault(i => i.IdItem == item.IdItem).AtualizarQuantidade(item.Quantidade);

                    CalcularTotal();
                }
                else
                {
                    // Exclui o item
                    RemoverItem(item.IdItem);
                }
            }
            else
            {
                // Erro: solicitado atualizar item que não está no carrinho
                return ErroBadRequest("Item solicitado não está no carrinho!");
            }

            return Ok();
        }

        [HttpPost("carrinho/voucher")]
        public IActionResult AplicarVoucher([FromForm] string Voucher)
        {
            _carrinho.Voucher = Voucher;

            CalcularTotal();

            if (_carrinho.PossuiVoucher)
            {
                return Ok();

            } 
            else
            {
                return Ok("Voucher inválido!");
            }

        }

        [HttpPost("carrinho/voucher/remover")]
        public IActionResult RetirarVoucher()
        {
            _carrinho.Voucher = null;
            
            CalcularTotal();

            return Ok();
        }
        
        internal void CalcularTotal()
        {
            _carrinho.ValorTotal = _carrinho.Itens.Sum(i => i.CalcularValor());

            CalcularDesconto();

            // Verifica se desconto não irá negativar o valor total
            if (_carrinho.ValorTotal > _carrinho.ValorDesconto)
            {
                _carrinho.ValorTotal -= _carrinho.ValorDesconto;
            }
            else
            {
                _carrinho.ValorTotal = 0;
            }

            CalcularTotalItens();
        }

        internal void CalcularTotalItens()
        {
            _carrinho.TotalItens = _carrinho.ContarItens();
        }


        internal void CalcularDesconto()
        {
            // Verifica se voucher existe
            if (_carrinho.Voucher != null && Vouchers.tabelaVouchers.Any(v => v.codigo == _carrinho.Voucher))
            {
                // Obtém detalhes do voucher
                Vouchers.structVoucher voucherEncontrado = Vouchers.tabelaVouchers.FirstOrDefault(v => v.codigo == _carrinho.Voucher);

                _carrinho.Voucher = _carrinho.Voucher;

                _carrinho.PossuiVoucher = true;

                if (voucherEncontrado.tipoDesconto == Vouchers.tipoDesconto.Percentual)
                {
                    _carrinho.ValorDesconto = _carrinho.ValorTotal * (voucherEncontrado.valor / 100);
                }
                else
                {
                    _carrinho.ValorDesconto = voucherEncontrado.valor;
                }
            }
            else
            {
                _carrinho.Voucher = null;

                _carrinho.PossuiVoucher = false;

                _carrinho.ValorDesconto = 0.0M;
            }
        }

        internal IActionResult ErroBadRequest(string mensagem)
        {
            string[] msg = { mensagem };
            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "Erro", msg}
            }));
        }
    }
}