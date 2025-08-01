﻿using AppMVCBasica.Controllers;
using AppMVCBasica.Data;
using AppMVCBasica.Faker;
using System.Text;
namespace ConsoleAppTests
{
    public class Program
    {
        static void Main(string[] args)
        {


            ProdutoFaker produto = new ProdutoFaker();
            produto.ImprimirLista();
            //EnderecoFaker endereco = new EnderecoFaker();
            //FornecedorFaker fornecedor = new FornecedorFaker();
            //for (int i = 1; i <= 10; i++)
            //{
            //    Console.WriteLine(gerarDadosFake(fornecedor.dataFake()));

            //}
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