using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace backend.Utils;

public class JWT
{
    public string GenerateJwtToken(User user)
    {
        // Qui dovresti utilizzare una libreria per generare e firmare il token JWT con le informazioni dell'utente.
        // Ad esempio, utilizzando System.IdentityModel.Tokens.Jwt o una libreria di gestione dei token JWT.

        // Esempio semplificato:
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("DZq7JkJj+z0O8TNTvOnlmj3SpJqXKRW44Qj8SmsW8bk="));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(30), // Imposta la scadenza del token
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}