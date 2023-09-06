using Microsoft.AspNetCore.Mvc;

namespace backend.Utils;

public static class ErrorManager
{
    public static ErrorResponse Error(string e)
    {
        switch (e)
        {
            case "NOT_FOUND":
                return new ErrorResponse(StatusCodes.Status404NotFound,"Items not found");
            case "UNAUTHORIZED":
                return new ErrorResponse(StatusCodes.Status401Unauthorized, "The token is not valid");
            default:
                return new ErrorResponse(StatusCodes.Status400BadRequest, string.Empty);
        }
    }
}