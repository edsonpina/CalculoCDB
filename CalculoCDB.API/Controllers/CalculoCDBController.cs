using CalculoCDB.API.Response;
using Microsoft.AspNetCore.Mvc;
using CalculoCDB.ApplicationCore.Domains.Entities;
using CalculoCDB.ApplicationCore.UseCases.Interfaces;

namespace CalculoCDB.API.Controllers
{
    [ApiController]
    [Route("CalculoCDB")]
    public class CalculoCDBController : ControllerBase
    {
        private readonly ILogger<CalculoCDBController> _logger;
        private readonly IConfiguration _configuration;
        private readonly IAplicacaoUseCase _aplicacaoUseCase;
        public CalculoCDBController(ILogger<CalculoCDBController> logger,
            IConfiguration configuration, IAplicacaoUseCase aplicacaoUseCase)
        {
            _logger = logger;
            _configuration = configuration;
            _aplicacaoUseCase = aplicacaoUseCase;
        }

        [HttpGet(Name = "Get")]
        public ActionResult<IEnumerable<AplicacaoResponse>> Get([FromHeader] decimal valorInicial, [FromHeader] int meses)
        {
            try
            {
                var aplicacao = new Aplicacao()
                {
                    ValorInicial = valorInicial,
                    Meses = meses
                };
                if (aplicacao.ValorInicial < 1)
                {
                    return BadRequest("Informe um valor válido");
                }
                if (aplicacao.Meses < 1 || aplicacao.Meses>99)
                {
                    return BadRequest("Informe um período válido");
                }
                var result = AplicacaoResponse.Converter(_aplicacaoUseCase.Calcular(aplicacao));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
    }
}
