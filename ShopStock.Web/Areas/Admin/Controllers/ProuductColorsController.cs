
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopStock.Domain.Models.Prouducts;
using ShopStock.Infra.Data.Context;
using System.Configuration;

[Area("Admin")]
public class ProuductColorsController : Controller
{
    private readonly EshopDbContext _context;

    public ProuductColorsController(EshopDbContext context)
    {
        _context = context;
    }

    // GET: prouductColorsS
    public async Task<IActionResult> Index(int? id)   
    {
        if(id == null)
        {

            return BadRequest();
        }
        ViewBag.Id = id;    
       var color= await _context.prouductColors.Where(i=>i.ProuductId == id&&i.IsDelete==false).ToListAsync();

        return View(color);
    }

    // GET: prouductColorsS/Details/5


    // GET: prouductColorsS/Create
    public IActionResult Create(int id)
    {
        ViewBag.id=id;

        return View();
    }

    // POST: prouductColorsS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(int id, [Bind("Name,Code,Price,Quantity,IsDefult")] ProuductColor prouductColors)
    {

      prouductColors.CreateDate = DateTime.Now;
            prouductColors.IsDelete = false;
          prouductColors.ProuductId = id;
        if (_context.prouductColors.Where(i => i.ProuductId == prouductColors.ProuductId).Count() == 0) { 
        
        prouductColors.IsDefult = true; 
        
        }
           

        if (ModelState.IsValid)
        {
           
            _context.prouductColors.Add(prouductColors);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index), new {id=prouductColors.ProuductId});
        }
        return View(prouductColors);
    }

    // GET: prouductColorsS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prouductColors = await _context.prouductColors.FindAsync(id);
        if (prouductColors == null)
        {
            return NotFound();
        }
        return View(prouductColors);
    }

    // POST: prouductColorsS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("ProuductId,Name,Id,Code,Price,Quantity,IsDefult,IsDelete")] ProuductColor prouductColors)
    {
        if (id != prouductColors.Id)
        {
            return NotFound();
        }
        prouductColors.UpdateDate = DateTime.Now;
     

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(prouductColors);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!prouductColorsExists(prouductColors.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index), new {id=prouductColors.ProuductId});
        }
        return View(prouductColors);
    }

    // GET: prouductColorsS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var prouductColors = await _context.prouductColors
            .FirstOrDefaultAsync(m => m.Id == id);
        if (prouductColors == null)
        {
            return NotFound();
        }

        return View(prouductColors);
    }

    // POST: prouductColorsS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var prouductColors = await _context.prouductColors.FindAsync(id);
        if (prouductColors != null)
        {
            prouductColors.IsDelete=true;
        }
         _context.prouductColors.Update(prouductColors);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index), new {id=prouductColors.ProuductId});
    }

    public IActionResult IsDefult(int id) {

        var color =  _context.prouductColors.Find(id);
     

        foreach(var item in _context.prouductColors.Where(i=>i.ProuductId==color.ProuductId).ToList())
        {
            item.IsDefult = false;

        }
  color.IsDefult=true;
        _context.SaveChanges();


        return RedirectToAction("Index",new {id=color.ProuductId});
    }

    private bool prouductColorsExists(int? id)
    {
        return _context.prouductColors.Any(e => e.Id == id);
    }
}
