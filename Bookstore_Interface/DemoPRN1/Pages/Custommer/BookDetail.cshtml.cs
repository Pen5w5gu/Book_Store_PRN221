using DemoPRN1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
			if (bookId != null)
			{
				currentBook = _context.Books.FirstOrDefault(b => b.BookId == bookId);
			}

			//phần session để lưu Cart
			var cart = HttpContext.Session.GetString("addCart");
			Dictionary<string, int> cartItems = string.IsNullOrEmpty(cart) ? new Dictionary<string, int>() : JsonConvert.DeserializeObject<Dictionary<string, int>>(cart);
			TempData["CartCount"] = cartItems.Count;

			recomendBooks = GetRandomBooks(await _context.Books.ToListAsync(), 4);
			return Page();
		}

		//hàm lấy ngẫu nhiên 4 quyển sách
		public List<Book> GetRandomBooks(List<Book> books, int count)
		{
			Random random = new Random();
			return books.OrderBy(b => random.Next()).Take(count).ToList();
		}

		// hàm add ca
		public async Task<IActionResult> OnPostAddToCart(int bookId, string type)
		{
			var cart = HttpContext.Session.GetString("addCart");

			// Lấy thông tin giỏ hàng từ Session
			Dictionary<string, int> cartItems = string.IsNullOrEmpty(cart) ? new Dictionary<string, int>() : JsonConvert.DeserializeObject<Dictionary<string, int>>(cart);

			string cartTypeAndName = bookId.ToString() + "_" + type;
			if (cartItems.ContainsKey(cartTypeAndName))
			{
				cartItems[cartTypeAndName]++;
			}
			else
			{
				cartItems[cartTypeAndName] = 1;
			}

			HttpContext.Session.SetString("addCart", JsonConvert.SerializeObject(cartItems));

			TempData["CartCount"] = cartItems.Count;

			return await OnGetAsync(bookId);
		}

	}
}
