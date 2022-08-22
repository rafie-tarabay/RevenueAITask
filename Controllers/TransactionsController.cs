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
    public class TransactionsController : Controller
    {
        private readonly RevenueAIContext _context;

        public TransactionsController(RevenueAIContext context)
        {
            _context = context;
        }

        public IActionResult Query()
        {
            return View();
        }

        // GET: Transactions
        public async Task<IActionResult> Index(string searchTerm)
        {
            int UserID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var queryable = _context.Transactions.Include(t => t.CardNumberNavigation).Include(t => t.TransactionType).Include(t => t.Vendor)
                .Where(e => e.CardNumberNavigation.UserId == UserID)
                .AsQueryable();


    

            if (!string.IsNullOrEmpty(searchTerm))
            {
                ViewBag.SearchTerm = searchTerm;
                searchTerm = searchTerm.ToLower();

                queryable = queryable.Where(x =>
                    x.CardNumber.Contains( searchTerm) 
                    //|| searchTerm.Contains(x.CardNumber.ToLower())
                );
            }


            var revenueAIContext = await queryable
                .OrderByDescending(e => e.Date)
                .Take(1000)
                .AsSplitQuery()
                .AsNoTracking()
                .ToListAsync();

            return View(revenueAIContext);
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int UserID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var transaction = await _context.Transactions
                .Include(t => t.CardNumberNavigation)
                .Include(t => t.TransactionType)
                .Include(t => t.Vendor)
                .Where(e => e.CardNumberNavigation.UserId == UserID)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            int UserID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

            ViewData["CardNumber"] = new SelectList(_context.Cards.Where(e => e.UserId == UserID), "CardNumber", "CardNumber");
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "TransactionTypeId", "TransactionTypeName");
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "Name");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Date,Amount,TransactionTypeId,CardNumber,VendorId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CardNumber"] = new SelectList(_context.Cards, "CardNumber", "CardNumber", transaction.CardNumber);
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "TransactionTypeId", "TransactionTypeName", transaction.TransactionTypeId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "Name", transaction.VendorId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["CardNumber"] = new SelectList(_context.Cards, "CardNumber", "CardNumber", transaction.CardNumber);
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "TransactionTypeId", "TransactionTypeName", transaction.TransactionTypeId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "Name", transaction.VendorId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Date,Amount,TransactionTypeId,CardNumber,VendorId")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
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
            ViewData["CardNumber"] = new SelectList(_context.Cards, "CardNumber", "CardNumber", transaction.CardNumber);
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "TransactionTypeId", "TransactionTypeName", transaction.TransactionTypeId);
            ViewData["VendorId"] = new SelectList(_context.Vendors, "VendorId", "Name", transaction.VendorId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.CardNumberNavigation)
                .Include(t => t.TransactionType)
                .Include(t => t.Vendor)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
