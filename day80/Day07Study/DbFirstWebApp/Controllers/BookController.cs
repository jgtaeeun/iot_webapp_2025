﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DbFirstWebApp.Models;

namespace DbFirstWebApp.Controllers
{
    public class BookController : Controller
    {
        private readonly MadangContext _context;

        public BookController(MadangContext context)
        {
            _context = context;
        }

        // GET: Book
        public async Task<IActionResult> Index()
        {
            var madangContext = _context.Bookstbls.Include(b => b.DivisionNavigation);
            return View(await madangContext.ToListAsync());
        }

        // GET: Book/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookstbl = await _context.Bookstbls
                .Include(b => b.DivisionNavigation)
                .FirstOrDefaultAsync(m => m.Idx == id);
            if (bookstbl == null)
            {
                return NotFound();
            }

            return View(bookstbl);
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            ViewData["Division"] = new SelectList(_context.Divtbls, "Division", "Division");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Idx,Author,Division,Names,ReleaseDate,Isbn,Price")] Bookstbl bookstbl)
        {   // 불필요한 속성 제거
            ModelState.Remove(nameof(bookstbl.DivisionNavigation));

            if (ModelState.IsValid)
            {

                // DivisionNavigation 수동 설정
                bookstbl.DivisionNavigation = await _context.Divtbls
                    .FirstOrDefaultAsync(d => d.Division == bookstbl.Division);

                _context.Add(bookstbl);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Division"] = new SelectList(_context.Divtbls, "Division", "Division", bookstbl.Division);
            return View(bookstbl);
        }

        // GET: Book/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookstbl = await _context.Bookstbls.FindAsync(id);
            if (bookstbl == null)
            {
                return NotFound();
            }
            ViewData["Division"] = new SelectList(_context.Divtbls, "Division", "Division", bookstbl.Division);
            return View(bookstbl);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Idx,Author,Division,Names,ReleaseDate,Isbn,Price")] Bookstbl bookstbl)
        {
            if (id != bookstbl.Idx)
            {
                return NotFound();
            }
            // 불필요한 속성 제거
            ModelState.Remove(nameof(bookstbl.DivisionNavigation));

            if (ModelState.IsValid)
            {
                try
                {
                    // DivisionNavigation 수동 설정
                    bookstbl.DivisionNavigation = await _context.Divtbls
                        .FirstOrDefaultAsync(d => d.Division == bookstbl.Division);

                    _context.Update(bookstbl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookstblExists(bookstbl.Idx))
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
            ViewData["Division"] = new SelectList(_context.Divtbls, "Division", "Division", bookstbl.Division);
            return View(bookstbl);
        }

        // GET: Book/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookstbl = await _context.Bookstbls
                .Include(b => b.DivisionNavigation)
                .FirstOrDefaultAsync(m => m.Idx == id);
            if (bookstbl == null)
            {
                return NotFound();
            }

            return View(bookstbl);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookstbl = await _context.Bookstbls.FindAsync(id);
            if (bookstbl != null)
            {
                _context.Bookstbls.Remove(bookstbl);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookstblExists(int id)
        {
            return _context.Bookstbls.Any(e => e.Idx == id);
        }
    }
}
