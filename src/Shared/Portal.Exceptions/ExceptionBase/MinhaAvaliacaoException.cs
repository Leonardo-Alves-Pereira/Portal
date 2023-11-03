using Portal.Exceptions.Resources;
using System;
using System.Runtime.Serialization;

namespace Portal.Exceptions.ExceptionBase;

[Serializable]
public class PortalException : SystemException
{
    public PortalException(string mensagem) : base(mensagem)
    {
    }
    protected PortalException(SerializationInfo info, StreamingContext context) : base(info, context)
    {

    }
}
