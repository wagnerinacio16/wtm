using AppMVCBasica.Models;
using Bogus;
using System.Globalization;

namespace AppMVCBasica.Faker
{
    public class ProdutoFaker : Faker<Produto>
    {
        public Produto dataFake()
        {
            var produtoFaker = new Faker<Produto>("pt_BR")

                .RuleFor(produto => produto.Nome, data => data.Commerce.Product())
                .RuleFor(produto => produto.Descricao, data => data.Commerce.ProductDescription())
                //.RuleFor(produto => produto.Imagem, data => data.Commerce.ProductName())
                .RuleFor(produto => produto.Valor, data => data.Random.Decimal(1,100000))
                .RuleFor(produto => produto.DataCadastro, data => data.Date.Past())
                .RuleFor(produto => produto.Ativo, data => data.Random.Bool());
           
            return produtoFaker;
        }
    }
}

