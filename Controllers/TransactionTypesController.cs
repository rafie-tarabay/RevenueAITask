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
    public class TransactionTypesController : Controller
    {
        private readonly RevenueAIContext _context;

        public TransactionTypesController(RevenueAIContext context)
        {
            _context = context;
        }

        // GET: TransactionTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TransactionTypes.ToListAsync());
        }

        // GET: TransactionTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionType = await _context.TransactionTypes
                .FirstOrDefaultAsync(m => m.TransactionTypeId == id);
            if (transactionType == null)
            {
                return NotFound();
            }

            return View(transactionType);
        }

        // GET: TransactionTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TransactionTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TransactionTypeId,TransactionTypeName")] TransactionType transactionType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transactionType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(transactionType);
        }

        // GET: TransactionTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionType = await _context.TransactionTypes.FindAsync(id);
            if (transactionType == null)
            {
                return NotFound();
            }
            return View(transactionType);
        }

        // POST: TransactionTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TransactionTypeId,TransactionTypeName")] TransactionType transactionType)
        {
            if (id != transactionType.TransactionTypeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transactionType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionTypeExists(transactionType.TransactionTypeId))
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
            return View(transactionType);
        }

        // GET: TransactionTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionType = await _context.TransactionTypes
                .FirstOrDefaultAsync(m => m.TransactionTypeId == id);
            if (transactionType == null)
            {
                return NotFound();
            }

            return View(transactionType);
        }

        // POST: TransactionTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionType = await _context.TransactionTypes.FindAsync(id);
            _context.TransactionTypes.Remove(transactionType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionTypeExists(int id)
        {
            return _context.TransactionTypes.Any(e => e.TransactionTypeId == id);
        }
    }
}
