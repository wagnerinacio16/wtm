using AppMVCBasica.Data;
using AppMVCBasica.Models;
using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace AppMVCBasica.Faker
{
    [Authorize]
    public class ProdutoFaker : Faker<Produto>
    {
        private readonly ApplicationDbContext _context;
        List<Fornecedor> fornecedores = new List<Fornecedor>();


        public ProdutoFaker(ApplicationDbContext context)
        {
            _context = context;
        }
        public ProdutoFaker()
        {
        }
        public Produto dataFake()
        {

            var produtoFaker = new Faker<Produto>("pt_BR")

                .RuleFor(produto => produto.Nome, data => data.Commerce.Product())
                .RuleFor(produto => produto.Descricao, data => data.Commerce.ProductDescription())
                .RuleFor(produto => produto.Valor, data => data.Random.Decimal(1, 100000))
                .RuleFor(produto => produto.DataCadastro, data => data.Date.Past())
                .RuleFor(produto => produto.Ativo, data => data.Random.Bool());

            return produtoFaker;
        }
        public string[] UpdateProduto()
        {
            Produto produtoFaker = new Faker<Produto>("pt_BR")
                .RuleFor(produto => produto.Nome, data => data.Commerce.ProductMaterial())
                .RuleFor(produto => produto.Descricao, data => data.Commerce.ProductDescription());

            string[] produto = new string[2] { produtoFaker.Nome, produtoFaker.Descricao };

            return produto;
        }
        public void ImprimirLista()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var result = context.Fornecedores.ToList().FirstOrDefault();
            Console.WriteLine(result);
        }
    }
}

