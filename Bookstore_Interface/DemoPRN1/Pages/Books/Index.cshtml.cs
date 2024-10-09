using DemoPRN1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DemoPRN1.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly PJPRN221Context _context=new PJPRN221Context();

        public List<Book> Books { get; set; }
        
        public void OnGet()
        {
            Books = _context.Books.Include(b=>b.Category).ToList();
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

            return RedirectToPage("/Books/Index");
        }
    }
}
