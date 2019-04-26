using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesApi.Model
{
    public class Quote
    {
        public int Id { get; set; }
        [Required]//标记不会影响数据库里面的数据，只是在post构造的时候会检验
        [StringLength(30)]
        public string Title { get; set; }
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Author { get; set; }
        [Required]
        [StringLength(100)]
        public string Description { get; set; }
        [Required]
        [StringLength(30)]
        public string Type { get; set; }
        // public string City { get; set; }

        public DateTime CreatedAt { get; set; }

        // public string Country { get; set; }
        public void SetUp(Quote quote)
        {
            Title = quote.Title;
            Author = quote.Author;
            Description = quote.Description;
            Type = quote.Type;
            CreatedAt = quote.CreatedAt;
            // City = quote.City;
            // this.Country = Country;
        }
    }
}
