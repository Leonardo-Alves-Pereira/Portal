using Microsoft.AspNetCore.Mvc;
using Portal.Api.Filtros;
using Portal.Application.UseCases.Tarefa.Atualizar;
using Portal.Application.UseCases.Tarefa.Deletar;
using Portal.Application.UseCases.Tarefa.Listar;
using Portal.Application.UseCases.Tarefa.Registrar;
using Portal.Comunicacao.Requisicao;
using Portal.Comunicacao.Resposta;

namespace Portal.Api.Controllers;

[Route("api/[controller]")]
public class TarefaController : PortalController
{

    [HttpGet]
    [Route("listar")]
    [ProducesResponseType(typeof(RespostaTarefaJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar([FromServices] IListarTarefaUseCase useCase)
    {
        var resposta = await useCase.Executar();
        return Ok(resposta);
    }

    [HttpPost]
    [Route("listarId")]
    [ProducesResponseType(typeof(RespostaTarefaJson), StatusCodes.Status200OK)]
    public async Task<IActionResult> ListarId([FromServices] IListarTarefaIdUseCase useCase, [FromBody] GenericRequestIdJson id)
    {
        if (id is null)
            throw new ArgumentNullException(nameof(id));

        var resposta = await useCase.Executar(id.Id);
        return Ok(resposta);
    }

    [HttpPost]
    [Route("incluir")]
    [ProducesResponseType(typeof(RespostaTarefaJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Incluir([FromServices] IRegistrarTarefaUseCase useCase,
                                           [FromBody] RequisicaoTarefaJson request)
    {
        if (request is null)
            throw new Exception();

        var resposta = await useCase.Executar(request);
        return Created(string.Empty, resposta);
    }

    [HttpPut]
    [Route("alterar")]
    [ProducesResponseType(typeof(RespostaTarefaJson), StatusCodes.Status201Created)]
    public async Task<IActionResult> Alterar([FromServices] IAtualizarTarefaUseCase useCase,
                                   [FromBody] RequisicaoTarefaJson request)
    {
        if (request is null)
            throw new Exception();

        var resposta = await useCase.Executar(request);
        return Created(string.Empty, resposta);
    }

    [HttpPost]
    [Route("deletar")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Deletar([FromServices] IDeletarTarefaUseCase useCase,
                               [FromBody] RequisicaoTarefaJson id)
    {
        if (id is null)
            throw new Exception();

        var resposta = await useCase.Executar(id);
        return NoContent();
    }
}
