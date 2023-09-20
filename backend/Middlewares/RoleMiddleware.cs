using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using backend.Models;
using backend.Utils;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Org.BouncyCastle.Crypto.Tls;
using JsonConverter = System.Text.Json.Serialization.JsonConverter;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

namespace backend.Middleware;

/// <summary> Middleware che permette di controllare il ruolo tramite il token che manda il FE quando effettua determinate chiamate </summary>
public class RoleMiddleware : IMiddleware
{
    
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var conditionRole = context.Request.Headers["Role"].ToString();
        
        //tramite i servizi prendo il dbContext da utilizzare per prendere il ruolo dal token
        var dbContext = context.RequestServices.GetRequiredService<SchoolContext>();
        
        //controllo endpoint in modo da eseguire la funzione
        if (context.Request.Path.Value.Contains("api/students") || context.Request.Path.Value.Contains("api/teachers") || context.Request.Path.Value.Contains("api/users"))
        {
            try
            {
                //Ricavo lo userid decodificando il token
                var userid = Guid.Parse(JWTHandler.DecodeJwtToken(context.Request.Headers["Token"]).Payload["userid"]
                    .ToString());
                
                //Prendo il ruolo in modo da controllare se ha i permessi necessari
                var role = RoleSearcher.GetRole(userid, dbContext);

                if (role == "unknown" || (conditionRole.Trim().ToLower() != "student" && conditionRole.Trim().ToLower() != "teacher"))
                {
                    throw new Exception("ROLE_NONEXISTENT");
                }
                if (role.Trim().ToLower() != conditionRole.Trim().ToLower())
                {
                    throw new Exception("UNAUTHORIZED");
                }

                switch (context.Request.Path.Value)
                {
                    case var s when s.Contains("/api/teachers") || s.Contains("/api/users"):
                        if (role != "teacher")
                            throw new Exception("UNAUTHORIZED");
                        break;
                    case var s when s.Contains("/api/students"):
                        if (role != "student")
                            throw new Exception("UNAUTHORIZED");
                        break;
                }
                await next(context);
            }
            catch (Exception e)
            {
                ErrorResponse error = ErrorManager.Error(e);
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                context.Response.ContentType = "text/plain";
                var jsonErrorResponse = JsonConvert.SerializeObject(error);
                await context.Response.WriteAsync(jsonErrorResponse);
                await context.Response.CompleteAsync();
            }
        }
        else
        {
            await next(context);
        }
    }
}

/// <summary> Classe che permette di utilizzare il Middleware creato sopra </summary>
public static class ClassWithNoImplementationMiddleware
{
    public static IApplicationBuilder UseClassWithNoImplementationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RoleMiddleware>();
    }
}