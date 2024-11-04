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
<<<<<<< HEAD
            int? userId = HttpContext.Session.GetInt32("UserId");         
             var bookStore = _context.Bookstores.Include(a => a.Books).FirstOrDefault(a => a.AccountId == userId.Value);
            Books = _context.Books.Include(b => b.Category).Where(b=>b.BookStoreId==bookStore.BookStoreId).ToList();
=======
            int? userid=HttpContext.Session.GetInt32("UserId");
            Books = _context.Books.Include(b => b.Category).Where(b => b.BookStore.AccountId == userid).ToList();
>>>>>>> origin/Thang
        }
        public IActionResult OnPostDelete(int id) 
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
        [HttpPost]
        public IActionResult OnPut(int id, int quantity)
        {
            var book = _context.Books.FirstOrDefault(b => b.BookId == id);
            if (book == null)
            {
                return NotFound();
            }
            book.Quantity += quantity;
            _context.Books.Update(book);
            _context.SaveChanges();

            return RedirectToPage("/BookStore/Books/Index");
        }

    }
}
