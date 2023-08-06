using backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace backend.Utils;

public class JWT
{
    public static string GenerateJwtToken(User user)
    {
        // Definisci la chiave segreta come un array di byte
        byte[] key = Encoding.ASCII.GetBytes("DZq7JkJj+z0O8TNTvOnlmj3SpJqXKRW44Qj8SmsW8bk=");

        var id = user.Id.ToString();
        // Crea una lista di claims (informazioni) per il token
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
            new (JwtRegisteredClaimNames.Sub, user.Username),
            new("userid", user.Id.ToString())
        };
        
        // Crea il token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(30),
            // Issuer = issuer,
            // Audience = audience,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        // Crea il token handler
        var tokenHandler = new JwtSecurityTokenHandler();

        // Genera il token
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // Converte il token in una stringa
        var tokenString = tokenHandler.WriteToken(token);

        return tokenString;
    }
}