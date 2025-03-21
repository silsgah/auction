using System.Security.Claims;
using IdentityModel;
using IdentityService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityService.Pages.Register;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    public Index(UserManager<ApplicationUser> userManager){
        _userManager = userManager;
    }

    [BindProperty]
    public RegisterViewModel Input{get;set;}

    [BindProperty]
    public bool RegisterSuccess {get;set;}

    [BindProperty]
    public bool ErrorsExist { get; set; }

    [BindProperty]
    public IEnumerable<string> Errors { get; set; }

    public IActionResult OnGet(string returnUrl)
    {
        Console.WriteLine("we are here in the post");
        Input = new RegisterViewModel{
            ReturnUrl = returnUrl,
        };
        return Page();
    }

   public async Task<IActionResult> OnPost()
   {
    Console.WriteLine("we are here in the post SECTION");
    if(Input.Button !="register") return Redirect("~/");
    Console.WriteLine("we are here in the posting...");
    if(ModelState.IsValid)
    {
        var user = new ApplicationUser{
            UserName = Input.Username,
            Email = Input.Email,
            EmailConfirmed = true
        };
        var result = await _userManager.CreateAsync(user, Input.Password);
        if (!result.Succeeded)
        {
            ErrorsExist = true;
            Errors = result.Errors.Select(x => $"{x.Description} ({x.Code})");
            return Page();
        }
        if(result.Succeeded)
        {
            await _userManager.AddClaimsAsync(user, new Claim[]{
                new Claim(JwtClaimTypes.Name, Input.FullName)
            });

            RegisterSuccess = true;
        }
    }
    return Page();
   }
}