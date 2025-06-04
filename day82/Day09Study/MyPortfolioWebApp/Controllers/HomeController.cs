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
            //����HTML�� DB�����ͷ� ����ó��
            //db���� �����͸� �ҷ��� �� about, skill��ü�� ������ ��Ƽ� ��� �Ѱ���
            var skillCount = _context.skill.Count();
            var skill = await _context.skill.ToListAsync();

            //FirstAsync()�� �����Ͱ� ������ ���ܹ߻�
            //FirstOrDefaultAsync()�� �����Ͱ� ������ �ΰ�
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
