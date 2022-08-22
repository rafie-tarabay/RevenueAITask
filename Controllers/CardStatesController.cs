using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RevenueAITask.Data;
using RevenueAITask.Models;

namespace RevenueAITask.Controllers
{
    [Microsoft.AspNetCore.Authorization.Authorize]
    public class CardStatesController : Controller
    {
        private readonly RevenueAIContext _context;

        public CardStatesController(RevenueAIContext context)
        {
            _context = context;
        }

        // GET: CardStates
        public async Task<IActionResult> Index()
        {
            return View(await _context.CardStates.ToListAsync());
        }

        // GET: CardStates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardState = await _context.CardStates
                .FirstOrDefaultAsync(m => m.StateId == id);
            if (cardState == null)
            {
                return NotFound();
            }

            return View(cardState);
        }

        // GET: CardStates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CardStates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StateId,StateDescription")] CardState cardState)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cardState);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cardState);
        }

        // GET: CardStates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardState = await _context.CardStates.FindAsync(id);
            if (cardState == null)
            {
                return NotFound();
            }
            return View(cardState);
        }

        // POST: CardStates/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StateId,StateDescription")] CardState cardState)
        {
            if (id != cardState.StateId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardState);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardStateExists(cardState.StateId))
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
            return View(cardState);
        }

        // GET: CardStates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardState = await _context.CardStates
                .FirstOrDefaultAsync(m => m.StateId == id);
            if (cardState == null)
            {
                return NotFound();
            }

            return View(cardState);
        }

        // POST: CardStates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cardState = await _context.CardStates.FindAsync(id);
            _context.CardStates.Remove(cardState);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardStateExists(int id)
        {
            return _context.CardStates.Any(e => e.StateId == id);
        }
    }
}
