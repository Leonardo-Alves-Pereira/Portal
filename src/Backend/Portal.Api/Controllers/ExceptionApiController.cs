
using Microsoft.AspNetCore.Mvc;
using Portal.Api.Filtros;

namespace Portal.Api.Controllers;

[ServiceFilter(typeof(UsuarioAutenticadoAttribute))]
[ApiController]
[Route("[controller]")]
public class ExceptionApiController : PortalController
{
    public ExceptionApiController() { }

}
