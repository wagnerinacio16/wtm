using AppMVCBasica.Models;
using Bogus.Premium;
using Bogus;
using Bogus.Extensions.Brazil;

namespace AppMVCBasica.Faker
{
    public class FornecedorFaker : Faker<Fornecedor>
    {
        public Guid Id { get; set; }
        Random rd = new Random();
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
            Fornecedor objeto = fake();
            EnderecoFaker enderecoFaker = new EnderecoFaker();
            objeto.Endereco = new EnderecoFaker().dataFake();
            Fornecedor fornecedorPF = new Faker<Fornecedor>("pt_BR")
               .RuleFor(fornecedor => fornecedor.Documento, data => data.Person.Cpf());

            Fornecedor fornecedorPJ = new Faker<Fornecedor>("pt_BR")
               .RuleFor(fornecedor => fornecedor.Nome, data => data.Company.CompanyName())
               .RuleFor(fornecedor => fornecedor.Documento, data => data.Company.Cnpj());

            if (objeto.TipoFornecedor == TipoFornecedor.PessoaFisica)
            {
                objeto.Documento = fornecedorPF.Documento;
            }
            else
            {
                objeto.Nome = fornecedorPJ.Nome;
                objeto.Documento = fornecedorPJ.Documento;
            }
            return objeto;
        }
    }
}