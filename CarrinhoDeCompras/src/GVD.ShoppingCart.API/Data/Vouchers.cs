namespace GVD.ShoppingCart.API.Data
{
    public static class Vouchers
    {
        public enum tipoDesconto : int
        {
            Percentual = 1,
            Valor_Absoluto = 2
        }

        public struct structVoucher
        {
            public string codigo;
            public tipoDesconto tipoDesconto;
            public decimal valor;
        }

        public static List<structVoucher> tabelaVouchers;

        static Vouchers()
        {
            tabelaVouchers = new List<structVoucher>();
            CriarVoucher("PROMOCAO10", tipoDesconto.Percentual, 10.0M);
            CriarVoucher("DESCONTO50", tipoDesconto.Valor_Absoluto, 50.0M);
        }

        private static void CriarVoucher(string codigo, tipoDesconto tipoDesconto, decimal valor)
        {
            structVoucher voucher = new structVoucher();
            voucher.codigo = codigo;
            voucher.tipoDesconto = tipoDesconto;
            voucher.valor = valor;

            tabelaVouchers.Add(voucher);
        }

       
    }

}
