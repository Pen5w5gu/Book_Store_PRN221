using DemoPRN1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DemoPRN1.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly PJPRN221Context _context = new PJPRN221Context();

        public List<Book> Books { get; set; }

        public void OnGet()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");         
             var bookStore = _context.Bookstores.Include(a => a.Books).FirstOrDefault(a => a.AccountId == userId.Value);
            Books = _context.Books.Include(b => b.Category).Where(b=>b.BookStoreId==bookStore.BookStoreId).ToList();
        }
        public IActionResult OnPostDelete(int id) // Phương thức không sử dụng async
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            _context.SaveChanges(); // Lưu thay đổi

            return RedirectToPage("/BookStore/Books/Index");
        }
    }
}
