using System.Drawing;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TargetDesafio
{
    public class FaturamentoDiario
    {
        [JsonPropertyName("dia")]
        public int Dia { get; set; }
        
        [JsonPropertyName("valor")]
        public double Valor { get; set; }

        public override string ToString()
        {
            return $"Dia: {Dia}, Valor: {Valor:C2}";
        }
    }

    public class FaturamentoMensalEstado
    {
        public string Estado { get; set; }
        public double Valor { get; set; }

        public override string ToString()
        {
            return $"Dia: {Estado}, Valor: {Valor:C2}";
        }
    }

    public class EstadoPercentual 
    {
        public string Estado { get; set; }
        public double Percentual { get; set; }

        public override string ToString()
        {
            return $"Estado: {Estado}, Percentual do faturamento: {Percentual:F2}%";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {

            // Questao 1

            int indice = 13;
            int soma = 0;
            int k = 0;

            while (k < indice)
            {
                k++;
                soma = soma + k;
            }
            Console.WriteLine("Valor de K: " + soma);
            Console.WriteLine("----------------------------------------------");

            // Questao 2

            static bool isFibonnaci(int n)
            {
                if (n == 0 || n == 1)
                    return true;

                int a = 0;
                int b = 1;
                while (b < n)
                {
                    int temp = a;
                    a = b;
                    b = temp + b;
                }
                return (b == n || a == n);
            }

            int teste = 33;

            if (isFibonnaci(teste))
            {
                Console.WriteLine("O numero " + teste + " faz parte do fibonacci");
            }
            else
            {
                Console.WriteLine("O numero " + teste + " NAO faz parte do fibonacci");
            }

            Console.WriteLine("----------------------------------------------");

            // Questao 3

            string caminho = Path.Combine("data", "dados.json");
            string json = File.ReadAllText(caminho);

            List<FaturamentoDiario> faturamentoDiarioBruto = JsonSerializer.Deserialize<List<FaturamentoDiario>>(json);
            List<FaturamentoDiario> diasValidos = faturamentoDiarioBruto.Where(f => f.Valor > 0).ToList();

            double valorMaximo = diasValidos.Max(f => f.Valor);
            double valorMinimo = diasValidos.Min(f => f.Valor);
            double mediaMensal = (diasValidos.Sum(f => f.Valor) / diasValidos.Count);

            FaturamentoDiario maiorFaturamento = diasValidos.Where(f => f.Valor == valorMaximo).First();
            FaturamentoDiario menorFaturamento = diasValidos.Where(f => f.Valor == valorMinimo).First();
            List<FaturamentoDiario> faturamentoAcimaDaMedia = diasValidos.Where(f => f.Valor > mediaMensal).ToList();

            Console.WriteLine("Maior faturamento: " + maiorFaturamento.ToString());
            Console.WriteLine("Menor faturamento: " + menorFaturamento.ToString());
            Console.WriteLine($"Media mensal: {mediaMensal:C2}");
            Console.WriteLine("Dias acima da media mensal: ");
            Console.WriteLine(string.Join("\n", faturamentoAcimaDaMedia));
            Console.WriteLine("Numero de dias em que o faturamento foi acima da media mensal: " + faturamentoAcimaDaMedia.Count + " dias");

            Console.WriteLine("----------------------------------------------");

            // Questao 4

            List<FaturamentoMensalEstado> faturamentoEstados = new List<FaturamentoMensalEstado>
            {
                new FaturamentoMensalEstado { Estado = "SP", Valor = 67836.43 },
                new FaturamentoMensalEstado { Estado = "RJ", Valor = 36678.66 },
                new FaturamentoMensalEstado { Estado = "MG", Valor = 29229.88 },
                new FaturamentoMensalEstado { Estado = "ES", Valor = 27165.48 },
                new FaturamentoMensalEstado { Estado = "Outros", Valor = 19849.53 }
            };

            double valorTotal = faturamentoEstados.Sum(f => f.Valor);

            List<EstadoPercentual> percentualEstados = new List<EstadoPercentual>();

            foreach (var estado in faturamentoEstados)
            {
                double percentual = (estado.Valor / valorTotal) * 100;
                percentualEstados.Add(new EstadoPercentual
                {
                    Estado = estado.Estado,
                    Percentual = percentual
                });
            }
            
            Console.WriteLine($"Percentual dos estados sobre o faturamento total de: {valorTotal:C2}");
            Console.WriteLine(string.Join("\n", percentualEstados));

            Console.WriteLine("----------------------------------------------");

            // Questao 5

            string textoNormal = "Me inverta!";
            string textoInvertido = "";

            for (int i = textoNormal.Length; i > 0; i--)
            {
                textoInvertido += textoNormal[i-1];
            }
            Console.WriteLine("String normal: " + textoNormal);
            Console.WriteLine("String invertida: " + textoInvertido);

            Console.WriteLine("----------------------------------------------");

        }
    }
}
