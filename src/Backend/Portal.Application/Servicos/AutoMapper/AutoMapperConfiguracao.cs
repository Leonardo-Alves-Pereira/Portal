using AutoMapper;
using Portal.Comunicacao.Requisicao;
using Portal.Comunicacao.Resposta;
using Portal.Domain.Entidade;

namespace Portal.Application.Servicos.AutoMapper;

public class AutoMapperConfiguracao : Profile
{
    public AutoMapperConfiguracao()
    {
        JsonParaEntidade();
        EntidadeParaJson();
    }

    private void JsonParaEntidade()
    {
        CreateMap<RespostaTarefaJson, Tarefa>();
        CreateMap<RequisicaoTarefaJson, Tarefa>();
        CreateMap<RequisicaoUsuarioJson, Usuario>();
        CreateMap<RespostaUsuarioJson, Usuario>();
        CreateMap<RequisicaoRegistrarUsuarioJson, Usuario>()
            .ForMember(destino => destino.Senha, config => config.Ignore());

    }

    private void EntidadeParaJson()
    {
        CreateMap<Usuario, RespostaUsuarioJson>();
        CreateMap<Usuario, RequisicaoUsuarioJson>();
        CreateMap<Tarefa, RespostaTarefaJson>();
        CreateMap<Tarefa, RequisicaoTarefaJson>();

    }
}
