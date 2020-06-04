using System;

namespace CsharpAT.Biblioteca.Modelo
{
    public class Pessoa
    {
        public string Nome;
        public string Sobrenome;
        public DateTime DataNascimento;

        public Pessoa(string nome, string sobrenome, DateTime dataNascimento)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            DataNascimento = dataNascimento;
        }

        public string CalcularAniversario()
        {
            DateTime comemoracaoAniversario = new DateTime(DateTime.Now.Year, DataNascimento.Month, DataNascimento.Day);
            TimeSpan contagemDias = comemoracaoAniversario - DateTime.Now;

            if (contagemDias.Days > 0)
            {
                return $"Falta(m) {contagemDias.Days} dia(s) para o aniversário deste ano.";
            }
            else if (contagemDias.Days == 0)
            {
                return "Hoje é o dia do aniversário.";
            }
            else
            {
                return $"O aniversário deste ano foi há {Math.Abs(contagemDias.Days)} dia(s).";
            }
        }
    }
}
