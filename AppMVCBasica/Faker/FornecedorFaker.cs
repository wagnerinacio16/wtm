using AppMVCBasica.Models;
using Bogus;
using Bogus.Extensions.Brazil;

namespace AppMVCBasica.Faker
{
    public class FornecedorFaker: Faker<Fornecedor>
    {
        public FornecedorFaker()
        {
            RuleFor(fornecedor => fornecedor.Nome, Data => Data.Name.FullName());
            RuleFor(fornecedor => fornecedor.Documento, Data => Data.Person.Cpf());
        }
    }
}
