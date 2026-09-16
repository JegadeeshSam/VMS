using Microsoft.AspNetCore.Mvc;
using VisitorManagementSystem.Models;
using VisitorManagementSystem.Repositories;

namespace VisitorManagementSystem.Controllers;

public class VisitorController : Controller
{
    private readonly IVisitorRepository _visitorRepository;

    public VisitorController(IVisitorRepository visitorRepository)
    {
        _visitorRepository = visitorRepository;
    }

    // GET: Visitor
    public async Task<IActionResult> Index()
    {
        var visitors = await _visitorRepository.GetAllAsync();
        return View(visitors);
    }

    // GET: Visitor/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var visitor = await _visitorRepository.GetByIdAsync(id.Value);
        if (visitor == null)
        {
            return NotFound();
        }

        return View(visitor);
    }

    // GET: Visitor/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Visitor/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("VisitorId,VisitorName,CompanyName,MobileNo,Email,IDProof")] Visitor visitor)
    {
        if (ModelState.IsValid)
        {
            await _visitorRepository.AddAsync(visitor);
            return RedirectToAction(nameof(Index));
        }
        return View(visitor);
    }

    // GET: Visitor/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var visitor = await _visitorRepository.GetByIdAsync(id.Value);
        if (visitor == null)
        {
            return NotFound();
        }
        return View(visitor);
    }

    // POST: Visitor/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("VisitorId,VisitorName,CompanyName,MobileNo,Email,IDProof")] Visitor visitor)
    {
        if (id != visitor.VisitorId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _visitorRepository.UpdateAsync(visitor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await VisitorExists(visitor.VisitorId))
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
        return View(visitor);
    }

    // POST: Visitor/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _visitorRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    // API: Get all visitors as JSON
    [HttpGet]
    public async Task<IActionResult> GetVisitors()
    {
        var visitors = await _visitorRepository.GetAllAsync();
        return Json(visitors);
    }

    private async Task<bool> VisitorExists(int id)
    {
        var visitor = await _visitorRepository.GetByIdAsync(id);
        return visitor != null;
    }
}
