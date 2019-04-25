using Microsoft.EntityFrameworkCore;
using QuotesApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesApi.DAO
{
    public class DataContext:DbContext
    {
        //通过services.AddDbContext注入就需要添加这个构造器
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {

        }
        //数据库中返回的结果
        public DbSet<Quote> Quotes { get; set; }
    }
}
