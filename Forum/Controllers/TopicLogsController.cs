using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forum.Models;
using System.Text.Json;

namespace Forum.Controllers
{
    public class TopicLogsController : Controller
    {
        private readonly ForumContext _context;

        public TopicLogsController(ForumContext context)
        {
            _context = context;
        }

        // GET: TopicLogs
        public async Task<IActionResult> Index()
        {
            var forumContext = _context.TopicLogs.Include(t => t.Topic);
            return View(await forumContext.ToListAsync());
        }

        // GET: TopicLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var topicLog = await _context.TopicLogs
                .Include(t => t.Topic)
                .FirstOrDefaultAsync(m => m.TopicLogId == id);
            if (topicLog == null)
                return NotFound();

            return View(topicLog);
        }

        // GET: TopicLogs/Create
        public IActionResult Create()
        {
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Description");
            return View();
        }

        // POST: TopicLogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopicLogId,TopicId,Action,Date,LogData")] TopicLog topicLog)
        {
            if (ModelState.IsValid)
            {
                _context.TopicLogs.Add(topicLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Description", topicLog.TopicId);
            return View(topicLog);
        }

        // GET: TopicLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var topicLog = await _context.TopicLogs.FindAsync(id);
            if (topicLog == null)
                return NotFound();

            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Description", topicLog.TopicId);
            return View(topicLog);
        }

        // PUT: TopicLogs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TopicLogId,TopicId,Action,Date,LogData")] TopicLog topicLog)
        {
            if (id != topicLog.TopicLogId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topicLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopicLogExists(topicLog.TopicLogId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TopicId"] = new SelectList(_context.Topics, "TopicId", "Description", topicLog.TopicId);
            return View(topicLog);
        }

        // GET: TopicLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var topicLog = await _context.TopicLogs
                .Include(t => t.Topic)
                .FirstOrDefaultAsync(m => m.TopicLogId == id);
            if (topicLog == null)
            {
                return NotFound();
            }

            return View(topicLog);
        }

        // DELETE: TopicLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topicLog = await _context.TopicLogs.FindAsync(id);
            _context.TopicLogs.Remove(topicLog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopicLogExists(int id)
        {
            return _context.TopicLogs.Any(e => e.TopicLogId == id);
        }
    }
}
