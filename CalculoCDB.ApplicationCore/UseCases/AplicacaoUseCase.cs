using CalculoCDB.ApplicationCore.Domains.Entities;
using CalculoCDB.ApplicationCore.UseCases.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoCDB.ApplicationCore.UseCases
{
    public class AplicacaoUseCase: IAplicacaoUseCase
    {
        private readonly ILogger<AplicacaoUseCase> _logger;
        private readonly IConfiguration _configuration;
        public AplicacaoUseCase(ILogger<AplicacaoUseCase> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

        }

        public Resgate Calcular(Aplicacao aplicacao)
        {
            var percentualImposto = Convert.ToDecimal(PercentualImposto(aplicacao.Meses));
            var TB = (Convert.ToDecimal(_configuration["TB"]) / 100);
            var CDI = (Convert.ToDecimal(_configuration["CDI"]) / 100);
            Resgate result = new Resgate()
            {
                VF = CalcularValorFinal(aplicacao.ValorInicial, CDI, TB, aplicacao.Meses).ToString("F2"),
                Imposto = string.Concat(percentualImposto.ToString(), "%"),
                CDI = string.Concat((CDI * 100).ToString("F1"), "%"),
                VI = string.Concat("R$", (aplicacao.ValorInicial).ToString("F2")),
                TB = string.Concat((TB * 100).ToString(), "%")
            };
            var valorImposto = (Convert.ToDecimal(result.VF) - aplicacao.ValorInicial) * (percentualImposto / 100);
            result.TotalImposto = string.Concat("R$", valorImposto.ToString("F2"));
            result.TotalLiquido = string.Concat("R$", (Convert.ToDecimal(result.VF) - valorImposto).ToString("F2"));
            result.VF = string.Concat("R$", result.VF);
            return result;
        }
        private decimal CalcularValorFinal(decimal valorInicial, decimal cdi, decimal tb, int meses)
        {
            decimal valorFinal = valorInicial;

            for (int i = 0; i < meses; i++)
            {
                valorFinal = valorFinal * (1 + (cdi * tb));
            }

            return Math.Round(valorFinal, 2);
        }
        private decimal PercentualImposto(int meses)
        {
            
            decimal result = meses switch
            {
                <= 6 => Convert.ToDecimal(_configuration["Imposto:Ate06Meses"]) ,
                <= 12 => Convert.ToDecimal(_configuration["Imposto:Ate12Meses"]),
                <= 24 => Convert.ToDecimal(_configuration["Imposto:Ate24Meses"]),
                _ => Convert.ToDecimal(_configuration["Imposto:Acima24Meses"]),
            };
            return result;
        }
    }
}
