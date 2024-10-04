using CalculoCDB.ApplicationCore.Domains.Entities;

namespace CalculoCDB.API.Response
{
    public class AplicacaoResponse:Resgate
    {
        public static IEnumerable<AplicacaoResponse> Converter(Resgate resgate)
        {
            AplicacaoResponse aplicacaoResponse = new AplicacaoResponse
            {
                CDI = resgate.CDI,
                Imposto = resgate.Imposto,
                TB = resgate.TB,
                TotalImposto = resgate.TotalImposto,
                TotalLiquido = resgate.TotalLiquido,
                VF = resgate.VF,
                VI = resgate.VI
            };

            return new List<AplicacaoResponse> { aplicacaoResponse };
        }
    }
}
