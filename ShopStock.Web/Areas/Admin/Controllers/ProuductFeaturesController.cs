
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopStock.Domain.Models.Prouducts;
using ShopStock.Infra.Data.Context;

[Area("Admin")]
public class ProuductFeaturesController : Controller
{
    private readonly EshopDbContext _context;

    public ProuductFeaturesController(EshopDbContext context)
    {
        _context = context;
    }

    // GET: ProuductFeatures
    public async Task<IActionResult> Index(int? id)    
    {
        if (id == null)
        {

            return BadRequest();
        }
        ViewBag.feature = "ویژگی های" + " "+_context.Prouducts.Find(id).Tittle;
        ViewBag.Id = id;
        return View(await _context.ProuductFeatures.Where(i=>i.ProuductId==id&&i.IsDelete==false).ToListAsync());
    }

    // GET: ProuductFeatures/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ProuductFeatures = await _context.ProuductFeatures
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ProuductFeatures == null)
        {
            return NotFound();
        }

        return View(ProuductFeatures);
    }

    // GET: ProuductFeatures/Create
    public IActionResult Create(int id)
    {
        var model = new ProuductFeature
        {
            ProuductId = id
        };

        return View(model);


      
    }

    // POST: ProuductFeatures/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ProuductId,Value,Name")] ProuductFeature ProuductFeatures)
    {
        if (ModelState.IsValid)
        {
            ProuductFeatures.CreateDate = DateTime.Now;
            ProuductFeatures.IsDelete=false;
            _context.Add(ProuductFeatures);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index),new {id=ProuductFeatures.ProuductId});
        }
        return View(ProuductFeatures);
    }

    // GET: ProuductFeatures/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ProuductFeatures = await _context.ProuductFeatures.FindAsync(id);
        if (ProuductFeatures == null)
        {
            return NotFound();
        }
        return View(ProuductFeatures);
    }

    // POST: ProuductFeatures/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("ProuductId,Value,Name,Id,CreateDate,UpdateDate,DeleteDate,IsDelete")] ProuductFeature ProuductFeatures)
    {
        if (id != ProuductFeatures.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {

            ProuductFeatures.UpdateDate=DateTime.Now;

            try
            {
                _context.Update(ProuductFeatures);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProuductFeaturesExists(ProuductFeatures.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index),new {id=ProuductFeatures.ProuductId});
        }
        return View(ProuductFeatures);
    }

    // GET: ProuductFeatures/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var ProuductFeatures = await _context.ProuductFeatures
            .FirstOrDefaultAsync(m => m.Id == id);
        if (ProuductFeatures == null)
        {
            return NotFound();
        }

        return View(ProuductFeatures);
    }

    // POST: ProuductFeatures/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var ProuductFeatures = await _context.ProuductFeatures.FindAsync(id);
        if (ProuductFeatures != null)
        {
            ProuductFeatures.IsDelete = true;
            _context.ProuductFeatures.Update(ProuductFeatures);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index),new {id=ProuductFeatures.ProuductId});
    }

    private bool ProuductFeaturesExists(int? id)
    {
        return _context.ProuductFeatures.Any(e => e.Id == id);
    }
}
