﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseWork2019.Data;
using CourseWork2019.Models;

namespace CourseWork2019.Controllers
{
    public class RubricsController : Controller
    {
        private readonly QuizContext _context;

        public RubricsController(QuizContext context)
        {
            _context = context;
        }

        // GET: Rubrics
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rubrics.ToListAsync());
        }

        // GET: Rubrics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rubric = await _context.Rubrics
                .FirstOrDefaultAsync(m => m.RubricID == id);
            if (rubric == null)
            {
                return NotFound();
            }

            return View(rubric);
        }

        // GET: Rubrics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rubrics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RubricID,RubricName")] Rubric rubric)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rubric);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rubric);
        }

        // GET: Rubrics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rubric = await _context.Rubrics.FindAsync(id);
            if (rubric == null)
            {
                return NotFound();
            }
            return View(rubric);
        }

        // POST: Rubrics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RubricID,RubricName")] Rubric rubric)
        {
            if (id != rubric.RubricID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rubric);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RubricExists(rubric.RubricID))
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
            return View(rubric);
        }

        // GET: Rubrics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rubric = await _context.Rubrics
                .FirstOrDefaultAsync(m => m.RubricID == id);
            if (rubric == null)
            {
                return NotFound();
            }

            return View(rubric);
        }

        // POST: Rubrics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rubric = await _context.Rubrics.FindAsync(id);
            _context.Rubrics.Remove(rubric);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RubricExists(int id)
        {
            return _context.Rubrics.Any(e => e.RubricID == id);
        }
    }
}
