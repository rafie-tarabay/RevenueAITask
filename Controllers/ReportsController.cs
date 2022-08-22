using Microsoft.AspNetCore.Mvc;
using RevenueAITask.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RevenueAITask.Controllers
{
    public class ReportsController : Controller
    {
        private readonly RevenueAIContext _context;

        public ReportsController(RevenueAIContext context)
        {
            _context = context;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> AllCardsReport()
        {
            var report = await _context.SQL2DataTable($@"
                        SELECT Card.CardNumber, currency.Currency, CardState.StateDescription
                        FROM CardState INNER JOIN
                         Card ON CardState.StateID = Card.StateID INNER JOIN
                         currency ON Card.currencyID = currency.CurrencyID
                            "
            , null);

            ViewBag.message = "All Cards Report";

            return View("Reports", report);
        }

    }
}
