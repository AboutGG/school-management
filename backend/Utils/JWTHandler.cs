using backend.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace backend.Utils;

public class JWTHandler
{
    private static string secretKey = "DZq7JkJj+z0O8TNTvOnlmj3SpJqXKRW44Qj8SmsW8bk=";
    public static string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        // Definisci la chiave segreta come un array di byte
        byte[] key = Encoding.ASCII.GetBytes(secretKey);
        // Crea una lista di claims (informazioni) per il token
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()), 
            new (JwtRegisteredClaimNames.Sub, user.Username),
            new("userid", user.Id.ToString()),
            new ("role", user.Teacher != null ? "teacher" : user.Student != null ? "student": "unknown")
        };
        
        // Crea il token descriptor
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(8),
            // Issuer = issuer,
            // Audience = audience,
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        

        // Genera il token
        var token = tokenHandler.CreateToken(tokenDescriptor);

        // Converte il token in una stringa
        var tokenString = tokenHandler.WriteToken(token);
        return tokenString;
    }
    
    public static JwtSecurityToken DecodeJwtToken(string token)
    {
        // Definisci la chiave segreta come un array di byte
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey));

        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false
        };

        SecurityToken decodedToken;
        tokenHandler.ValidateToken(token, tokenValidationParameters, out decodedToken);

        return decodedToken as JwtSecurityToken;
    }
}