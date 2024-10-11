using DemoPRN1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DemoPRN1.Pages.Custommer
{
    public class ListBookcshtmlModel : PageModel
    {
        private readonly PJPRN221Context _context;

        public ListBookcshtmlModel(PJPRN221Context context)
        {
            _context = context;
        }

        public List<Category> Categories { get; set; }
        public List<Book> Books { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public int CurrentCategoryId {  get; set; }

		public async Task<IActionResult> OnGetAsync(int? categoryId, string searchString, int pageNumber = 1)
		{
			int pageSize = 4;
			Categories = await _context.Categories.ToListAsync();

			IQueryable<Book> query = _context.Books.Include(b => b.Category);

			// L?c theo th? lo?i
			if (categoryId.HasValue && categoryId != 0)
			{
				query = query.Where(b => b.CategoryId == categoryId);
				CurrentCategoryId = categoryId.Value;
			}

			// Tìm ki?m sách theo tên
			if (!string.IsNullOrEmpty(searchString))
			{
				query = query.Where(b => b.BookTitle.Contains(searchString));
			}

			int totalBooks = await query.CountAsync();
			PagingInfo = new PagingInfo
			{
				CurrentPage = pageNumber,
				TotalItems = totalBooks,
				ItemsPerPage = pageSize,
			};

			Books = await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
			return Page();
		}
	}
}

public class PagingInfo
{
	public int TotalItems { get; set; }
	public int ItemsPerPage { get; set; }
	public int CurrentPage { get; set; }
	public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
}
