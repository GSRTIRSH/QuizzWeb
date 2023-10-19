using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using CMS.Models;
using System.Linq;
using System.Collections.Generic;

namespace CMS.Controllers;

public class UserController : Controller
{
    private readonly UserContext _context;

    private const string BaseUrl = "http://localhsot:5200/api";

    private readonly ClientApi _clientApi;

    public UserController()
    {
        _context = new UserContext();
        _clientApi = new ClientApi(BaseUrl, new HttpClient());
    }

    // GET: Users/Index
    public async Task<IActionResult> Index()
    {
        //var c = _clientApi.GetAsync<List<User>>("user").Result;

        if (_context.Users == null) return NotFound();

        return _context.Users != null
            ? View(_context.Users.ToList())
            : Problem("Entity set 'CMS.Users'  is null.");
    }

    // GET: Users/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Users == null) return NotFound();

        var users = _context.Users
            .FirstOrDefault(m => m.Id == id);
        if (users == null) return NotFound();

        return View(users);
    }

    // GET: Users/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Users/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("Id,Title,DateStart,DateEnd,Subject,Auditory,Proffessor")]
        User user)
    {
        if (ModelState.IsValid)
        {
            _context.Users.Add(user);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(user);
    }

    // GET: Users/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Users == null) return NotFound();

        var users = _context.Users.Find(u => u.Id == id);
        if (users == null) return NotFound();
        return View(users);
    }

    // POST: Users/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("Id, Email, FirstName, SubName, PasswordHash")]
        User user)
    {
        if (id != user.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(user.Id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(user);
    }

    // GET: Users/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Users == null) return NotFound();

        var users = _context.Users
            .FirstOrDefault(m => m.Id == id);
        if (users == null) return NotFound();

        return View(users);
    }

    // POST: Users/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Users == null) return Problem("Entity set 'IcalWebAppContext.Users'  is null.");
        var users = _context.Users.Find(u => u.Id == id);
        if (users != null) _context.Users.Remove(users);

        //await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EventExists(int id)
    {
        return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
    }


    public async Task<IActionResult> Clear()
    {
        var users = _context.Users.ToList();

        foreach (var ev in users) _context.Users.Remove(ev);

        //await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}