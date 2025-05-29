using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        public async Task<IActionResult> Index()
        {
            //SELECT
            return View(await _context.news.ToListAsync());
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
        public async Task<IActionResult> Create([Bind("Id,Writer,Title,Description,PostdDate,ReadCount")] News news)
        {
            
            if (ModelState.IsValid)
            {
                //INSERT
                _context.Add(news);
                //COMMIT
                await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Writer,Title,Description,PostdDate,ReadCount")] News news)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {   //UPDATE
                    _context.Update(news);
                    //COMMIT
                    await _context.SaveChangesAsync();
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
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.news.Any(e => e.Id == id);
        }
    }
}
