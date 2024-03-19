using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SignalRChat.DataAccess;

namespace SignalRChat.Pages;

public class RegisterModel(ChatContext context) : PageModel
{
    private readonly ChatContext _context = context;

    [BindProperty]
    public new required ApplicationUser User { get; set; }

    [BindProperty]
    public required string ConfirmPassword { get; set; }

    public IActionResult OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(User.UserName) || string.IsNullOrWhiteSpace(User.Password) || string.IsNullOrWhiteSpace(ConfirmPassword) || User.Password != ConfirmPassword)
        {
            return Page();
        }

        HttpContext.Session.SetString("Username", User.UserName);

        _context.ApplicationUsers.Add(User);
        _context.SaveChanges();

        return RedirectToPage("/Home");
    }
}