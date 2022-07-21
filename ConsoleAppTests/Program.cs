using AppMVCBasica.Faker;
using System.Text;
namespace ConsoleAppTests
{
    public class Program
    {
        static void Main(string[] args)
        {

            ProdutoFaker produto = new ProdutoFaker();
            EnderecoFaker endereco = new EnderecoFaker();
            FornecedorFaker fornecedor = new FornecedorFaker();
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine(gerarDadosFake(produto.dataFake()));

            }
        }

        static string gerarDadosFake(object objeto)
        {
            var tipo = objeto.GetType();

            var sb = new StringBuilder();

            foreach (var p in tipo.GetProperties())
            {
                sb.AppendLine(p.Name + ": " + p.GetValue(objeto));
            }
            return sb.ToString();
        }

    }
}