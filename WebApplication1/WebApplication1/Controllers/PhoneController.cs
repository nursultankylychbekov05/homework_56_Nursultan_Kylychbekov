using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Authorize]
public class PhoneController : Controller
{
    private readonly MobileContext _context;
    private readonly IWebHostEnvironment _env;

    public PhoneController(MobileContext context, IWebHostEnvironment env)
    {
        _context = context;
        _env = env;
    }

    [AllowAnonymous]
    public IActionResult Index()
    {
        List<Phone> phones = _context.Phones.ToList();
        return View(phones);
    }
    
    [Authorize(Roles = "admin")]
    public IActionResult Create()
    {
        PopulateBrandsDropDownList();
        return View();
    }
    
    [HttpPost]
    [Authorize(Roles = "admin")]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Phone phone)
    {
        if (ModelState.IsValid)
        {
            _context.Phones.Add(phone);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        PopulateBrandsDropDownList(phone.Company);
        return View(phone);
    }
    
    [Authorize(Roles = "admin")]
    public IActionResult Edit(int? id)
    {
        if (!id.HasValue) return NotFound();

        Phone? phone = _context.Phones.FirstOrDefault(p => p.Id == id);
        if (phone == null) return NotFound();

        PopulateBrandsDropDownList(phone.Company);
        return View(phone);
    }
    
    [HttpPost]
    [Authorize(Roles = "admin")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(Phone phone)
    {
        if (ModelState.IsValid)
        {
            _context.Phones.Update(phone);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        PopulateBrandsDropDownList(phone.Company);
        return View(phone);
    }
    
    [AllowAnonymous]
    public IActionResult Details(int? id)
    {
        if (!id.HasValue) return NotFound();

        Phone? phone = _context.Phones
            .Include(p => p.Reviews)
            .FirstOrDefault(p => p.Id == id);

        if (phone == null) return NotFound();

        LoadCurrenciesToViewBag();
        return View(phone);
    }
    
    [HttpPost]
    [Authorize(Roles = "user, admin")] 
    [ValidateAntiForgeryToken]
    public IActionResult AddReview(Review review)
    {
        if (ModelState.IsValid)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return RedirectToAction(nameof(Details), new { id = review.PhoneId });
        }

        var phone = _context.Phones
            .Include(p => p.Reviews)
            .FirstOrDefault(p => p.Id == review.PhoneId);

        if (phone == null) return NotFound();

        ViewBag.ReviewError = true;
        ViewBag.NewReview = review;
        LoadCurrenciesToViewBag(); 

        return View("Details", phone);
    }

    private void PopulateBrandsDropDownList(object? selectedBrand = null)
    {
        var brands = _context.Brands.Select(b => b.Name).ToList();
        ViewBag.Brands = new SelectList(brands, selectedBrand);
    }

    private void LoadCurrenciesToViewBag()
    {
        string filePath = Path.Combine(_env.WebRootPath, "currencies.json");
        List<CurrencyRate> currencies = new();

        if (System.IO.File.Exists(filePath))
        {
            string jsonString = System.IO.File.ReadAllText(filePath);
            currencies = JsonSerializer.Deserialize<List<CurrencyRate>>(jsonString) ?? new();
        }

        ViewBag.Currencies = currencies;
    }
    
    [Authorize(Roles = "admin")]
    public IActionResult Delete(int? id)
    {
        if (!id.HasValue) return NotFound();

        Phone? phone = _context.Phones.FirstOrDefault(p => p.Id == id);
        if (phone == null) return NotFound();

        return View(phone);
    }

    [HttpPost, ActionName("Delete")]
    [Authorize(Roles = "admin")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        Phone? phone = _context.Phones.FirstOrDefault(p => p.Id == id);
        if (phone != null)
        {
            _context.Phones.Remove(phone);
            _context.SaveChanges();
        }

        return RedirectToAction(nameof(Index));
    }
}