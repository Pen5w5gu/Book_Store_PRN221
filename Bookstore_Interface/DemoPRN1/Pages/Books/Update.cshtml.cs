using DemoPRN1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DemoPRN1.Pages.Books
{
    public class UpdateModel : PageModel
    {
        private readonly PJPRN221Context _context;

        public UpdateModel(PJPRN221Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Book updateBook { get; set; }
        [BindProperty]
        public List<Category> Categories { get; set; }  

        public async Task<IActionResult> OnGet(int id)
        {
           Categories = _context.Categories.ToList();

            updateBook = _context.Books.Include(b => b.Category).FirstOrDefault(b => b.BookId == id);

            if (updateBook == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            updateBook.UpdateAt = DateTime.Now;
            _context.Books.Update(updateBook);
            _context.SaveChanges();
            return RedirectToPage("/Books/Index");
        }
    }
}
