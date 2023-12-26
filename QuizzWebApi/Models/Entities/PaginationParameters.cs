using Microsoft.AspNetCore.Mvc;

namespace QuizzWebApi.Models.Entities;

public class PaginationParameters
{
    private const int MaxPageSize = 100;
    private const int MinPageSize = 10;

    [BindProperty(Name = "pageNumber")] public int PageNumber { get; set; } = 1;

    private int _pageSize = 20;

    [BindProperty(Name = "pageSize")]
    public int PageSize
    {
        get => _pageSize;
        set
        {
            _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
            _pageSize = (value < MinPageSize) ? MinPageSize : value;
        }
    }
}