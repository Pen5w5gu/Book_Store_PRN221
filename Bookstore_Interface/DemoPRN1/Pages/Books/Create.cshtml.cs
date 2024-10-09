using DemoPRN1.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DemoPRN1.Pages.Books
{
    public class CreateModel : PageModel
    {
        private readonly PJPRN221Context _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CreateModel(PJPRN221Context context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
        public Book newBook { get; set; }=new Book();
        [BindProperty]
        public List<Category> Categories { get; set; }
        [BindProperty]
        public IFormFile ImageFile { get; set; }
        public async Task<IActionResult> OnGet()
        {
            Categories =  _context.Categories.ToList();
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                Categories = await _context.Categories.ToListAsync();
                return Page();
            }
            if (ImageFile != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Images");

                string fileName = Guid.NewGuid().ToString() + "_" + ImageFile.FileName;

                string filePath = Path.Combine(uploadFolder, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(fileStream);
                }

                newBook.Image = "~/Images/" + fileName;
            }
            newBook.RentalQuantity = newBook.Quantity;
            newBook.CreateAt = DateTime.Now;
            newBook.UpdateAt = DateTime.Now;
            newBook.BookStoreId = 1;
            _context.Books.Add(newBook);
             _context.SaveChanges();

            return RedirectToPage("/Books/Index");
        }
    }
}
