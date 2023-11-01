using Microsoft.AspNetCore.Mvc;
using Portal.Api.Filtros;
using Portal.Application.UseCases.Login.FazerLogin;
using Portal.Comunicacao.Requisicao;
using Portal.Comunicacao.Resposta;

namespace Portal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(RespostaLoginJson), StatusCodes.Status200OK)]
    [ServiceFilter(typeof(LoginAttribute))]
    public async Task<IActionResult> Login(
        [FromServices] ILoginUseCase useCase,
        [FromBody] RequisicaoLoginJson requisicao)
    {
        var resposta = await useCase.Executar(requisicao);
        return Ok(resposta);
    }
}
