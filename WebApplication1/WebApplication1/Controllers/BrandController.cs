using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers;

[Authorize(Roles = "admin")] 
public class BrandController : Controller
{
    private readonly MobileContext _context;

    public BrandController(MobileContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.Brands.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Brand brand)
    {
        if (await _context.Brands.AnyAsync(b => b.Name.ToLower() == brand.Name.ToLower()))
        {
            ModelState.AddModelError("Name", "Бренд с таким названием уже существует");
        }

        if (ModelState.IsValid)
        {
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(brand);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var brand = await _context.Brands.FindAsync(id);
        if (brand == null) return NotFound();

        return View(brand);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Brand brand)
    {
        if (id != brand.Id) return NotFound();
        
        if (await _context.Brands.AnyAsync(b => b.Name.ToLower() == brand.Name.ToLower() && b.Id != brand.Id))
        {
            ModelState.AddModelError("Name", "Бренд с таким названием уже существует");
        }

        if (ModelState.IsValid)
        {
            _context.Brands.Update(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(brand);
    }
}