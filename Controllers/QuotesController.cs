using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuotesApi.DAO;
using QuotesApi.Model;

namespace QuotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private DataContext _quotesDbContext;

        public QuotesController(DataContext dataContext)//FIXME:什么时候调用的这个构造器？依赖注入。因为只有这一个构造器？
        {
            _quotesDbContext = dataContext;
        }

        // GET: api/Quotes
        [HttpGet]
        public IEnumerable<Quote> Get()
        {
            return _quotesDbContext.Quotes;
        }
        [HttpGet("{id}")]
        public Quote Get(int id){
            return _quotesDbContext.Quotes.Find(id);
        }

        // GET: api/Quotes/5
        // [HttpGet("{id}", Name = "Get")]
        // public string Get(int id)
        // {
        //     return "value";
        // }

        // POST: api/Quotes
        [HttpPost]
        public void Post([FromBody] Quote value)
        {
            _quotesDbContext.Quotes.Add(value);
        }

        // PUT: api/Quotes/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Quote value)
        {
            // _quotesDbContext.Quotes.
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var q = _quotesDbContext.Quotes.Find(id);
            if(q!=null){
                _quotesDbContext.Quotes.Remove(q);
            }
        }
    }
}
