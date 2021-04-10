using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using outils_dotnet.Areas.Identity.Data;
using outils_dotnet.Data;
using outils_dotnet.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using outils_dotnet.Areas.Identity.Pages.Account;

namespace outils_dotnet.Controllers
{
    public class ClientsController : Controller
    {
        private readonly dbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;

        public ClientsController(dbContext context, UserManager<User> userManager, ILogger<RegisterModel> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        // GET: Clients
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Index()
        {
            var dbContext = _context.Client.Include(c => c.User);
            return View(await dbContext.ToListAsync());
        }

        // GET: Clients/Details/5
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // GET: Clients/Create
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Create([Bind("Email,Nom,Prenom")] User user, string password, string[] roles = null)
        {
            roles ??= new string[0];
            if (ModelState.IsValid)
            {
                user.UserName = user.Email;
                var result = await _userManager.CreateAsync(user, password);
                if(result.Succeeded)
                {
               
                    _logger.LogInformation("User created a new account with password.");

                    user = await _userManager.FindByNameAsync(user.UserName);

                    if(roles.Length == 0)
                    {
                        await _userManager.AddToRoleAsync(user, "CLIENT");
                        Client client = new Client();
                        _context.Add(client);
                        await _context.SaveChangesAsync();
                        client.User = user;
                        _context.Update(client);
                        await _context.SaveChangesAsync();
                    }
                    else foreach(String str in roles)
                    {
                        switch(str)
                        {
                            case ("admin"):
                                await _userManager.AddToRoleAsync(user, "ADMIN");
                                break;
                            case ("vendeur"):
                                await _userManager.AddToRoleAsync(user, "VENDEUR");
                                break;
                            case ("client"):
                                await _userManager.AddToRoleAsync(user, "CLIENT");
                                Client client = new Client();
                                _context.Add(client);
                                await _context.SaveChangesAsync();
                                client.User = user;
                                _context.Update(client);
                                await _context.SaveChangesAsync();
                                break;
                        }
                    }

                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View(user);
        }

        // GET: Clients/Edit/5
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", client.UserId);
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Edit(long id, [Bind("Id,UserId")] Client client)
        {
            if (id != client.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(client);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClientExists(client.Id))
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
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", client.UserId);
            return View(client);
        }

        // GET: Clients/Delete/5
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var client = await _context.Client
                .Include(c => c.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (client == null)
            {
                return NotFound();
            }

            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var client = await _context.Client.FindAsync(id);
            _context.Client.Remove(client);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClientExists(long id)
        {
            return _context.Client.Any(e => e.Id == id);
        }
    }
}
