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
        public IActionResult Get()
        {
            // return _quotesDbContext.Quotes;
            return Ok(_quotesDbContext.Quotes);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id){
            var entity = _quotesDbContext.Quotes.Find(id);
            if(entity!=null){
                return Ok(entity);//FIXME:常见的只有Ok和NotFound可以设置返回的消息字符串
            }
            else{
                // return StatusCode(StatusCodes.Status404NotFound);
                return NotFound($"Not found entity with id={id}");
            }
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
            if(value!=null){
                _quotesDbContext.Quotes.Add(value);
                _quotesDbContext.SaveChanges();
                return StatusCode(StatusCodes.Status201Created);
            }
            else{
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
            if(entity!=null&&value!=null){
                entity.SetUp(value);
                _quotesDbContext.SaveChanges();
                return Accepted();
                // return StatusCode(StatusCodes.Status202Accepted);
            }
            else{
                if(entity==null)return NotFound($"Not found entity with id={id}");
                else return Forbid();
            }
            
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var q = _quotesDbContext.Quotes.Find(id);
            if(q!=null){
                _quotesDbContext.Quotes.Remove(q);
                _quotesDbContext.SaveChanges();
                return Ok();
            }
            else{
                return NotFound($"Not found entity with id={id}");
            }
        }
    }
}
