using System.Runtime.Serialization;

namespace Portal.Exceptions.ExceptionBase;

[Serializable]
public class ErroDeValidacaoException : PortalException
{
    public List<string> MensagensDeErro { get; set; }

    public ErroDeValidacaoException(List<string> mensagensDeErro) : base(string.Empty)
    {
        MensagensDeErro = mensagensDeErro;
    }
    protected ErroDeValidacaoException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}
