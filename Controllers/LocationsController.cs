using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using outils_dotnet.Data;
using outils_dotnet.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using outils_dotnet.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

namespace outils_dotnet.Controllers
{
    public class LocationsController : Controller
    {
        private readonly dbContext _context;
        private readonly UserManager<User> _userManager;

        public LocationsController(dbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Locations
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Location.ToListAsync());
        }

        //GET: Locations/MesLocations
        [Authorize(Roles = "CLIENT")]
        public async Task<IActionResult> MesLocations()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Client client = _context.Client.Where(c => c.UserId == userId).First();
            return View(await _context.Location.Where(a => a.ClientId == client.Id).ToListAsync());
        }

        // GET: Locations/Details/5
        [Authorize(Roles = "ADMIN, VENDEUR, CLIENT")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Location
                .FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            var article = _context.Article.Find((long)location.ArticleId);
            var client = _context.Client.Find((long)location.ClientId);
            var user = await _userManager.FindByIdAsync(client.UserId);

            ViewData["Article"] = article;
            ViewData["User"] = user;

            return View(location);
        }

        // GET: Locations/Create
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> CreateAsync(long? id)
        {
            var listItem = new List<SelectListItem>();
            foreach (Client c in _context.Set<Client>())
            {
                var user = await _userManager.FindByIdAsync(c.UserId);
                var name = user.Nom;
                listItem.Add(new SelectListItem { Text = name.ToString(), Value = c.Id.ToString() });
            }
            ViewData["ClientId"] = new SelectList(listItem, "Value", "Text", 1);

            var article = _context.Article.Find(id);
            ViewData["Article"] = article;
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Create([Bind("Date_retour,Quantite,ArticleId,ClientId")] Location location)
        {
            if (ModelState.IsValid)
            {
                location.Date = DateTime.Now;
                var article = _context.Article.Find((long)location.ArticleId);
                article.Quantite -= location.Quantite;
                _context.Update(article);
                _context.Add(location);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(location);
        }

        // GET: Locations/Edit/5
        /*[Authorize(Roles = "CLIENT")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Location.FindAsync(id);
            if (location == null)
            {
                return NotFound();
            }
            return View(location);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CLIENT")]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Date,Date_retour,Quantite,ArticleId,ClientId")] Location location)
        {
            if (id != location.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(location);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationExists(location.Id))
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
            return View(location);
        }

        /*
        // GET: Locations/Delete/5
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var location = await _context.Location
                .FirstOrDefaultAsync(m => m.Id == id);
            if (location == null)
            {
                return NotFound();
            }

            return View(location);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var location = await _context.Location.FindAsync(id);
            _context.Location.Remove(location);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        private bool LocationExists(long id)
        {
            return _context.Location.Any(e => e.Id == id);
        }
    }
}
