using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AppMVCBasica.Models
{
    public class Produto : Entity
    {
        [DisplayName("Fornecedor")]
        public Guid FornecedorId { get; set; }

        public string Nome { get; set; }

        [DisplayName("Descrição")]
        public string Descricao { get; set; }

        public decimal Valor { get; set; }
        
        [DisplayName("Data do Cadastro")]
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }

        /* EF Relation*/
        public Fornecedor Fornecedor { get; set; }

    }
}
