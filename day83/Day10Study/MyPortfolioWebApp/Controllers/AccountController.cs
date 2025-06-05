using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyPortfolioWebApp.Models;

namespace MyPortfolioWebApp.Controllers
{
    public class AccountController : Controller
    {
        //ASP.NET Core Identity필요한 변ㅌ수
        private readonly UserManager<CustomUser> userManager;
        private readonly SignInManager<CustomUser> signInManager;

        public AccountController(UserManager<CustomUser> userManager , SignInManager<CustomUser> signInManager)
        {
            //userManager나 signInManager에 null값이 들어오면 안됨
            this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            this.signInManager = signInManager?? throw new ArgumentNullException(nameof(signInManager));
        }

        //NewsController의 GET Create(), Post Create()와 동일하게 생각
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                //아이디를 이메일로 사용
                var user = new CustomUser { UserName= model.Email, Email = model.Email , PhoneNumber= model.PhoneNumber 
                , Mobile = model.Mobile, City = model.City, Hobby = model.Hobby}; 
                var result = await userManager.CreateAsync(user, model.Password);   //자동으로 db에 저장

                if (result.Succeeded)
                {
                    //위의 저장한 사용자로 로그인, isPersistent는 로그인상태 유지
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors) 
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                
            }
            return View(model); //회원가입 오류시 그대로 회원가입 화면 유지
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction(actionName:"Index", controllerName:"Home");
                }
                ModelState.AddModelError("","로그인 실패");
                

            }
            return View(model); //회원가입 오류시 그대로 회원가입 화면 유지
        }


        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
