using Microsoft.AspNetCore.Mvc;
using Portal.Application.UseCases.Usuario.Registrar;
using Portal.Comunicacao.Requisicao;
using Portal.Application.UseCases.Usuario.AlterarSenha;
using Portal.Api.Filtros;
using Portal.Comunicacao.Resposta;

namespace Portal.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    [HttpPost]
    [Route("registro")]
    [ProducesResponseType(typeof(RespostaUsuarioRegistradoJson), StatusCodes.Status201Created)]
    [ServiceFilter(typeof(LoginAttribute))]
    public async Task<IActionResult> RegistrarUsuario([FromServices] IRegistrarUsuarioUseCase useCase, [FromBody] RequisicaoRegistrarUsuarioJson usuario)
    {
       var resposta = await useCase.Executar(usuario);
       return Created("Sucesso" ,resposta);
    }

    [HttpPut]
    [Route("alterar-senha")]
    [ProducesResponseType(typeof(RespostaUsuarioRegistradoJson), StatusCodes.Status204NoContent)]
    [ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
    public async Task<IActionResult> AlterarSenha([FromServices] IAlterarSenhaUseCase useCase, [FromBody] AlterarSenhaJson usuario)
    {
        await useCase.Executar(usuario);
        return NoContent();
    }
}