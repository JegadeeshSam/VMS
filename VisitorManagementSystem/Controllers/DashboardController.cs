using Microsoft.AspNetCore.Mvc;
using VisitorManagementSystem.Repositories;

namespace VisitorManagementSystem.Controllers;

public class DashboardController : Controller
{
    private readonly IVisitRepository _visitRepository;

    public DashboardController(IVisitRepository visitRepository)
    {
        _visitRepository = visitRepository;
    }

    public async Task<IActionResult> Index()
    {
        var todayVisits = await _visitRepository.GetTodayVisitsAsync();
        var visitorsInside = await _visitRepository.GetVisitorsInsideAsync();
        var recentVisits = await _visitRepository.GetRecentVisitsAsync(10);

        ViewBag.TotalVisitorsToday = todayVisits.Count();
        ViewBag.VisitorsInside = visitorsInside.Count();
        ViewBag.CheckedOutVisitors = todayVisits.Count(v => v.Status == "CheckedOut");
        ViewBag.RecentVisits = recentVisits;

        return View();
    }
}
