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
using Microsoft.AspNetCore.Identity;
using outils_dotnet.Areas.Identity.Data;

namespace outils_dotnet.Controllers
{
    public class AchatsController : Controller
    {
        private readonly dbContext _context;
        private readonly UserManager<User> _userManager;


        public AchatsController(dbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Achats
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Achat.ToListAsync());
        }

        // GET: Achats/Details/5
        [Authorize(Roles = "ADMIN, VENDEUR, CLIENT")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achat = await _context.Achat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (achat == null)
            {
                return NotFound();
            }

            var article = _context.Article.Find((long)achat.ArticleId);
            var client = _context.Client.Find((long)achat.ClientId);
            var user = await _userManager.FindByIdAsync(client.UserId);

            ViewData["Article"] = article;
            ViewData["User"] = user;

            return View(achat);
        }

        // GET: Achats/Create
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> CreateAsync(long? id)
        {
            var listItem = new List<SelectListItem>();
            foreach(Client c in _context.Set<Client>())
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

        // POST: Achats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Create([Bind("Quantite,ArticleId,ClientId")] Achat achat)
        {
            if (ModelState.IsValid)
            {
                achat.Date = DateTime.Now;
                var article = _context.Article.Find((long)achat.ArticleId);
                article.Quantite -= achat.Quantite;
                _context.Update(article);
                _context.Add(achat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(achat);
        }

        /*
        // GET: Achats/Edit/5
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achat = await _context.Achat.FindAsync(id);
            if (achat == null)
            {
                return NotFound();
            }
            return View(achat);
        }

        // POST: Achats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Date,Quantite,ArticleId,ClientId")] Achat achat)
        {
            if (id != achat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(achat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AchatExists(achat.Id))
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
            return View(achat);
        }

        // GET: Achats/Delete/5
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var achat = await _context.Achat
                .FirstOrDefaultAsync(m => m.Id == id);
            if (achat == null)
            {
                return NotFound();
            }

            return View(achat);
        }

        // POST: Achats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var achat = await _context.Achat.FindAsync(id);
            _context.Achat.Remove(achat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }*/

        private bool AchatExists(long id)
        {
            return _context.Achat.Any(e => e.Id == id);
        }
    }
}
