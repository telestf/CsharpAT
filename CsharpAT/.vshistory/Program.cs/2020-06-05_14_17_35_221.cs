using CsharpAT.Biblioteca.Modelo;
using CsharpAT.Biblioteca.Repositorio;
using System;

namespace CsharpAT
{
    class Program
    {
        static void Main(string[] args)
        {
            PessoaRep.CriarArquivo();          
            PessoaRep.MostraPessoas();
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
                        "3 - Editar uma Pessoa\r\n" +
                        "4 - Deletar uma Pessoa\r\n" +
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
                        case 3:
                            PessoaRep.EditarPessoa();
                            break;
                        case 4:
                            PessoaRep.DeletarPessoa();
                            break;
                        case 0:
                            Console.WriteLine("\r\nVocê encerrou a sessão.");
                            break;
                        default:
                            Console.WriteLine("\r\nOpção Inválida!!\r\n");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\r\nOpção Inválida!!\r\n");
                }
            }
            while (opcao != 0);
        }
    }
}