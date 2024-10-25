using DemoPRN1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DemoPRN1.Pages.Custommer
{
    public class HomeModel : PageModel
    {
        private readonly PJPRN221Context _context = new PJPRN221Context();
		public List<Book> Allbook { get; set; } = new List<Book>();
        public List<Book> BestSellBooks { get; set; }
		public List<Book> TopRatedBooks { get; set; }
		public List<Category> Categories { get; set; } = new List<Category>();


		public void OnGet()
		{
			GetTopSellingBooks();
			GetTopRatedBooks();
			GetCategoryBook();
			GetAllBooks();
		}

		private void GetAllBooks()
		{
			Allbook = _context.Books.ToList();

			// Làm tròn giá cho từng sách trong danh sách
			Allbook.ForEach(b => b.Price = Math.Round(b.Price ?? 0, 2));
		}


		private void GetTopRatedBooks()
		{
			// Lấy 4 quyển sách có đánh giá cao nhất
			var topRatedBooks = _context.Books
				.Select(b => new
				{
					Book = b,
					AverageRating = b.Bookratings.Average(br => br.RatingId) // Tính điểm trung bình
				})
				.Where(b => b.AverageRating > 0) // Chỉ lấy những sách có đánh giá
				.OrderByDescending(b => b.AverageRating) // Sắp xếp theo điểm trung bình giảm dần
				.Take(4) // Lấy 4 quyển sách
				.ToList(); // Chuyển đổi thành danh sách

			TopRatedBooks = topRatedBooks.Select(b =>
			{
				b.Book.Price = Math.Round(b.Book.Price ?? 0, 2);
				return b.Book;
			})
			.ToList();
		}
		private void GetTopSellingBooks()
		{
			BestSellBooks = new List<Book>();

			// Tìm cuốn sách bán chạy nhất
			var bestSellingBooks = (from b in _context.Books
									join od in _context.Oderdetails on b.BookId equals od.BookId
									group od by new { b.BookId,b.BookTitle } into g
									select new
									{
										BookId = g.Key.BookId,
										BookTitle = g.Key.BookTitle,
										TotalSold = g.Sum(od => od.Quantity)
									})
					  .OrderByDescending(g => g.TotalSold)
					  .Take(3)
					  .ToList();

			foreach (var SellingBook in bestSellingBooks)
			{
				var book = _context.Books.FirstOrDefault(b => b.BookId == SellingBook.BookId);

				if (book != null)
				{
					BestSellBooks.Add(book);  // Thêm từng cuốn sách vào danh sách
				}
			}
		}
		private void GetCategoryBook()
		{
			Categories = _context.Categories.ToList();
		}


    }
}
