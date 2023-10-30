using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CMS.Data;
using CMS.Models;
using Microsoft.AspNetCore.Authorization;

namespace CMS.Controllers;

//[Authorize]
public class QuestionController : Controller
{
    private readonly QuestionContext _context;

    private const string BaseUrl = "http://localhsot:5200/api";

    private readonly ClientApi _clientApi;

    public QuestionController()
    {
        _context = new QuestionContext();
        _clientApi = new ClientApi(BaseUrl, new HttpClient());
    }

    // GET: Questions/Index
    public async Task<IActionResult> Index()
    {
        if (_context.Questions == null) return NotFound();

        return _context.Questions != null
            ? View(_context.Questions)
            : Problem("Entity set 'CMS.Questions'  is null.");
    }

    // GET: Questions/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Questions == null) return NotFound();

        var quizzes = _context.Questions
            .FirstOrDefault(m => m.Id == id);
        if (quizzes == null) return NotFound();

        return View(quizzes);
    }

    // GET: Questions/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Questions/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        [Bind("")] QuestionV2 question)
    {
        if (ModelState.IsValid)
        {
            _context.Questions.Add(question);
            //await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        return View(question);
    }

    // GET: Questions/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Questions == null) return NotFound();

        var quizzes = _context.Questions.Find(u => u.Id == id);
        if (quizzes == null) return NotFound();
        return View(quizzes);
    }

    // POST: Questions/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id,
        [Bind("")] QuestionV2 question)
    {
        if (id != question.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EventExists(question.Id))
                    return NotFound();
                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        return View(question);
    }

    // GET: Questions/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Questions == null) return NotFound();

        var quizzes = _context.Questions
            .FirstOrDefault(m => m.Id == id);
        if (quizzes == null) return NotFound();

        return View(quizzes);
    }

    // POST: Questions/Delete/5
    [HttpPost]
    [ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Questions == null) return Problem("Entity set 'IcalWebAppContext.Questions'  is null.");
        var quizzes = _context.Questions.Find(u => u.Id == id);
        if (quizzes != null) _context.Questions.Remove(quizzes);

        //await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EventExists(int id)
    {
        return (_context.Questions?.Any(e => e.Id == id)).GetValueOrDefault();
    }


    public async Task<IActionResult> Clear()
    {
        var quizzes = _context.Questions.ToList();

        foreach (var ev in quizzes) _context.Questions.Remove(ev);

        //await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}