using Microsoft.AspNetCore.Authorization;
using System.ComponentModel;

namespace AppMVCBasica.Models
{
    public class Venda : Entity
    {
        [DisplayName("Fornecedor")]
        public Guid FornecedorId { get; set; }
       
        [DisplayName("Produto")]
        public Guid ProdutoId { get; set; }

        public int Quantidade { get; set; }
       
        [DisplayName("Data da Venda")]
        public DateTime DataVenda { get; set; }
        
        public Fornecedor Fornecedor { get; set; }
        public Produto Produto { get; set; }



    }
}
