using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolioWebApp.Models;
using System.Collections.Immutable;
using System.Diagnostics;
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


        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
