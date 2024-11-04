using DemoPRN1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace DemoPRN1.Pages.Custommer
{
    public class CartModel : PageModel
    {
        // CartItem với cartItem là 2 cái khác nhau
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public async Task<IActionResult> OnGetAsync()
        {
            var cart = HttpContext.Session.GetString("addCart");
            // cartItem được lưu theo format    idBook_type
            Dictionary<string, int> cartItems = string.IsNullOrEmpty(cart) ? new Dictionary<string, int>() : JsonConvert.DeserializeObject<Dictionary<string, int>>(cart);
            TempData["CartCount"] = cartItems.Count;
            foreach (KeyValuePair<string, int> item in cartItems)
            {
                int bookId = int.Parse(item.Key.Split("_")[0]);
                string type = item.Key.Split('_')[1];

                using (var _context = new PJPRN221Context()) 
                {
                    Book book = await _context.Books
                        .Include(b => b.Category) 
                        .FirstOrDefaultAsync(b => b.BookId == bookId);
                    Items.Add(new CartItem
                    {
                        book = book,
                        quantity = item.Value,
                        type = type
                    });
                }
            }

            return Page();
        }
    }
}

public class CartItem
{
    public Book book { get; set; }
    public int quantity { get; set; }

    public string type {  get; set; }
}
