using System.Runtime.Serialization;

namespace Portal.Exceptions.ExceptionBase;

[Serializable]
public class ErroCategoriaException : PortalException
{
    public List<string> MensagensDeErro { get; set; }

    public ErroCategoriaException(List<string> mensagensDeErro) : base(string.Empty)
    {
        MensagensDeErro = mensagensDeErro;
    }
    protected ErroCategoriaException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}
