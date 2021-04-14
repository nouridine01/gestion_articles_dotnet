using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using outils_dotnet.Areas.Identity.Data;
using outils_dotnet.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace outils_dotnet
{
    public class RechercheController : Controller
    {

        private readonly dbContext _context;
        private readonly UserManager<User> _userManager;

        public RechercheController(dbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Recherche
        [Route("/recherche")]
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Recherche()
        {
            List<object> liste = new List<Object>();
            liste.AddRange(await _context.Article.Include(a => a.Categorie).ToListAsync());
            liste.AddRange(await _context.Categorie.ToListAsync());
            liste.AddRange(await _userManager.Users.ToListAsync());

            return View(liste);
        }

        // POST: Categories
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/recherche")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Recherche(string keyword)
        {
            if(keyword == null || keyword == "")
            {
                return RedirectToAction(nameof(Recherche));
            }

            List<object> liste = new List<Object>();
            liste.AddRange(await _context.Article.Include(a => a.Categorie).Where(a => a.Nom.Contains(keyword)).ToListAsync());
            liste.AddRange(await _context.Categorie.Where(c => c.Nom.Contains(keyword)).ToListAsync());
            liste.AddRange(await _userManager.Users.Where(u => u.UserName.Contains(keyword)).ToListAsync());

            return View(liste);
        }

        /*private readonly IHttpClientFactory _clientFactory;
        public RechercheController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [Route("/recherche")]
        [HttpGet]
        public async Task<IActionResult> Recherche(string mc)
        {
            if (mc == null)
            {
                return NotFound();
            }

            List<System.Object> liste = new List<object>();

            var request = new HttpRequestMessage(HttpMethod.Get,
            "https:url/recherche?mc=" + mc);
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStringAsync();
                liste = JsonConvert.DeserializeObject<List<Object>>(responseStream);
            }

            return View(liste);
        }*/
    }
}
