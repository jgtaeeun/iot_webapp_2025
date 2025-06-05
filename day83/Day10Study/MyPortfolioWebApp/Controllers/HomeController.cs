using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using MyPortfolioWebApp.Models;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyPortfolioWebApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDBContext _context;

        public HomeController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> About()
        {  
            //정적HTML을 DB데이터로 동적처리
            //db에서 데이터를 불러온 뒤 about, skill객체에 데이터 담아서 뷰로 넘겨줌
            var skillCount = _context.skill.Count();
            var skill = await _context.skill.ToListAsync();

            //FirstAsync()는 데이터가 없으면 예외발생
            //FirstOrDefaultAsync()는 데이터가 없으면 널값
            var about = await _context.About.FirstOrDefaultAsync();

            ViewBag.SkillCount = skillCount;
            ViewBag.ColNum = (skillCount/2)+ 1;

            var model = new AboutModel();
            model.Skill = skill;
            model.About = about;
            return View(model);
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task <IActionResult> Contact(ContactModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var smtpClient = new SmtpClient("smtp.gmail.com")  //gmail사용
                    {
                        Port = 587, // ✅ 포트 수정: Gmail은 일반적으로 587번 사용
                        Credentials = new NetworkCredential("admin@gmail.com", "your-app-password"), // ⚠️ 앱 비밀번호 사용
                        EnableSsl = true

                    };
                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress("admin@gmail.com"),                //작성자의 메일주소
                        Subject = model.Subject ?? "[제목없음]" ,
                        Body = $"보낸사람 : {model.Name}({model.Email})\n\n메시지:{model.Message}",
                        IsBodyHtml = false,
                    };

                    mailMessage.To.Add("admin@gmail.com"); // 실제 수신자 주소
                    mailMessage.ReplyToList.Add(new MailAddress(model.Email)); // 사용자가 입력한 이메일
                    await smtpClient.SendMailAsync(mailMessage);    //위 생성된 메일객체 전송
                }
                catch (Exception ex)
                {
                    ViewBag.Success = false;
                    ViewBag.Error = $"메일전송 실패! {ex.Message}";
                }
            }

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
