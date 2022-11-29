namespace GVD.WebApp.MVC.Data
{
    public class Itens
    {        
        public struct structItem
        {
            public Guid id;
            public string nome;
            public string imagem;
            public decimal valor;
        }

        public static List<structItem> tabelaItens;

        static Itens()
        {
            tabelaItens = new List<structItem>();
            CriarItem("jQuery Pocket Reference", "1dbbd291-279f-4743-a781-0c92a482bc2b_JQuery.jpg", 60.0M);
            CriarItem("CSS and Documents", "1e723e03-a406-442b-b2c8-f4c561681379_CSS.jpg", 55.0M);
            CriarItem("Python Pocket Reference", "6ba49f3f-f55a-4505-86c2-f4e58ad86c62_Python.jpg", 72.0M);
            CriarItem("ASP.NET MVC 5", "9e2b29e2-f7e6-4fbf-8696-761ebd462f47_MVC5.jpg", 110.0M);
            CriarItem("HTML 5 Pocket Reference", "734ed7ec-23c7-4b6a-80ea-9cbf445a6729_HTML.jpg", 60.0M);
            CriarItem("Java Examples in a Nutshell", "2994c744-02d5-4b4a-98d1-e04ae8709e5c_Java.jpg", 52.0M);
            CriarItem("Regular Expression", "4677e4a2-c57e-42f9-8cef-7ea55aa92c33_Regex.jpg", 45.0M);
            CriarItem("Programming Razor", "cc0a711a-a172-48a1-ab69-b074b572321b_Razor.jpg", 110.0M);
        }

        private static void CriarItem(string nome, string imagem, decimal valor)
        {
            structItem item = new structItem();
            item.id = Guid.NewGuid();
            item.nome = nome;
            item.imagem = imagem;
            item.valor = valor;

            tabelaItens.Add(item);
        }

    }
}
