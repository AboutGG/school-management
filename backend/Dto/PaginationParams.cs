using System.Linq.Expressions;

namespace backend.Dto;

public class PaginationParams
{
    private int _maxItemPerPage = 10;
    private int itemsPerPage = 2;
    private string search = string.Empty;
    public int Page { get; set; } = 1;
    public string? Role { get; set; } = null;
    
    public string? Search
    {
        get => search; 
        set => search = string.IsNullOrWhiteSpace(value) ? search : value;
    }

    public string OrderType { get; set; }
    public string Order { get; set; } = "Name";
    public int ItemsPerPage
    {
        get => itemsPerPage; 
        set => itemsPerPage = value > _maxItemPerPage ? _maxItemPerPage: value; 
        
    }
}