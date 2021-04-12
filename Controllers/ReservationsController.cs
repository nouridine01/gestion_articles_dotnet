﻿using System;
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
using Microsoft.AspNetCore.Identity;
using outils_dotnet.Areas.Identity.Data;

namespace outils_dotnet.Controllers
{
    public class ReservationsController : Controller
    {
        private readonly dbContext _context;
        private readonly UserManager<User> _userManager;

        public ReservationsController(dbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Reservations
        [Authorize(Roles = "ADMIN, VENDEUR")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Reservation.ToListAsync());
        }

        //GET: Reservations/MesReservations
        [Authorize(Roles = "CLIENT")]
        public async Task<IActionResult> MesReservations()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            Client client = _context.Client.Where(c => c.UserId == userId).First();
            return View(await _context.Reservation.Where(a => a.ClientId == client.Id).ToListAsync());
        }

        // GET: Reservations/Details/5
        [Authorize(Roles = "ADMIN, VENDEUR, CLIENT")]
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            var article = _context.Article.Find((long)reservation.ArticleId);
            var client = _context.Client.Find((long)reservation.ClientId);
            var user = await _userManager.FindByIdAsync(client.UserId);

            ViewData["Article"] = article;
            ViewData["User"] = user;

            return View(reservation);
        }

        // GET: Reservations/Create
        [Authorize(Roles = "CLIENT")]
        public async Task<IActionResult> Create(long? id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);
            Client client = _context.Client.Where(c => c.UserId == userId).First();

            var article = _context.Article.Find(id);
            ViewData["User"] = user;
            ViewData["Article"] = article;
            ViewData["Client"] = client;
            return View();
        }

        // POST: Reservations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "CLIENT")]
        public async Task<IActionResult> Create([Bind("Type,Date_recup,Quantity,ArticleId,ClientId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                reservation.Date = DateTime.Now;
                var article = _context.Article.Find((long)reservation.ArticleId);
                article.Quantite -= reservation.Quantity;
                _context.Update(article);
                _context.Add(reservation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(MesReservations));
            }

            return View(reservation);
        }

        /*
        // GET: Reservations/Edit/5
        [Authorize(Roles = "ADMIN, VENDEUR, CLIENT")]
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }
            return View(reservation);
        }

        // POST: Reservations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Type,Date,Date_recup,Quantity,ArticleId,ClientId")] Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reservation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReservationExists(reservation.Id))
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
            return View(reservation);
        }*/

        // GET: Reservations/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reservation = await _context.Reservation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (reservation == null)
            {
                return NotFound();
            }

            var article = _context.Article.Find((long)reservation.ArticleId);
            var client = _context.Client.Find((long)reservation.ClientId);
            var user = await _userManager.FindByIdAsync(client.UserId);

            ViewData["Article"] = article;
            ViewData["User"] = user;

            return View(reservation);
        }

        // POST: Reservations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Reservations/Honorer/5
        public async Task<IActionResult> Honorer(long? id)
        {
            var reservation = await _context.Reservation.FindAsync(id);
            if(reservation.Type == "Achat")
            {

            }
            else if(reservation.Type == "Location")
            {

            }

            _context.Reservation.Remove(reservation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReservationExists(long id)
        {
            return _context.Reservation.Any(e => e.Id == id);
        }
    }
}
