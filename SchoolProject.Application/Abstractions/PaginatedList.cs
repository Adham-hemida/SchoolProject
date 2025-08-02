using Microsoft.EntityFrameworkCore;

namespace SchoolProject.Application.Abstractions;
public class PaginatedList<T>
{
	public PaginatedList(List<T> items, int pageNumber, int count, int pageSize)
	{
		Items = items;
		PageNumber = pageNumber;
		TotalPages = (int)Math.Ceiling(count / (double)pageSize);
	}
	public List<T> Items { get; private set; }
	public int PageNumber { get; private set; }
	public int TotalPages { get; private set; }

	public bool HasPreviousPage => PageNumber > 1;
	public bool HasNextPage => PageNumber < TotalPages;



	public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize, CancellationToken cancellationToken)
	{
		var count = await source.CountAsync(cancellationToken);
		var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync(cancellationToken);
		return new PaginatedList<T>(items, pageNumber, count, pageSize);
	}
	

}

