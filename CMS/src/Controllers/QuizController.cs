using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using CMS.Models;
using System.Linq;
using System.Collections.Generic;

namespace CMS.Controllers;

public class QuizController : Controller
{
    private readonly QuizContext _context;

    private const string BaseUrl = "http://localhsot:5200/api";

    private readonly ClientApi _clientApi;

    public QuizController()
    {
        _context = new QuizContext();
        _clientApi = new ClientApi(BaseUrl, new HttpClient());
    }

    // GET: Quizzes/Index
    public async Task<IActionResult> Index()
    {
        if (_context.Quizzes == null) return NotFound();

        return _context.Quizzes != null
            ? View(_context.Quizzes.ToList())
            : Problem("Entity set 'CMS.Quizzes'  is null.");
    }

    // GET: Quizzes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Quizzes == null) return NotFound();

        var quizzes = _context.Quizzes
            .FirstOrDefault(m => m.Id == id);
        if (quizzes == null) return NotFound();

        return View(quizzes);
    }

    // GET: Quizzes/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Quizzes/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("")] Quiz quiz)
    {
        if (ModelState.IsValid)
        {
            _context.Quizzes.Add(quiz);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(quiz);
    }

    // GET: Quizzes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Quizzes == null) return NotFound();

        var quizzes = _context.Quizzes.Find(u => u.Id == id);
        if (quizzes == null) return NotFound();
        return View(quizzes);
    }

    // POST: Quizzes/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("")] Quiz quiz)
    {
        if (id != quiz.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(quiz.Id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(quiz);
    }

    // GET: Quizzes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Quizzes == null) return NotFound();

        var quizzes = _context.Quizzes
            .FirstOrDefault(m => m.Id == id);
        if (quizzes == null) return NotFound();

        return View(quizzes);
    }

    // POST: Quizzes/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Quizzes == null) return Problem("Entity set 'IcalWebAppContext.Quizzes'  is null.");
        var quizzes = _context.Quizzes.Find(u => u.Id == id);
        if (quizzes != null) _context.Quizzes.Remove(quizzes);

        //await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EventExists(int id)
    {
        return (_context.Quizzes?.Any(e => e.Id == id)).GetValueOrDefault();
    }


    public async Task<IActionResult> Clear()
    {
        var quizzes = _context.Quizzes.ToList();

        foreach (var ev in quizzes) _context.Quizzes.Remove(ev);

        //await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}