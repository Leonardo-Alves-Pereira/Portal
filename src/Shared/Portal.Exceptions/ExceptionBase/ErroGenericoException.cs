using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Portal.Exceptions.ExceptionBase;

[Serializable]
public class ErroGenericoException : PortalException
{
    public List<Comunicacao.Resposta.ErroValidacaoJson> MensagensDeErro { get; set; }

    public ErroGenericoException(List<Comunicacao.Resposta.ErroValidacaoJson> mensagensDeErro) : base(string.Empty)
    {
        MensagensDeErro = mensagensDeErro;
    }
    protected ErroGenericoException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}
