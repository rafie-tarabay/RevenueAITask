using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RevenueAITask.Data;
using RevenueAITask.Models;

namespace RevenueAITask.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class CardsController : Controller
    {
        private readonly RevenueAIContext _context;

        public CardsController(RevenueAIContext context)
        {
            _context = context;
        }

        // GET: Cards
        public async Task<IActionResult> Index()
        {
            int UserID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var revenueAIContext = _context.Cards.Include(c => c.CardType).Include(c => c.Currency).Include(c => c.State).Where(c => c.UserId==UserID);
            return View(await revenueAIContext.ToListAsync());
        }

        // GET: Cards/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int UserID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var card = await _context.Cards
                .Include(c => c.CardType)
                .Include(c => c.Currency)
                .Include(c => c.State)
                .Where(c => c.UserId == UserID)
                .FirstOrDefaultAsync(m => m.CardNumber == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // GET: Cards/Create
        public IActionResult Create()
        {
            ViewData["CardTypeId"] = new SelectList(_context.CardTypes, "CardTypeId", "CardType1");
            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "CurrencyId", "Currency1");
            ViewData["StateId"] = new SelectList(_context.CardStates, "StateId", "StateDescription");
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CardNumber,Valid,StateId,CardTypeId,CurrencyId,UserId")] Card card)
        {
            int UserID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            card.UserId = UserID;

            if (ModelState.IsValid)
            {
                _context.Add(card);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardTypeId"] = new SelectList(_context.CardTypes, "CardTypeId", "CardType1", card.CardTypeId);
            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "CurrencyId", "Currency1", card.CurrencyId);
            ViewData["StateId"] = new SelectList(_context.CardStates, "StateId", "StateDescription", card.StateId);
            return View(card);
        }

        // GET: Cards/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var card = await _context.Cards.FindAsync(id);
            if (card == null)
            {
                return NotFound();
            }
            ViewData["CardTypeId"] = new SelectList(_context.CardTypes, "CardTypeId", "CardType1", card.CardTypeId);
            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "CurrencyId", "Currency1", card.CurrencyId);
            ViewData["StateId"] = new SelectList(_context.CardStates, "StateId", "StateDescription", card.StateId);
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("CardNumber,Valid,StateId,CardTypeId,CurrencyId,UserId")] Card card)
        {
            if (id != card.CardNumber)
            {
                return NotFound();
            }

            int UserID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            card.UserId = UserID;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(card);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardExists(card.CardNumber))
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
            ViewData["CardTypeId"] = new SelectList(_context.CardTypes, "CardTypeId", "CardType1", card.CardTypeId);
            ViewData["CurrencyId"] = new SelectList(_context.Currencies, "CurrencyId", "Currency1", card.CurrencyId);
            ViewData["StateId"] = new SelectList(_context.CardStates, "StateId", "StateDescription", card.StateId);
            return View(card);
        }

        // GET: Cards/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            int UserID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            var card = await _context.Cards
                .Include(c => c.CardType)
                .Include(c => c.Currency)
                .Include(c => c.State)
                .Where(e=>e.UserId==UserID)
                .FirstOrDefaultAsync(m => m.CardNumber == id);
            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var card = await _context.Cards.FindAsync(id);
            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardExists(string id)
        {
            return _context.Cards.Any(e => e.CardNumber == id);
        }
    }
}
