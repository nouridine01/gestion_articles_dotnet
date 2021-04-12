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
            var dbContext = _userManager.Users;
            return View(await dbContext.ToListAsync());
        }

        // GET: Clients/Details/5
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if(await _userManager.IsInRoleAsync(user, "admin"))
            {
                ViewData["Role"] = "Admin";
            }
            else if(await _userManager.IsInRoleAsync(user, "vendeur"))
            {
                ViewData["Role"] = "Vendeur";
            }
            else if(await _userManager.IsInRoleAsync(user, "client"))
            {
                ViewData["Role"] = "Client";
            }

            return View(user);
        }

        // GET: Clients/Create
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
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
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
           
            if (user == null)
            {
                return NotFound();
            }

            if (await _userManager.IsInRoleAsync(user, "admin"))
                ViewData["Admin"] = true;
            else
                ViewData["Admin"] = false;

            if (await _userManager.IsInRoleAsync(user, "vendeur"))
                ViewData["Vendeur"] = true;
            else
                ViewData["Vendeur"] = false;

            if (await _userManager.IsInRoleAsync(user, "client"))
                ViewData["Client"] = true;
            else
                ViewData["Client"] = false;

            return View(user);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Edit(string id, [Bind("Id,UserName,Nom,Prenom")] User user, string[] roles = null)
        {
            roles ??= new string[0];

            if (await _userManager.IsInRoleAsync(user, "admin"))
                ViewData["Admin"] = true;
            else
                ViewData["Admin"] = false;

            if (await _userManager.IsInRoleAsync(user, "vendeur"))
                ViewData["Vendeur"] = true;
            else
                ViewData["Vendeur"] = false;

            if (await _userManager.IsInRoleAsync(user, "client"))
                ViewData["Client"] = true;
            else
                ViewData["Client"] = false;

            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var currentUser = await _userManager.FindByIdAsync(id);

                currentUser.UserName = user.UserName;
                currentUser.Nom = user.Nom;
                currentUser.Prenom = user.Prenom;

                if (roles.Length == 0 && !await _userManager.IsInRoleAsync(currentUser, "client"))
                {
                    ViewData["Suppression"] = "Veuillez sélectionner au moins un rôle.";
                    return View(user);
                }

                if (!await _userManager.IsInRoleAsync(currentUser, "vendeur"))
                {
                    if (roles.Contains("vendeur"))
                    {
                        await _userManager.AddToRoleAsync(currentUser, "vendeur");
                    }
                }
                else if (!roles.Contains("vendeur"))
                {
                    await _userManager.RemoveFromRoleAsync(currentUser, "vendeur");
                }

                if (!await _userManager.IsInRoleAsync(currentUser, "admin"))
                {
                    if (roles.Contains("admin"))
                    {
                        await _userManager.AddToRoleAsync(currentUser, "admin");
                    }
                }
                else if (!roles.Contains("admin"))
                {
                    await _userManager.RemoveFromRoleAsync(currentUser, "admin");
                }

                await _userManager.UpdateAsync(currentUser);
                
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Clients/Delete/5
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                ViewData["Role"] = "Admin";
            }
            else if (await _userManager.IsInRoleAsync(user, "vendeur"))
            {
                ViewData["Role"] = "Vendeur";
            }
            else if (await _userManager.IsInRoleAsync(user, "client"))
            {
                ViewData["Role"] = "Client";
            }

            return View(user);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (await _userManager.IsInRoleAsync(user, "admin"))
            {
                ViewData["Role"] = "Admin";
            }
            else if (await _userManager.IsInRoleAsync(user, "vendeur"))
            {
                ViewData["Role"] = "Vendeur";
            }
            else if (await _userManager.IsInRoleAsync(user, "client"))
            {
                ViewData["Role"] = "Client";
            }

            if (await _userManager.IsInRoleAsync(user, "client"))
            {
                var clients = await _context.Client.Where(c => c.UserId == id).ToListAsync();
                var client = clients.First();

                var isEmptyReservation = await _context.Reservation.FirstOrDefaultAsync(r => r.ClientId == client.Id);
                var isEmptyLocation = await _context.Location.FirstOrDefaultAsync(l => l.ClientId == client.Id);
                var isEmptyAchat = await _context.Achat.FirstOrDefaultAsync(a => a.ClientId == client.Id);

                if (isEmptyReservation == null && isEmptyLocation == null && isEmptyAchat == null)
                {
                    _context.Client.Remove(client);
                    await _userManager.DeleteAsync(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewData["Suppression"] = "Vous ne pouvez pas supprimer ce client car il a effectué des transactions.";
                    return View(user);
                }
            }

            if ((await _userManager.IsInRoleAsync(user, "admin") || await _userManager.IsInRoleAsync(user, "Vendeur")))
            {
                await _userManager.DeleteAsync(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        private bool ClientExists(long id)
        {
            return _context.Client.Any(e => e.Id == id);
        }
    }
}
