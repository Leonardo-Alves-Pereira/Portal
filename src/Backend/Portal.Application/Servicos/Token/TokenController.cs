using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft;

namespace Portal.Application.Servicos.Token;

public class TokenController
{
    private const string emailAlias = "eml";
    private readonly double _tempoDeVidaTokenMinutos;
    private readonly string _chaveToken;

    public TokenController(double tempoDeVidaTokenMinutos, string chaveToken)
    {
        _tempoDeVidaTokenMinutos = tempoDeVidaTokenMinutos;
        _chaveToken = chaveToken;
    }

    public string GerarToken(string emailDoUsuario)
    {
        var claims = new List<Claim>
        {
        new Claim(emailAlias, emailDoUsuario)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_tempoDeVidaTokenMinutos),
            SigningCredentials = new SigningCredentials(SimetricKey(), SecurityAlgorithms.HmacSha256Signature)
        };
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(securityToken);
    }

    public ClaimsPrincipal ValidarToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var parametrosValidacao = new TokenValidationParameters
        {
            RequireExpirationTime = true,
            IssuerSigningKey = SimetricKey(),
            ClockSkew = new TimeSpan(0),
            ValidateIssuer = false,
            ValidateAudience = false
        };
        var claims = tokenHandler.ValidateToken(token, parametrosValidacao, out _);
        return claims;
    }

    public string RecuperarEmail(string token)
    {
        var claims = ValidarToken(token);
        return claims.FindFirst(emailAlias).Value;
    }

    private SymmetricSecurityKey SimetricKey()
    {
        var symetricKey = Convert.FromBase64String(_chaveToken);
        return new SymmetricSecurityKey(symetricKey);
    }

}
