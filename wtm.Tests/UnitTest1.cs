using AppMVCBasica.Faker;
using System;
using System.Text;
using Xunit;

namespace wtm.Tests
{
    public class UnitTest
    {
        void Main()
        {

            ProdutoFaker produto = new ProdutoFaker();
            Console.Write(gerarDadosFake(produto.dataFake()));
        }

        public String gerarDadosFake(object objeto)
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
