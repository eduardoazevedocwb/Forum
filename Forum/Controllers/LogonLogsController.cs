using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forum.Models;

namespace Forum.Controllers
{
    public class LogonLogsController : Controller
    {
        private readonly ForumContext _context;

        public LogonLogsController(ForumContext context)
        {
            _context = context;
        }

        // GET: LogonLogs
        public async Task<IActionResult> Index()
        {
            var forumContext = _context.LogonLogs.Include(l => l.User);
            return View(await forumContext.ToListAsync());
        }

        // GET: LogonLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logonLog = await _context.LogonLogs
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LogonLogId == id);
            if (logonLog == null)
            {
                return NotFound();
            }

            return View(logonLog);
        }

        // GET: LogonLogs/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Name");
            return View();
        }

        // POST: LogonLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LogonLogId,UserId,Action,Date,LogData")] LogonLog logonLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(logonLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Name", logonLog.UserId);
            return View(logonLog);
        }

        // GET: LogonLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logonLog = await _context.LogonLogs.FindAsync(id);
            if (logonLog == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Name", logonLog.UserId);
            return View(logonLog);
        }

        // POST: LogonLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LogonLogId,UserId,Action,Date,LogData")] LogonLog logonLog)
        {
            if (id != logonLog.LogonLogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(logonLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LogonLogExists(logonLog.LogonLogId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Name", logonLog.UserId);
            return View(logonLog);
        }

        // GET: LogonLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var logonLog = await _context.LogonLogs
                .Include(l => l.User)
                .FirstOrDefaultAsync(m => m.LogonLogId == id);
            if (logonLog == null)
            {
                return NotFound();
            }

            return View(logonLog);
        }

        // POST: LogonLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var logonLog = await _context.LogonLogs.FindAsync(id);
            _context.LogonLogs.Remove(logonLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LogonLogExists(int id)
        {
            return _context.LogonLogs.Any(e => e.LogonLogId == id);
        }
    }
}
