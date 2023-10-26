using AutoMapper;
using Portal.Comunicacao.Requisicao;
using Portal.Comunicacao.Resposta;
using Portal.Domain.Entidade;

namespace Portal.Application.Servicos.AutoMapper;

public class AutoMapperConfiguracao : Profile
{
    public AutoMapperConfiguracao()
    {
        RequisicaoParaEntidade();
        EntidadeParaResposta();
    }

    private void RequisicaoParaEntidade()
    {
        CreateMap<RequisicaoRegistrarUsuarioJson, Usuario>()
            .ForMember(destino => destino.Senha, config => config.Ignore());

    }

    private void EntidadeParaResposta()
    {
        CreateMap<Usuario, RespostaUsuarioJson>();
        CreateMap<Tarefa, RespostaTarefaJson>();
    }
}
