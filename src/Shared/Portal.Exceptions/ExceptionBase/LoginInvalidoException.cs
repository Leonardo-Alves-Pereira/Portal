using Portal.Exceptions.Resources;
using System.Runtime.Serialization;

namespace Portal.Exceptions.ExceptionBase;

[Serializable]
public class LoginInvalidoException : PortalException
{
    public LoginInvalidoException() : base(ResourceErrorMessage.LOGIN_INVALIDO)
    {   
    }
}
