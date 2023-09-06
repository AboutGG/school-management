namespace backend.Utils;

public class ErrorResponse
{
    private readonly int statusCode;
    private readonly string message;
    
    public ErrorResponse(int statusCode, string message)
    {
        this.statusCode = statusCode;
        this.message = message;
    }
}