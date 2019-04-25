﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuotesApi.Model
{
    public class Quote
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public void SetUp(Quote quote){
            Title = quote.Title;
            Author = quote.Author;
            Description = quote.Description;
        }
    }
}
