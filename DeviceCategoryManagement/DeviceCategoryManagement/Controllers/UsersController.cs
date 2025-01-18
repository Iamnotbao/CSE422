using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceCategoryManagement.Data;
using DeviceCategoryManagement.Models;

namespace DeviceCategoryManagement.Controllers
{
    public class UsersController : Controller
    {
        private readonly DeviceCategoryManagementContext _context;
        public static List<User> users = new List<User>
{
    new User(1, "Bao", "Baole@gmail.com", "090190414"),
    new User(2, "Nhat", "Nhatle@gmail.com", "095590414"),
};

        public UsersController(DeviceCategoryManagementContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            ViewBag.User = users;
            return View(users);
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phone")] User user)
        {
            if (ModelState.IsValid)
            {
                users.Add(user);
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editUser = users.FirstOrDefault(u => u.Id == id);
            if (editUser == null)
            {
                return NotFound();
            }
            return View(editUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phone")] User user)
        {
            var editUser = users.FirstOrDefault(u => u.Id == id);
            if (editUser == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    editUser.Name = user.Name;
                    editUser.Email = user.Email;
                    editUser.Phone = user.Phone;

                }
                catch
                {
                    Console.WriteLine("Not ok!!!");
                }
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deleteUser = users.FirstOrDefault(c => c.Id == id);
            if (deleteUser == null)
            {
                return NotFound();
            }
            return View(deleteUser);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deleteUser = users.FirstOrDefault(c => c.Id == id);
            if (deleteUser == null)
            {
                return Problem("Entity set 'DeviceCategoryManagementContext.Device'  is null.");
            }
            if (deleteUser != null)
            {
                users.Remove(deleteUser);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.User?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
