using Moq;
using Xunit;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using CalculoCDB.API.Controllers;
using CalculoCDB.API.Response;
using CalculoCDB.ApplicationCore.Domains.Entities;
using CalculoCDB.ApplicationCore.UseCases.Interfaces;
using CalculoCDB.ApplicationCore.UseCases;
using Microsoft.AspNetCore.Mvc;

public class CalculoCDBControllerTests
{
    private readonly Mock<IAplicacaoUseCase> _aplicacaoUseCaseMock;
    private readonly CalculoCDBController _controller;

    public CalculoCDBControllerTests()
    {
        _aplicacaoUseCaseMock = new Mock<IAplicacaoUseCase>();

    _controller = new CalculoCDBController(null, null, _aplicacaoUseCaseMock.Object);
    }

    [Fact]
    public void Get_ReturnsExpectedAplicacaoResponses()
    {
        decimal valorInicial = 20000;
        int meses = 1;
        var aplicacao = new Aplicacao
        {
            ValorInicial = valorInicial,
            Meses = meses
        };

        var aplicacaoCalculada = new List<AplicacaoResponse>
        {
            new AplicacaoResponse { VF = Convert.ToString(1200)},
            new AplicacaoResponse { VF = Convert.ToString(1300) }
        };
        Resgate resgate = new Resgate()
        {
            VF = "R$20194,40",
            VI = "R$20000,00",
            CDI = "0,9%",
            TB = "108,00%",
            Imposto = "22,5%",
            TotalImposto = "R$43,74",
            TotalLiquido = "R$20150,66"
        };

        _aplicacaoUseCaseMock
            .Setup(a => a.Calcular(It.IsAny<Aplicacao>()))
            .Returns(resgate);

        var result = _controller.Get(valorInicial, meses);

        var okResult = Xunit.Assert.IsType<OkObjectResult>(result.Result);
        var returnedResponses = Xunit.Assert.IsAssignableFrom<IEnumerable<AplicacaoResponse>>(okResult.Value);
        Xunit.Assert.NotNull(result);
        Xunit.Assert.Equal(1, returnedResponses.Count());
        Xunit.Assert.Equal("R$20194,40", returnedResponses.First().VF);
    }
    [Fact]
    public void Get_ReturnsExpectedAplicacaoResponsesFail()
    {
        var result = _controller.Get(-10,-1);
        Xunit.Assert.Equal(null,result.Value);
    }

    [Fact]
    public void Get_ShouldReturnBadRequest_WhenValorInicialIsLessThanOrEqualToZero()
    {
        var result = _controller.Get(0, 12);

        var badRequestResult = Xunit.Assert.IsType<BadRequestObjectResult>(result.Result);
        Xunit.Assert.Equal("Informe um valor válido", badRequestResult.Value);
    }

    [Fact]
    public void Get_ShouldReturnBadRequest_WhenMesesIsLessThanOrEqualToZero()
    {
        var result = _controller.Get(1000, 0);

        var badRequestResult = Xunit.Assert.IsType<BadRequestObjectResult>(result.Result);
        Xunit.Assert.Equal("Informe um período válido", badRequestResult.Value);
    }

    [Fact]
    public void Get_ShouldReturnNotFound_WhenAplicacaoUseCaseReturnsNoResults()
    {
        decimal valorInicial = 1000;
        int meses = 12;

        _aplicacaoUseCaseMock.Setup(x => x.Calcular(It.IsAny<Aplicacao>()))
            .Returns(new Resgate());

        var result = _controller.Get(valorInicial, meses);

        Xunit.Assert.Equal(null, result.Value);
    }


    [Fact]
    public void Get_ShouldReturnInternalServerError_WhenExceptionIsThrown()
    {
        decimal valorInicial = 1000;
        int meses = 12;

        _aplicacaoUseCaseMock.Setup(x => x.Calcular(It.IsAny<Aplicacao>()))
            .Throws(new System.Exception("Erro no cálculo"));

        var result = _controller.Get(valorInicial, meses);

        var internalServerErrorResult = Xunit.Assert.IsType<ObjectResult>(result.Result);
        Xunit.Assert.Equal(500, internalServerErrorResult.StatusCode);
        Xunit.Assert.Equal("Erro no cálculo", internalServerErrorResult.Value);
    }
}
