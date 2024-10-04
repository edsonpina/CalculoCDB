using Moq;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using CalculoCDB.ApplicationCore.UseCases;
using CalculoCDB.ApplicationCore.Domains.Entities;
using System.Collections.Generic;

public class AplicacaoUseCaseTests
{
    private readonly Mock<ILogger<AplicacaoUseCase>> _loggerMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly AplicacaoUseCase _aplicacaoUseCase;

    public AplicacaoUseCaseTests()
    {
        _loggerMock = new Mock<ILogger<AplicacaoUseCase>>();

        _configurationMock = new Mock<IConfiguration>();

        _configurationMock.Setup(x => x["TB"]).Returns("2.5");
        _configurationMock.Setup(x => x["CDI"]).Returns("9.5");
        _configurationMock.Setup(x => x["Imposto:Ate06Meses"]).Returns("225");
        _configurationMock.Setup(x => x["Imposto:Ate12Meses"]).Returns("200");
        _configurationMock.Setup(x => x["Imposto:Ate24Meses"]).Returns("175");
        _configurationMock.Setup(x => x["Imposto:Acima24Meses"]).Returns("150");

        _aplicacaoUseCase = new AplicacaoUseCase(_loggerMock.Object, _configurationMock.Object);
    }

    [Fact]
    public void Calcular_DeveRetornarValoresCorretos_ParaValoresValidos()
    {
        var aplicacao = new Aplicacao
        {
            ValorInicial = 20000,
            Meses = 1
        };

        var result = _aplicacaoUseCase.Calcular(aplicacao);

        Xunit.Assert.NotNull(result);
        Xunit.Assert.Equal("R$20475,00", result.VF); 
        Xunit.Assert.Equal("R$106,88", result.TotalImposto); 
        Xunit.Assert.Equal("R$20368,13", result.TotalLiquido);
        Xunit.Assert.Equal("22,5%", result.Imposto);
        Xunit.Assert.Equal("9,5%", result.CDI);
        Xunit.Assert.Equal("25,00%", result.TB);
    }

    [Fact]
    public void Calcular_DeveUsarPercentualImpostoCorreto_ParaDiferentesPeriodos()
    {
        var aplicacaoSeisMeses = new Aplicacao { ValorInicial = 1000m, Meses = 6 };
        var aplicacaoDozeMeses = new Aplicacao { ValorInicial = 1000m, Meses = 12 };
        var aplicacaoVinteQuatroMeses = new Aplicacao { ValorInicial = 1000m, Meses = 24 };
        var aplicacaoAcimaVinteQuatroMeses = new Aplicacao { ValorInicial = 1000m, Meses = 36 };

        var resultSeisMeses = _aplicacaoUseCase.Calcular(aplicacaoSeisMeses);
        var resultDozeMeses = _aplicacaoUseCase.Calcular(aplicacaoDozeMeses);
        var resultVinteQuatroMeses = _aplicacaoUseCase.Calcular(aplicacaoVinteQuatroMeses);
        var resultAcimaVinteQuatroMeses = _aplicacaoUseCase.Calcular(aplicacaoAcimaVinteQuatroMeses);

        Xunit.Assert.Equal("22,5%", resultSeisMeses.Imposto); 
        Xunit.Assert.Equal("20%", resultDozeMeses.Imposto); 
        Xunit.Assert.Equal("17,5%", resultVinteQuatroMeses.Imposto); 
        Xunit.Assert.Equal("15%", resultAcimaVinteQuatroMeses.Imposto);
    }

    [Fact]
    public void Calcular_DeveRetornarResultadoEsperadoPara1Mes()
    {
        // Arrange
        var aplicacao = new Aplicacao
        {
            ValorInicial = 1000m,
            Meses = 1
        };

        // Act
        var result = _aplicacaoUseCase.Calcular(aplicacao);

        // Xunit.Assert
        Xunit.Assert.NotNull(result);
        Xunit.Assert.True(result.VF.StartsWith("R$1023,75")); // Valor final esperado para 1 mês
    }
}
