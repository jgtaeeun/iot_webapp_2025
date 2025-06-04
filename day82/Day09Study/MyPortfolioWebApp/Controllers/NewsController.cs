using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using MyPortfolioWebApp.Models;

namespace MyPortfolioWebApp.Controllers
{
    public class NewsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public NewsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: News
        public async Task<IActionResult> Index(int page = 1, string search = "")
        {
            //1. SELECT
            //return View(await _context.news.OrderByDescending(o=>o.PostdDate).ToListAsync());

            //2. 쿼리로 처리방식
            //var news = await _context.news.FromSql($@"SELECT Id, Writer, Title, Description, PostdDate, ReadCount
            //                            FROM news").OrderByDescending(o => o.PostdDate).ToListAsync();
            //return View(news);


            //3. 최종
            //int page = 1;                                       //현재페이지-기본1페이지
            var totalCount = _context.news.Where(n => EF.Functions.Like(n.Title, $"%{search}%")).Count();
            //var totalCount = _context.news.Count();            //게시물 총 갯수
            var countList = 10;                                 //한페이지에 10개의 게시글 보여줌
            var totalPage = totalCount / countList;             //전체 페이지수
            if (totalCount % countList > 0) { totalPage++; }       //남은 게시글이 있으면 페이지수 증가
            if (totalPage < page) page = totalPage;             
           
            //페이지번호 1~10
            var countPage = 10;                                 //가장 max 페이지수 :10
            var startPage = ((page - 1) / countPage) * countPage + 1;
            var endPage = startPage + countPage - 1;
            if (totalPage < endPage) endPage = totalPage;

            //게시글 번호 1~10, 11~20
            var startCount = ((page - 1) * countList) + 1;
            var endCount = startCount + countList - 1;

            ViewBag.StartPage = startPage;
            ViewBag.EndPage = endPage;
            ViewBag.Page = page;
            ViewBag.TotalPage = totalPage;
            ViewBag.Search = search;
            //저장프로시저
            var n = await _context.news.FromSql($@"call New_PagingBoard({startCount},{endCount},{search})").ToListAsync();
            return View(n);
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //SELECT + WHERE절
            var news = await _context.news
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }
            //조회수 증가
            news.ReadCount++;
            _context.news.Update(news);
            await _context.SaveChangesAsync();

            return View(news);
        }

        // GET: http://localhost:포트번호/News/Create
        public IActionResult Create()

        {
            return View();
        }

        // POST: News/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description")] News news)
        {
            
            if (ModelState.IsValid)
            {
                news.Writer = "비회원";
                news.PostdDate = DateTime.Now;
                news.ReadCount = 0;

                //INSERT
                _context.Add(news);
                //COMMIT
                await _context.SaveChangesAsync();

                TempData["success"] = "뉴스 생성 성공!";
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.news.FindAsync(id);
            if (news == null)
            {
                return NotFound();
            }
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  [Bind("Id,Title,Description")] News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try

                {
                    var existNews = await _context.news.FindAsync(id);
                    if (existNews == null)
                    {
                        return NotFound();
                    }
                  
                    existNews.PostdDate = DateTime.Now;
                    existNews.Title = news.Title;
                    existNews.Description = news.Description;

                  
                    await _context.SaveChangesAsync();
                    TempData["success"] = "뉴스 수정 성공!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.news
                .FirstOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {   //DELETE +WHERE절
            var news = await _context.news.FindAsync(id);
            if (news != null)
            {
                _context.news.Remove(news);
            }
            //COMMIT
            await _context.SaveChangesAsync();
            TempData["success"] = "뉴스 삭제 성공!";
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.news.Any(e => e.Id == id);
        }
    }
}
