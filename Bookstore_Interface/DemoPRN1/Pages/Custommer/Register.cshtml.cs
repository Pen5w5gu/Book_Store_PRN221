using DemoPRN1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DemoPRN1.Pages.Custommer
{
    public class RegisterModel : PageModel
    {
        private readonly PJPRN221Context _context;

        public RegisterModel(PJPRN221Context context)
        {
            _context = context;
        }

        public Bookstore newBooktore { get; set; } = new Bookstore();
        [BindProperty]
        public RegisterInputModel Input { get; set; }

        public class RegisterInputModel
        {


            [Required(ErrorMessage = "tên không được bỏ trống")]
            public string Name { get; set; }


            [Required(ErrorMessage = "Địa chỉ không được bỏ trống")]
            public string Address { get; set; }
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            int? userId = HttpContext.Session.GetInt32("UserId");
             
            Bookstore bookstore = _context.Bookstores.Include(x=>x.Account).FirstOrDefault(b=>b.AccountId == userId.Value);
            if (bookstore != null) {
                ModelState.AddModelError(string.Empty, "Tài khoản này đã được đăng kí.Vui lòng chờ xét duyệt");
                return Page();
            }

            newBooktore.StoreName = Input.Name;
            newBooktore.Address = Input.Address;
            newBooktore.Status = false;
            newBooktore.CreateAt = DateTime.Now;
            newBooktore.UpdateAt = DateTime.Now;
            newBooktore.AccountId = userId.Value;
            _context.Bookstores.Add(newBooktore);
            _context.SaveChanges();
            TempData["SuccessMessage"] = "Đăng ký thành công! Haỹ đợi để được xét duyệt";
            return Page();
        }

    }
}
