using Microsoft.AspNetCore.Mvc;

namespace backend.Utils;

public static class ErrorManager
{
    public static ErrorResponse Error(Exception e)
    {
        ErrorResponse error;
        switch (e.Message)
        {
            case "NOT_FOUND":
                error = new ErrorResponse(StatusCodes.Status404NotFound,"Item not found", e.StackTrace);
                return error;
            case "UNAUTHORIZED":
                error = new ErrorResponse(StatusCodes.Status401Unauthorized, "The token is not valid", e.StackTrace);
                return error;
            case "NOT_CREATED":
                error = new ErrorResponse(StatusCodes.Status400BadRequest, "The entity is not created", e.StackTrace);
                return error;
            case "NOT_UPDATED":
                error = new ErrorResponse(StatusCodes.Status400BadRequest, "The entity is not updated", e.StackTrace);
                return error;
            case "USERNAME_EXISTS":
                error = new ErrorResponse(StatusCodes.Status409Conflict, "The username already exists", e.StackTrace);
                return error;
            case "ROLE_NONEXISTENT":
                error = new ErrorResponse(StatusCodes.Status406NotAcceptable, "The role is not valid", e.StackTrace);
                return error;
            case "UNKNOWN_CLASSROOM":
                error = new ErrorResponse(StatusCodes.Status409Conflict, "The Id classroom is not valid", e.StackTrace);
                return error;
            case "INVALID_PARAMETERS":
                error = new ErrorResponse(StatusCodes.Status409Conflict, "The paremeters aren't valid", e.StackTrace);
                return error;
            default:
                error = new ErrorResponse(StatusCodes.Status500InternalServerError,e.Message, e.StackTrace);
                return error;
        }
    }
}