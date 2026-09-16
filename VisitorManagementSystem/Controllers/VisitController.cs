using Microsoft.AspNetCore.Mvc;
using VisitorManagementSystem.Models;
using VisitorManagementSystem.Repositories;

namespace VisitorManagementSystem.Controllers;

public class VisitController : Controller
{
    private readonly IVisitRepository _visitRepository;
    private readonly IVisitorRepository _visitorRepository;
    private readonly IEmployeeRepository _employeeRepository;

    public VisitController(
        IVisitRepository visitRepository,
        IVisitorRepository visitorRepository,
        IEmployeeRepository employeeRepository)
    {
        _visitRepository = visitRepository;
        _visitorRepository = visitorRepository;
        _employeeRepository = employeeRepository;
    }

    // GET: Visit
    public async Task<IActionResult> Index()
    {
        var visits = await _visitRepository.GetAllAsync();
        return View(visits);
    }

    // GET: Visit/Register
    public async Task<IActionResult> Register()
    {
        ViewBag.Visitors = new SelectList(await _visitorRepository.GetAllAsync(), "VisitorId", "VisitorName");
        ViewBag.Employees = new SelectList(await _employeeRepository.GetActiveEmployeesAsync(), "EmployeeId", "EmployeeName");
        return View();
    }

    // POST: Visit/Register
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register([Bind("VisitorId,EmployeeId,Purpose,VisitDate,CheckInTime")] Visit visit)
    {
        if (ModelState.IsValid)
        {
            visit.Status = "CheckedIn";
            await _visitRepository.AddAsync(visit);
            return RedirectToAction(nameof(Index));
        }
        ViewBag.Visitors = new SelectList(await _visitorRepository.GetAllAsync(), "VisitorId", "VisitorName", visit.VisitorId);
        ViewBag.Employees = new SelectList(await _employeeRepository.GetActiveEmployeesAsync(), "EmployeeId", "EmployeeName", visit.EmployeeId);
        return View(visit);
    }

    // GET: Visit/CheckOut/5
    public async Task<IActionResult> CheckOut(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var visit = await _visitRepository.GetByIdAsync(id.Value);
        if (visit == null)
        {
            return NotFound();
        }

        if (visit.Status == "CheckedOut")
        {
            TempData["Error"] = "This visit has already been checked out.";
            return RedirectToAction(nameof(Index));
        }

        return View(visit);
    }

    // POST: Visit/CheckOut/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CheckOut(int id, DateTime checkOutTime)
    {
        var visit = await _visitRepository.GetByIdAsync(id);
        if (visit == null)
        {
            return NotFound();
        }

        visit.CheckOutTime = checkOutTime;
        visit.Status = "CheckedOut";
        await _visitRepository.UpdateAsync(visit);

        TempData["Success"] = "Visitor checked out successfully.";
        return RedirectToAction(nameof(Index));
    }

    // GET: Visit/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var visit = await _visitRepository.GetByIdAsync(id.Value);
        if (visit == null)
        {
            return NotFound();
        }

        return View(visit);
    }

    // API: Get today's visits
    [HttpGet]
    public async Task<IActionResult> GetTodayVisits()
    {
        var visits = await _visitRepository.GetTodayVisitsAsync();
        return Json(visits);
    }

    // API: Get visitors inside
    [HttpGet]
    public async Task<IActionResult> GetVisitorsInside()
    {
        var visits = await _visitRepository.GetVisitorsInsideAsync();
        return Json(visits);
    }

    // API: Get recent visits
    [HttpGet]
    public async Task<IActionResult> GetRecentVisits(int count = 5)
    {
        var visits = await _visitRepository.GetRecentVisitsAsync(count);
        return Json(visits);
    }
}
