using Microsoft.AspNetCore.Mvc;

namespace backend.Utils;

public static class ErrorManager
{
    public static ErrorResponse Error(Exception e)
    {
        ErrorResponse dummy;
        switch (e.Message)
        {
            case "NOT_FOUND":
                dummy = new ErrorResponse(StatusCodes.Status404NotFound,"Item not found", e.StackTrace);
                return dummy;
            case "UNAUTHORIZED":
                dummy = new ErrorResponse(StatusCodes.Status401Unauthorized, "The token is not valid", e.StackTrace);
                return dummy;
            default:
                dummy = new ErrorResponse(StatusCodes.Status500InternalServerError,e.Message, e.StackTrace);
                return dummy;
        }
    }
}