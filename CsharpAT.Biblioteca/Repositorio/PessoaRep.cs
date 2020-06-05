using CsharpAT.Biblioteca.Modelo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CsharpAT.Biblioteca.Repositorio
{
    public class PessoaRep
    {
        private static string diretorio = "dir";
        private static string nomeArquivo = "aniversariantes.csv";
        private static string arquivo = Path.Combine(diretorio, nomeArquivo);
        private static StringBuilder csv = new StringBuilder();

        private static List<Pessoa> Pessoas = new List<Pessoa>()
        {
            new Pessoa(1, "Thyago", "Teles", DateTime.Parse("11/05/1989")),
            new Pessoa(2, "Candida", "Teles", DateTime.Parse("24/05/1962")),
            new Pessoa(3, "Gabrielle", "Teles", DateTime.Parse("29/12/1996")),
            new Pessoa(4, "Alexandre", "Ferreira", DateTime.Parse("21/07/1989")),
            new Pessoa(5, "Fulano", "Fulano", DateTime.Now.AddDays(-1)),
            new Pessoa(6, "Nome", "Sobrenome", DateTime.Now)
        };

        public static void CriarArquivo()
        {
            if (!File.Exists(arquivo))
            {
                Directory.CreateDirectory(diretorio);

                foreach (var item in Pessoas)
                {
                    csv.Append($"{item.Id};{item.Nome};{item.Sobrenome};{item.DataNascimento:dd/MM/yyyy}");
                    csv.AppendLine();
                }
                File.WriteAllText(arquivo, csv.ToString());
                Console.WriteLine("O arquivo foi criado.\r\n");
            }
        }

        private static List<Pessoa> CriarLista()
        {
            List<Pessoa> arquivoEmLista = new List<Pessoa>();

            var linhas = File.ReadAllLines(arquivo);

            foreach (var linha in linhas)
            {
                char[] separadores = new char[] { ';', ',', '\n' };
                string[] dadosPessoa = linha.Split(separadores);

                Pessoa p = new Pessoa(int.Parse(dadosPessoa[0]), dadosPessoa[1], dadosPessoa[2], DateTime.Parse(dadosPessoa[3]));

                arquivoEmLista.Add(p);
            }

            return arquivoEmLista;
        }

        public static void MostraPessoas()
        {
            List<Pessoa> arquivoEmLista = CriarLista();

            List<Pessoa> linqNiver = new List<Pessoa>();

            IEnumerable<Pessoa> consultaNiver = arquivoEmLista.Where(p => p.DataNascimento.Month == DateTime.Now.Month & p.DataNascimento.Day == DateTime.Now.Day);

            foreach (var resultadoNiver in consultaNiver)
            {
                linqNiver.Add(resultadoNiver);
            }

            if (linqNiver.Count >= 1)
            {
                Console.WriteLine("Pessoas que fazem aniversário hoje:\r\n");

                foreach (var aniversariantes in linqNiver)
                {
                    Console.WriteLine($"{aniversariantes.Nome} {aniversariantes.Sobrenome}");
                }
            }
            else
            {
                Console.WriteLine("\r\nNão há aniversariantes hoje.\r\n");
            }
        }

        public static void PesquisarPessoa()
        {
            List<Pessoa> arquivoEmLista = CriarLista();

            List<Pessoa> Linq = new List<Pessoa>();

            if (arquivoEmLista.Count >= 1)
            {
                Console.WriteLine("\r\nPessoas cadastradas:\r\n");
                foreach (var pessoa in arquivoEmLista)
                {
                    Console.WriteLine($"{pessoa.Nome} {pessoa.Sobrenome}");
                }

                Console.Write("\r\nDigite o nome ou parte do nome de quem você busca: ");
                var busca = Console.ReadLine().ToLower();
                IEnumerable<Pessoa> consulta = arquivoEmLista.Where(p => p.Nome.ToLower().Contains(busca) || p.Sobrenome.ToLower().Contains(busca));

                Console.WriteLine();

                int i = 1;

                foreach (var resultado in consulta)
                {
                    Linq.Add(resultado);
                    Console.WriteLine($"{i} - {resultado.Nome} {resultado.Sobrenome}");
                    i++;
                }

                if (Linq.Count >= 1)
                {
                    Console.Write("\r\nSelecione a opção desejada para visualizar os dados da pessoa escolhida: ");

                    if (int.TryParse(Console.ReadLine(), out int escolha) && escolha > 0 && escolha <= Linq.Count)
                    {
                        Console.WriteLine($"\r\nDados da pessoa escolhida:\r\n\r\nNome completo: {Linq[escolha - 1].Nome} {Linq[escolha - 1].Sobrenome}\r\nData de nascimento: {Linq[escolha - 1].DataNascimento:dd/MM/yyyy}\r\n{Linq[escolha - 1].CalcularAniversario()}\r\n");
                    }
                    else
                    {
                        Console.WriteLine("\r\nOpção Inválida!!\r\n");
                    }
                }
                else
                {
                    Console.WriteLine("\r\nA busca não encontrou resultados\r\n");
                }
            }
            else
            {
                Console.WriteLine("\r\nNão há pessoas na lista.\r\n");
            }

        }

        public static void AdicionarPessoa()
        {
            List<Pessoa> arquivoEmLista = CriarLista();

            Console.WriteLine("\r\nVocê escolheu **ADICIONAR NOVA PESSOA*\r\n");

            Console.Write("Nome: ");
            var nome = Console.ReadLine();
            Console.Write("Sobrenome: ");
            var sobrenome = Console.ReadLine();
            Console.Write("Data de Nascimento (no formato dd/MM/yyyy): ");
            var dataNascimento = Console.ReadLine();

            if (DateTime.TryParse(dataNascimento, out DateTime parsedData))
            {
                Console.WriteLine("\r\nDeseja adicionar essa pessoa?\r\n");
                Console.WriteLine($"Nome Completo: {nome} {sobrenome}");
                Console.WriteLine($"Data do Nascimento: {parsedData:dd/MM/yyyy} \r\n");

                Console.Write("1 - Sim\r\n" +
                        "0 - Não (Voltar ao menu principal)\r\n" +
                        "\r\nEscolha uma opção: ");

                if (int.TryParse(Console.ReadLine(), out int opcao))
                {
                    switch (opcao)
                    {
                        case 1:
                            if (arquivoEmLista.Count >= 1)
                            {
                                csv.Append($"{arquivoEmLista.Last().Id + 1};{nome};{sobrenome};{dataNascimento}\r\n");
                            }
                            else
                            {
                                csv.Append($"1;{nome};{sobrenome};{dataNascimento}\r\n");
                            }
                            File.WriteAllText(arquivo, csv.ToString());
                            Console.WriteLine("\r\nDados adicionados com sucesso!\r\n");
                            break;
                        case 0:
                            break;
                        default:
                            Console.WriteLine("\r\nOpção Inválida!!\r\n");
                            break;
                    }
                }
            }
            else
            {
                Console.WriteLine("\r\nFormato incorreto de data! Tente novamente.\r\n");
            }
        }

        public static void EditarPessoa()
        {
            List<Pessoa> arquivoEmLista = CriarLista();

        }

        public static void DeletarPessoa()
        {
            List<Pessoa> arquivoEmLista = CriarLista();

            if (arquivoEmLista.Count() >= 1)
            {
                foreach (var pessoa in arquivoEmLista)
                {
                    Console.WriteLine($"{pessoa.Id} - {pessoa.Nome} {pessoa.Sobrenome}");
                }

                Console.Write("\r\nQual pessoa você deseja deletar? Digite o número correspondente: ");

                if (int.TryParse(Console.ReadLine(), out int escolha))
                {
                    IEnumerable<Pessoa> listaEscolha = arquivoEmLista.Where(p => p.Id == escolha);
                    Console.WriteLine($"\r\nVocê realmente deseja deletar essa pessoa da lista?\r\n\r\nNome Completo: {listaEscolha.ToArray()[0].Nome} {listaEscolha.ToArray()[0].Sobrenome}\r\nData de nascimento: {listaEscolha.ToArray()[0].DataNascimento:dd/MM/yyyy}\r\n");

                    Console.Write("1 - Sim\r\n" +
                       "0 - Não (Voltar ao menu principal)\r\n" +
                       "\r\nEscolha uma opção: ");

                    if (int.TryParse(Console.ReadLine(), out int opcao))
                    {
                        switch (opcao)
                        {
                            case 1:
                                IEnumerable<Pessoa> listaDelete = arquivoEmLista.Where(p => p.Id != escolha);
                                csv.Clear();
                                foreach (var pessoa in listaDelete)
                                {
                                    csv.Append($"{pessoa.Id};{pessoa.Nome};{pessoa.Sobrenome};{pessoa.DataNascimento}\r\n");
                                }
                                File.WriteAllText(arquivo, csv.ToString());
                                Console.WriteLine("\r\nEntrada removida com sucesso!\r\n");
                                break;
                            case 0:
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
                else
                {
                    Console.WriteLine("\r\nOpção Inválida!!\r\n");
                }
            }
            else
            {
                Console.WriteLine("\r\nNão há pessoas na lista.\r\n");
            }
        }
    }
}
