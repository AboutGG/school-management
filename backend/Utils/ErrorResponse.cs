namespace backend.Utils;

public class ErrorResponse
{
    public readonly int statusCode;
    public readonly string message;
    public readonly string detail;
    
    public ErrorResponse(int statusCode, string message, string detail)
    {
        this.statusCode = statusCode;
        this.message = message;
        this.detail = detail;
    }
}