using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QuotesApi.DAO;
using QuotesApi.Model;

namespace QuotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuotesController : ControllerBase
    {
        private DataContext _quotesDbContext;
        private readonly ILogger<QuotesController> _logger;
        public QuotesController(DataContext dataContext, ILogger<QuotesController> logger)//FIXME:什么时候调用的这个构造器？依赖注入。因为只有这一个构造器？
        {
            _logger = logger;
            _quotesDbContext = dataContext;
        }
        [HttpGet("[action]")]
        public IActionResult page(int? pageNum, int? pageSize)
        {
            int num = pageNum ?? 1;
            int size = pageSize ?? 3;
            IQueryable rs = _quotesDbContext.Quotes.Skip((num - 1) * size).Take(size);
            return Ok(rs);
        }

        // GET: api/Quotes
        // 返回的List经过asp.net pipeline转换成浏览器accept的格式发送出去，默认是json
        // [HttpGet]
        // public IActionResult Get()
        // {
        //     // return _quotesDbContext.Quotes;
        //     return Ok(_quotesDbContext.Quotes);
        // }
        [HttpGet("[action]")]
        public IActionResult sort(string sort)//?sort=asc，如果没有会是一个空字符串，不是null
        {
            _logger.LogInformation($"sort={sort}");
            IQueryable<Quote> rs;
            switch (sort)
            {
                case "asc":
                    rs = _quotesDbContext.Quotes.OrderBy(e => e.CreatedAt);
                    break;
                case "desc":
                    rs = _quotesDbContext.Quotes.OrderByDescending(e => e.CreatedAt);
                    break;
                default:
                    rs = _quotesDbContext.Quotes;
                    break;
            }
            return Ok(rs);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var entity = _quotesDbContext.Quotes.Find(id);
            if (entity != null)
            {
                return Ok(entity);//FIXME:常见的只有Ok和NotFound可以设置返回的消息字符串
            }
            else
            {
                // return StatusCode(StatusCodes.Status404NotFound);
                return NotFound($"Not found entity with id={id}");
            }
        }
        [HttpGet("[action]/{param}")]
        public int Test(int param)
        {
            return param;
        }

        // GET: api/Quotes/5
        // [HttpGet("{id}", Name = "Get")]
        // public string Get(int id)
        // {
        //     return "value";
        // }

        // POST: api/Quotes
        [HttpPost]
        public IActionResult Post([FromBody] Quote value)//Id是autoincrement的，不能手动设置
        {
            if (value != null)
            {
                _quotesDbContext.Quotes.Add(value);
                _quotesDbContext.SaveChanges();
                // return Forbid();
                return StatusCode(StatusCodes.Status201Created);
            }
            else
            {
                // return StatusCode(StatusCodes.Status403Forbidden);
                return Forbid();
            }


        }

        // PUT: api/Quotes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote value)
        {
            // _quotesDbContext.Quotes.
            var entity = _quotesDbContext.Quotes.Find(id);
            if (entity != null && value != null)
            {
                entity.SetUp(value);
                _quotesDbContext.SaveChanges();
                return Accepted();
                // return StatusCode(StatusCodes.Status202Accepted);
            }
            else
            {
                if (entity == null) return NotFound($"Not found entity with id={id}");
                else return Forbid();
            }

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var q = _quotesDbContext.Quotes.Find(id);
            if (q != null)
            {
                _quotesDbContext.Quotes.Remove(q);
                _quotesDbContext.SaveChanges();
                return Ok();
            }
            else
            {
                return NotFound($"Not found entity with id={id}");
            }
        }
    }
}
