namespace backend.Models;
/// <summary> This class is used when an API call which returns a list needs to have the data for the pagination </summary>
public class PaginationResponse <T>
{
    public int Total { get; set; }

    public object Data { get; set; }
}