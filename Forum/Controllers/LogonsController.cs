using System;
using System.Collections.Generic;
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
    public class LogonsController : Controller
    {
        private readonly ForumContext _context;

        public LogonsController(ForumContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var forumContext = _context.TopicLogs.Include(t => t.Topic);
            return View("Logon");
        }
        public async Task<IActionResult> Logon(string User, string Password)
        {
            var username = User?.Trim().ToUpper();
            var password = Password?.Trim().ToUpper();
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return View("LogonFail");

            var user = _context.Users.Where(a => a.UserName.Trim().ToUpper() == username && a.Password.Trim().ToUpper() == password).FirstOrDefault();

            if (user != null)
            {
                var log = new LogonLog()
                {
                    UserId = user.UserId,
                    User = user,
                    Date = DateTime.Now,
                    Action = "LOGON",
                    LogData = JsonSerializer.Serialize(user),
                };
                var logsController = new LogonLogsController(_context);
                await logsController.Create(log);

                HttpContext.Session.SetString("UserId", user.UserId.ToString());
                HttpContext.Session.SetString("Name",user.Name);
                HttpContext.Session.SetString("UserName", user.UserName);
                HttpContext.Session.SetString("DateTimeOn", DateTime.Now.ToString());
                return View("LogonSuccess");
            }
            else
                return View("LogonFail");
        }
    }
}
