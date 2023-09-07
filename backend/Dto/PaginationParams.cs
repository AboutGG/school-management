namespace backend.Dto;

public class PaginationParams
{
    private int _maxItemPerPage = 100;
    private int itemsPerPage = 10;
    private string search = string.Empty;
    private int page = 1;

    /// <summary> This variable contains the current page number. The default value is 1</summary>
    public int Page
    {
        get => page; 
        set => page = value == null || value < 1 ? page : value;
    }
    
    /// <summary> This variable contains the roles to filter the element in a table. The default value is null</summary>
    public string? Filter { get; set; } = null;
    
    /// <summary> This variable contains the string to search in a table record</summary>
    public string? Search
    {
        get => search; 
        set => search = string.IsNullOrWhiteSpace(value) ? search : value;
    }

    /// <summary> Order type can be asc or desc, by default is asc. </summary>
    public string OrderType { get; set; } = "asc";
    
    /// <summary> Order accept by default Name. This variable indicate the column name which you can order. </summary>
    public string Order { get; set; } = "Id";
    
    /// <summary> The number of item per page, by default you can have 10 items per page</summary>
    public int ItemsPerPage
    {
        get => itemsPerPage; 
        set => itemsPerPage = value > _maxItemPerPage ? _maxItemPerPage: value; 
        
    }
}