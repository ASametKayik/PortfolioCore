using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PortfolioCore.Models;
using PortfolioCore.Services;

namespace PortfolioCore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPortfolioService _portfolioService;

    public HomeController(ILogger<HomeController> logger, IPortfolioService portfolioService)
    {
        _logger = logger;
        _portfolioService = portfolioService;
    }

    public IActionResult Index()
    {
        var model = _portfolioService.GetHomeData();
        return View(model);
    }

    [HttpGet("project/{id}")]
    public IActionResult ProjectDetail(int id)
    {
        var project = _portfolioService.GetProjectById(id);
        if (project == null)
        {
            return NotFound();
        }
        return View(project);
    }

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public IActionResult SubmitContact([FromBody] ContactFormModel model)
    {
        if (model == null || !ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            string errorMessage = errors.Any() ? string.Join(" ", errors) : "Lütfen tüm alanları geçerli şekilde doldurunuz.";
            return Json(new { success = false, message = errorMessage });
        }

        var result = _portfolioService.ProcessContactMessage(model);
        return Json(new { success = result.Success, message = result.Message });
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
