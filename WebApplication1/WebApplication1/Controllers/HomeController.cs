using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.ViewModels;

namespace WebApplication1.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MobileContext _context;

    public HomeController(ILogger<HomeController> logger, MobileContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        ViewData["message"] = "Hello World";
        ViewBag.Message = "Hello World";
        List<Order> orders = _context.Orders.ToList();
        List<Phone> phones = _context.Phones.ToList();
        PhonesWithOrders pwo = new PhonesWithOrders()
        {
            Orders = orders,
            Phones = phones
        };
        return View(pwo);
    }

    public IActionResult GetMessage()
    {
        return PartialView("_GetMessage");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
