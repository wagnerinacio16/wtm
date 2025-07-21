using AppMVCBasica.Data;
using AppMVCBasica.Models;
using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace AppMVCBasica.Faker
{
    [Authorize]
    public class VendaFaker : Faker<Venda>
    {
        List<Fornecedor> fornecedores = new List<Fornecedor>();

        public Venda dataFake()
        {

            var vendaFaker = new Faker<Venda>("pt_BR")

                .RuleFor(venda => venda.Quantidade, venda => venda.Random.Int(1,1000))
                .RuleFor(venda => venda.DataVenda, venda => venda.Date.Past(1));

            return vendaFaker;
        }

        //public string[] UpdateVenda()
        //{
        //    Venda vendaFake = new FakerVenda("pt_BR")
        //        .RuleFor(Venda => Venda.)
        //}
    }
}

