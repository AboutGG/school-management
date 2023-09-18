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
        var dummy = context.Request.Query["role"].ToString();
        
        //tramite i servizi prendo il dbContext da utilizzare per prendere il ruolo dal token
        var dbContext = context.RequestServices.GetRequiredService<SchoolContext>();
        
        //controllo endpoint in modo da eseguire la funzione
        if (context.Request.Path.Value.Contains("api/students"))
        {
            try
            {
                //Ricavo lo userid decodificando il token
                var userid = Guid.Parse(JWTHandler.DecodeJwtToken(context.Request.Headers["token"]).Payload["userid"]
                    .ToString());
                
                //Prendo il ruolo in modo da controllare se ha i permessi necessari
                var role = RoleSearcher.GetRole(userid, dbContext);
                if (role.Trim().ToLower() != dummy.Trim().ToLower())
                {
                    throw new Exception("UNAUTHORIZED");
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