using Microsoft.AspNetCore.Mvc;
using VisitorManagementSystem.Models;
using VisitorManagementSystem.Repositories;

namespace VisitorManagementSystem.Controllers;

public class EmployeeController : Controller
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeController(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    // GET: Employee
    public async Task<IActionResult> Index()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return View(employees);
    }

    // GET: Employee/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await _employeeRepository.GetByIdAsync(id.Value);
        if (employee == null)
        {
            return NotFound();
        }

        return View(employee);
    }

    // GET: Employee/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Employee/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("EmployeeId,EmployeeName,Department,Designation,MobileNo,Email,IsActive")] Employee employee)
    {
        if (ModelState.IsValid)
        {
            await _employeeRepository.AddAsync(employee);
            return RedirectToAction(nameof(Index));
        }
        return View(employee);
    }

    // GET: Employee/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var employee = await _employeeRepository.GetByIdAsync(id.Value);
        if (employee == null)
        {
            return NotFound();
        }
        return View(employee);
    }

    // POST: Employee/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("EmployeeId,EmployeeName,Department,Designation,MobileNo,Email,IsActive")] Employee employee)
    {
        if (id != employee.EmployeeId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                await _employeeRepository.UpdateAsync(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await EmployeeExists(employee.EmployeeId))
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
        return View(employee);
    }

    // POST: Employee/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _employeeRepository.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    // API: Get all employees as JSON
    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _employeeRepository.GetAllAsync();
        return Json(employees);
    }

    // API: Get active employees as JSON
    [HttpGet]
    public async Task<IActionResult> GetActiveEmployees()
    {
        var employees = await _employeeRepository.GetActiveEmployeesAsync();
        return Json(employees);
    }

    private async Task<bool> EmployeeExists(int id)
    {
        var employee = await _employeeRepository.GetByIdAsync(id);
        return employee != null;
    }
}
