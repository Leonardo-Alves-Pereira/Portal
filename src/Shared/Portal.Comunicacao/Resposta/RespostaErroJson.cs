namespace Portal.Comunicacao.Resposta;

public class RespostaErroJson
{
    public List<string> Mensagem { get; set; }

    public RespostaErroJson(string mensagem)
    {
        Mensagem = new List<string>
        {
            mensagem
        };
    }

    public RespostaErroJson(List<string> mensagens)
    {
        Mensagem = mensagens;
    }
}
