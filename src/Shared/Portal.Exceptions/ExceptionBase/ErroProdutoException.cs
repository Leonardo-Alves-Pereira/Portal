using System.Runtime.Serialization;

namespace Portal.Exceptions.ExceptionBase;

[Serializable]
public class ErroTarefaException : PortalException
{
    public List<string> MensagensDeErro { get; set; }

    public ErroTarefaException(List<string> mensagensDeErro) : base(string.Empty)
    {
        MensagensDeErro = mensagensDeErro;
    }
    protected ErroTarefaException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}
