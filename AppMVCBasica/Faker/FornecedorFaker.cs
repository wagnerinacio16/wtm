using AppMVCBasica.Models;
using Bogus.Premium;
using Bogus;
using Bogus.Extensions.Brazil;

namespace AppMVCBasica.Faker
{
    public class FornecedorFaker : Faker<Fornecedor>
    {

        public Fornecedor fake()
        {
            var fornecedorFaker = new Faker<Fornecedor>("pt_BR")

                .RuleFor(fornecedor => fornecedor.Nome, data => data.Name.FullName())
                .RuleFor(fornecedor => fornecedor.Ativo, data => data.Random.Bool())
                .RuleFor(fornecedor => fornecedor.TipoFornecedor, data => data.Random.Enum<TipoFornecedor>());

            return fornecedorFaker;
        }

        public Fornecedor dataFake()
        {
            Fornecedor objeto = this.fake();
            EnderecoFaker enderecoFaker = new EnderecoFaker();
            objeto.Endereco = new EnderecoFaker().dataFake();

            if (objeto.TipoFornecedor == TipoFornecedor.PessoaFisica)
            {
                objeto.Nome = PessoaFisica().First();
                objeto.Documento = PessoaFisica().Last();
            }
            else
            {

                objeto.Nome = PessoaJuridica().First();
                objeto.Documento = PessoaJuridica().Last();
            }
            return objeto;
        }

        public Fornecedor UpdateFornecedor(Fornecedor fornecedor)
        {

            if (fornecedor.TipoFornecedor == TipoFornecedor.PessoaFisica)
            {
                fornecedor.Nome = PessoaFisica().First();
                fornecedor.Documento = PessoaFisica().Last();
            }
            else
            {

                fornecedor.Nome = PessoaJuridica().First();
                fornecedor.Documento = PessoaJuridica().Last();
            }


            return fornecedor;
        }


        public string[] PessoaFisica()
        {
            Fornecedor fornecedorPF = new Faker<Fornecedor>("pt_BR")
                .RuleFor(fornecedor => fornecedor.Nome, data => data.Name.FullName())
                .RuleFor(fornecedor => fornecedor.Documento, data => data.Person.Cpf());
            string[] pf = new string[2] { fornecedorPF.Nome, fornecedorPF.Documento };

            return pf;
        }

        public string[] PessoaJuridica()
        {
            Fornecedor fornecedorPJ = new Faker<Fornecedor>("pt_BR")
                .RuleFor(fornecedor => fornecedor.Nome, data => data.Company.CompanyName())
                .RuleFor(fornecedor => fornecedor.Documento, data => data.Company.Cnpj());
            string[] pj = new string[2] { fornecedorPJ.Nome, fornecedorPJ.Documento };

            return pj;
        }
    }
}