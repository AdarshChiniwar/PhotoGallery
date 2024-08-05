using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhotoGallery.Model
{
    public class Category
    {
        [AutoIncrement, PrimaryKey]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string CategoryPoster { get; set; }
        public string ImageUrl { get; set; }
    }
}
