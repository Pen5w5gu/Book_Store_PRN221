using DemoPRN1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DemoPRN1.Pages.Custommer
{
    public class BookDetailModel : PageModel
    {



        public readonly PJPRN221Context _context;

        public Book currentBook;

        public List<Book> recomendBooks;

        public BookDetailModel(PJPRN221Context context)
        {
            _context = context;
        }
		public async Task<IActionResult> OnGetAsync(int bookId)
        {
            if(bookId != null)
            {
                currentBook = _context.Books.FirstOrDefault(b => b.BookId == bookId);
            }

            

            recomendBooks = GetRandomBooks(await _context.Books.ToListAsync(), 4);
            return Page();
        }

        //hàm lấy ngẫu nhiên 4 quyển sách
		public static List<Book> GetRandomBooks(List<Book> books, int count )
		{
			Random random = new Random();
			return books.OrderBy(b => random.Next()).Take(count).ToList();
		}
	}
}
