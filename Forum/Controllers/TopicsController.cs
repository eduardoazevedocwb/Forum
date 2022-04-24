using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Forum.Models;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace Forum.Controllers
{
    public class TopicsController : Controller
    {
        private readonly ForumContext _context;

        public TopicsController(ForumContext context)
        {
            _context = context;
        }

        // GET: Topics
        public async Task<IActionResult> Index()
        {
            var forumContext = _context.Topics.Include(t => t.User);
            return View(await forumContext.OrderByDescending(a => a.CreationDate).ToListAsync());
        }

        // GET: Topics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var topic = await _context.Topics
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.TopicId == id);
            if (topic == null)
                return NotFound();

            return View(topic);
        }

        // GET: Topics/Create
        public IActionResult Create()
        {
            if (HttpContext?.Session?.Keys?.Contains("UserId") != true)
                return View("~/Views/Logons/Logon.cshtml");

            var loggedId = HttpContext?.Session?.GetString("UserId").ToString();
            var idUser = string.IsNullOrEmpty(loggedId) ? 0 : Convert.ToInt32(loggedId);
            if (idUser == 0)
                return View("~/Views/Logons/Logon.cshtml");

            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Name");
            return View();
        }

        // POST: Topics/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TopicId,Title,Description,UserId,CreationDate,Active")] Topic topic)
        {
            if (HttpContext?.Session == null)
                return View("~/Views/Logons/Logon.cshtml");

            var userId = HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userId))
                return View("~/Views/Logons/Logon.cshtml");

            var user = _context.Users.Where(a => a.UserId == Convert.ToInt32(userId)).FirstOrDefault();
            if(user == null)
                return View("~/Views/Logons/Logon.cshtml");

            if (ModelState.IsValid)
            {
                try
                {
                    topic.UserId = user.UserId;
                    topic.User = user;
                    topic.CreationDate = DateTime.Now;
                    topic.Active = true;

                    var item = _context.Add(topic);

                    await _context.SaveChangesAsync();
                    await PushTopicLog(topic);

                    return RedirectToAction(nameof(Index));
                }
                catch(Exception ex)
                {
                    return StatusCode(500, $"{ex.Message} | {ex.InnerException.Message}");
                }
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", topic.UserId);
            return View(topic);
        }

        // GET: Topics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var topic = await _context.Topics.FindAsync(id);
            if (topic == null)
                return NotFound();

            if (HttpContext?.Session?.Keys?.Contains("UserId") != true)
                return View("~/Views/Logons/Logon.cshtml");

            var loggedId = HttpContext?.Session?.GetString("UserId").ToString();
            var idUser = string.IsNullOrEmpty(loggedId) ? 0 : Convert.ToInt32(loggedId);
            if (idUser == 0)
                return View("~/Views/Logons/Logon.cshtml");

            if(idUser != topic.UserId)
                return View("NotAllowed");

            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Name", topic.UserId);
            return View(topic);
        }

        // POST: Topics/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TopicId,Title,Description,UserId,CreationDate,Active")] Topic topic)
        {
            if (id != topic.TopicId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    var register = await _context.Topics.FindAsync(id);
                    if(register != null) 
                    {
                        register.Title = topic.Title;
                        register.Description = topic.Description;
                        register.Active = topic.Active;
                        register.CreationDate = DateTime.Now;

                        var changes = await _context.SaveChangesAsync();
                        if( changes > 0 )
                        {
                            var newItem = register;
                            await PushTopicLog(newItem);
                        }
                    }
                    else
                        return NotFound();
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"{ex.Message} | {ex.InnerException.Message}");
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", topic.UserId);
            return View(topic);
        }

        // GET: Topics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var topic = await _context.Topics
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.TopicId == id);
            if (topic == null)
                return NotFound();

            if (HttpContext?.Session?.Keys?.Contains("UserId") != true)
                return View("~/Views/Logons/Logon.cshtml");

            var loggedId = HttpContext?.Session?.GetString("UserId").ToString();
            var idUser = string.IsNullOrEmpty(loggedId) ? 0 : Convert.ToInt32(loggedId);
            if (idUser == 0)
                return View("~/Views/Logons/Logon.cshtml");

            if (idUser != topic.UserId)
                return View("NotAllowed");

            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var topic = await _context.Topics.FindAsync(id);

            try
            {
                _context.Topics.Remove(topic);
                await _context.SaveChangesAsync();
                await PushTopicLog(topic);

                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"{ex.Message} | {ex.InnerException.Message}");
            }
        }

        private bool TopicExists(int id)
        {
            return _context.Topics.Any(e => e.TopicId == id);
        }

        private async Task PushTopicLog(Topic topic)
        {
            var newLog = new TopicLog()
            {
                Topic = topic,
                TopicId = topic.TopicId,
                Date = DateTime.Now,
                Action = "PUSH",
                LogData = JsonSerializer.Serialize(topic),
            };
            var logsController = new TopicLogsController(_context);
            await logsController.Create(newLog);
        }
    }
}
