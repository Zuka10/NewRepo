using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SignalRChat.DataAccess;
using System.Security.Claims;

namespace SignalRChat.Pages;

public class IndexModel(ChatContext context) : PageModel
{
    private readonly ChatContext _context = context;

    [BindProperty]
    public new required ApplicationUser User { get; set; }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(User.UserName) || string.IsNullOrWhiteSpace(User.Password))
        {
            return Page();
        }

        var authenticatedUser = _context.ApplicationUsers.FirstOrDefault(u => u.UserName == User.UserName)!;

        if (authenticatedUser.Password == User.Password && authenticatedUser.UserName == User.UserName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, authenticatedUser.UserName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(30)
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
            HttpContext.Session.SetString("Username", authenticatedUser.UserName);
            return RedirectToPage("/Home");
        }

        ModelState.AddModelError(string.Empty, "Invalid username or password.");
        return Page();
    }
}