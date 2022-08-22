using Microsoft.AspNetCore.Mvc;
using RevenueAITask.Data;
using RevenueAITask.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RevenueAITask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {

        private readonly RevenueAIContext _context;

        public SearchController(RevenueAIContext context)
        {
            _context = context;
        }

        // GET: api/<SearchController>
        [HttpGet]
        public ActionResult<IEnumerable<Transaction>> Get()
        {
            return _context.Transactions.ToList();
        }

        // GET api/<SearchController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SearchController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

    }
}
