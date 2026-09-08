using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Authorize(Roles = "user")]
public class OrderController : Controller
{
    private readonly MobileContext _context;

    public OrderController(MobileContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        List<Order> orders = _context.Orders.Include(o => o.Phone).ToList();
        return View(orders);
    }
    
    public IActionResult Create(int id)
    {
        Phone? p = _context.Phones.FirstOrDefault(p => p.Id == id);
        if (p == null) return NotFound();
        
        return View(new Order { PhoneId = p.Id, Phone = p });
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Order? order)
    {
        if (order != null && ModelState.IsValid)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        return View(order);
    }
}