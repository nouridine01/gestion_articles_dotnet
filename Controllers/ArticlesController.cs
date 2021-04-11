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

namespace outils_dotnet.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly dbContext _context;

        public ArticlesController(dbContext context)
        {
            _context = context;
        }

        // GET: Articles
        [Authorize(Roles = "ADMIN, VENDEUR, CLIENT")]
        public async Task<IActionResult> Index()
        {
            var dbContext = _context.Article.Include(a => a.Categorie);
            return View(await dbContext.ToListAsync());
        }

        // GET: Articles/Details/5
        [Authorize(Roles = "ADMIN, VENDEUR, CLIENT")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.Categorie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // GET: Articles/Create
        [Authorize(Roles = "ADMIN")]
        public IActionResult Create()
        {
            ViewData["CategorieId"] = new SelectList(_context.Set<Categorie>(), "Id", "Nom");
            return View();
        }

        // POST: Articles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Create([Bind("Id,Nom,Prix,Quantite,Louable,Achetable")] Article article, long Categorie)
        {
            if (ModelState.IsValid)
            {
                article.Categorie = _context.Categorie.Find(Categorie);
                _context.Add(article);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(article);
        }

        // GET: Articles/Edit/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var article = await _context.Article.Include(a => a.Categorie).FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            var listCategorie = new SelectList(_context.Set<Categorie>(), "Id", "Nom", article.Categorie.Id);
            ViewData["CategorieId"] = listCategorie;
            Console.WriteLine(article.Categorie.Id);
            return View(article);
        }

        // POST: Articles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Nom,Prix,Quantite,Louable,Achetable")] Article article, long Categorie)
        {
            if (id != article.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    article.Categorie = _context.Categorie.Find(Categorie);
                    _context.Update(article);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArticleExists(article.Id))
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
            return View(article);
        }

        // GET: Articles/Delete/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var article = await _context.Article
                .Include(a => a.Categorie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            return View(article);
        }

        // POST: Articles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var article = await _context.Article.FindAsync(id);
            _context.Article.Remove(article);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArticleExists(long id)
        {
            return _context.Article.Any(e => e.Id == id);
        }
    }
}
