using CsharpAT.Biblioteca.Repositorio;
using System;

namespace CsharpAT
{
    class Program
    {
        static void Main(string[] args)
        {
            GerenciarAniversarios();
        }

        private static void GerenciarAniversarios()
        {
            int opcao;

            do
            {
                Console.Write("Menu Principal\r\n" +
                    "\r\n1 - Pesquisar Pessoas\r\n" +
                        "2 - Adicionar Nova Pessoa\r\n" +
                        "0 - Sair\r\n" +
                        "\r\nEscolha uma opção: ");

                if (int.TryParse(Console.ReadLine(), out opcao))
                {

                    switch (opcao)
                    {
                        case 1:
                            PessoaRep.PesquisarPessoa();
                            break;
                        case 2:
                            PessoaRep.AdicionarPessoa();
                            break;
                        case 0:
                            Console.WriteLine("Você encerrou a sessão.");
                            break;
                        default:
                            Console.WriteLine("Opção Inválida!!\r\n");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Opção Inválida!!\r\n");
                }
            }
            while (opcao != 0);
        }
    }
}