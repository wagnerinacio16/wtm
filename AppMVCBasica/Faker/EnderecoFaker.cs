using AppMVCBasica.Models;
using Bogus;

namespace AppMVCBasica.Faker
{
    public class EnderecoFaker: Faker<Endereco> 
    {
        public EnderecoFaker()
        {
            RuleFor(x => x.Logradouro, f => f.Address.StreetName());
            RuleFor(x => x.Numero, f => f.Address.BuildingNumber());
            RuleFor(x => x.Complemento, f => f.Address.SecondaryAddress());
            RuleFor(x => x.Cep, f => f.Address.ZipCode());
            RuleFor(x => x.Bairro, f => f.Address.Direction());
            RuleFor(x => x.Cidade, f => f.Address.City());
            RuleFor(x => x.Estado, f => f.Address.StateAbbr());
        }
    }
}
