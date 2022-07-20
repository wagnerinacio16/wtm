using AppMVCBasica.Models;
using Bogus;

namespace AppMVCBasica.Faker
{
    public class ProdutoFaker: Faker<Produto>
    {

        public ProdutoFaker()
        {
            RuleFor(produto => produto.Nome, data => data.Commerce.ProductName());
            RuleFor(produto => produto.Descricao, data => data.Commerce.ProductAdjective());
            RuleFor(produto => produto.Imagem, data => data.Commerce.ProductName());
            RuleFor(produto => produto.Valor, data => data.Random.Decimal());
            RuleFor(produto => produto.DataCadastro, data => data.Date.Past());
            RuleFor(produto => produto.Ativo, data => data.Random.Bool());
            //RuleFor(produto => produto.Fornecedor, data => FornecedorFaker());
            
        }
    }
}
