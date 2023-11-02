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
                error = new ErrorResponse(StatusCodes.Status404NotFound, "The entity has not found", e.StackTrace);
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
                error = new ErrorResponse(StatusCodes.Status409Conflict, "The parameters aren't valid", e.StackTrace);
                return error;
            case "UNAUTHORIZED_UPDATE_EXAM":
                error = new ErrorResponse(StatusCodes.Status401Unauthorized,
                    "You can't update this exam because you are not assigned on this classroom and/or subject",
                    e.StackTrace);
                return error;
            case "INVALID_SCHOOL_YEAR":
                error = new ErrorResponse(StatusCodes.Status400BadRequest,
                    "The school year which you send is not valid", e.StackTrace);
                return error;
            case "UNAUTHORIZED_STUDENT_PROMOTION":
                error = new ErrorResponse(StatusCodes.Status400BadRequest,
                    "You are not authorized to proceed with the student promotion", e.StackTrace);
                return error;
            case "INVALID_PDF_TYPE":
                error = new ErrorResponse(StatusCodes.Status400BadRequest, "The type of the PDF is not valid",
                    e.StackTrace);
                return error;
            case "UNAUTHORIZED_QUARTER_REPORT":
                error = new ErrorResponse(StatusCodes.Status401Unauthorized, "You can't access to the quarter reports",
                    e.StackTrace);
                return error;
            case "CHANGE_PASSWORD_ACCESS_DENIED":
                error = new ErrorResponse(StatusCodes.Status401Unauthorized,
                    "You haven't the permissions to change the password of another user", e.StackTrace);
                return error;
            case "EQUALS_PASSWORDS":
                error = new ErrorResponse(StatusCodes.Status409Conflict,
                    "The new password can't be equal then the older", e.StackTrace);
                return error;
            case "PASSWORD_NOT_CHANGED":
                error = new ErrorResponse(StatusCodes.Status400BadRequest,
                    "The password has not changed", e.StackTrace);
                return error;
            case "UNAUTHORIZED_DETAIL":
                error = new ErrorResponse(StatusCodes.Status401Unauthorized,
                    "You are not authorized to access on the other users detail", e.StackTrace);
                return error;
            case "INVALID_OLD_PASSWORD":
                error = new ErrorResponse(StatusCodes.Status401Unauthorized,
                    "The new password is equal then the older.", e.StackTrace);
                return error;
            case "INVALID_STUDENT_CLASSROOM":
                error = new ErrorResponse(StatusCodes.Status404NotFound,
                    "In this classroom there isn't this student", e.StackTrace);
                return error;
            case "UNAUTHORIZED_STUDENT_GRADES":
                error = new ErrorResponse(StatusCodes.Status401Unauthorized,
                    "You can't access to the final mean of the student", e.StackTrace);
                return error;
            case "SCHOOL_YEAR_NOT_FOUND":
                error = new ErrorResponse(StatusCodes.Status401Unauthorized,
                    "The student hasn't the school year", e.StackTrace);
                return error;
            case "STUDENT_HASNT_EXAMS":
                error = new ErrorResponse(StatusCodes.Status401Unauthorized,
                    "The student doesn't have any exam", e.StackTrace);
                return error;
            default:
                error = new ErrorResponse(StatusCodes.Status400BadRequest, e.Message, e.StackTrace);
                return error;
        }
    }
}