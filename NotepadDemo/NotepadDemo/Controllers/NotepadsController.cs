using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NotepadDemo.Data;
using NotepadDemo.Models.NotepadModels;

namespace NotepadDemo.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class NotepadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public NotepadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Notepads
        public async Task<IActionResult> Index()
        {
            return View(await _context.Notepad.ToListAsync());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Notepads/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notepad = await _context.Notepad
                .SingleOrDefaultAsync(m => m.Id == id);
            if (notepad == null)
            {
                return NotFound();
            }

            return View(notepad);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: Notepads/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Notepads/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        /// <summary>
        /// 
        /// </summary>
        /// <param name="notepad"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Content,CreateTime")] Notepad notepad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(notepad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(notepad);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Notepads/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notepad = await _context.Notepad.SingleOrDefaultAsync(m => m.Id == id);
            if (notepad == null)
            {
                return NotFound();
            }
            return View(notepad);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="notepad"></param>
        /// <returns></returns>
        // POST: Notepads/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Title,Content,CreateTime")] Notepad notepad)
        {
            if (id != notepad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(notepad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NotepadExists(notepad.Id))
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
            return View(notepad);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Notepads/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notepad = await _context.Notepad
                .SingleOrDefaultAsync(m => m.Id == id);
            if (notepad == null)
            {
                return NotFound();
            }

            return View(notepad);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // POST: Notepads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var notepad = await _context.Notepad.SingleOrDefaultAsync(m => m.Id == id);
            _context.Notepad.Remove(notepad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NotepadExists(long id)
        {
            return _context.Notepad.Any(e => e.Id == id);
        }
    }
}
