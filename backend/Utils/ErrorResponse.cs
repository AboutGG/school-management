namespace backend.Utils;

public class ErrorResponse
{
    public readonly int statusCode;
    public readonly string message;
    
    public ErrorResponse(int statusCode, string message)
    {
        this.statusCode = statusCode;
        this.message = message;
    }
}