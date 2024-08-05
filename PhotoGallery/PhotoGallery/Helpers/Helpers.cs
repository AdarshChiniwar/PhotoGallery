using PhotoGallery.Interfaces;
using PhotoGallery.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PhotoGallery.Helpers
{
    public class HelpersClass
    {
        #region Global Variables
        private SQLiteConnection sqliteConnection;
        public List<Category> Categories { get; set; }
        public List<PhotoItem> PhotoItems { get; set; }
        #endregion
        public HelpersClass()
        {
            sqliteConnection = DependencyService.Get<Isqlite>().GetConnection();
        }

        public void AddCategories()
        {
            Categories = new List<Category>()
            {
                new Category(){
                 
                    CategoryName = "Impressionism",
                    CategoryPoster = "Impressionismposter.png",
                    ImageUrl = "Impressionismimage.png"
                },
                new Category(){
                
                    CategoryName = "Abstract art",
                    CategoryPoster = "abstractposter.png",
                    ImageUrl = "abstractimage.png"
                },
                new Category(){
                  
                    CategoryName = "Pop art",
                    CategoryPoster = "popposter.png",
                    ImageUrl = "popimage.png"
                },
                new Category(){
                  
                    CategoryName = "Realism",
                    CategoryPoster = "realismposter.jpg",
                    ImageUrl = "realismimage.jpeg"
                },
                new Category(){
              
                    CategoryName = "Water Color",
                    CategoryPoster = "waterposter.png",
                    ImageUrl = "waterimage.png"
                },
                new Category(){
                 
                    CategoryName = "Painterly",
                    CategoryPoster = "Painterlyposter.png",
                    ImageUrl = "painterlyimage.png"
                }
            };

            foreach (var item in Categories)
            {
                int result = sqliteConnection.Insert(item);
            }
        }

        public void AddPhotoItems()
        {
            PhotoItems = new List<PhotoItem>()
            {
                new PhotoItem
                {
                    ProductID = 1,
                    CategoryID = 1,
                    ImageUrl = "https://www.jacksonsart.com/blog/wp-content/uploads/2015/04/claude-monet-impression-sunrise.jpg",
                    Name = "Jackson's Art",
                    Description = "Impressionism - Jackson's Art",
                    Rating = " 4.8",
                    RatingDetail = " (121 raitings)",
                    HomeSelected = "CompleteHeart",
                    Price = 45
                },
                new PhotoItem
                {
                    ProductID = 2,
                    CategoryID = 1,
                    ImageUrl = "https://media.nga.gov/iiif/99758d9d-c10b-4d02-a198-7e49afb1f3a6/full/!740,560/0/default.jpg",
                    Name = "Painting of women and child",
                    Description = "Impressionism - Painting",
                    Rating = " 4.8",
                    RatingDetail = " (121 raitings)",
                    HomeSelected = "EmptyHeart",
                    Price = 45
                },
                new PhotoItem
                {
                    ProductID = 3,
                    CategoryID = 4,
                    ImageUrl = "https://study.com/cimages/multimages/16/512px-jules_bastien-lepage_-_october_-_google_art_project7519628057672595799.jpg",
                    Name = "Women working in farm",
                    Description = "Realism - Painting",
                    Rating = " 4.8",
                    RatingDetail = " (100 raitings)",
                    HomeSelected = "CompleteHeart",
                    Price = 45
                },
                new PhotoItem
                {
                    ProductID = 4,
                    CategoryID = 5,
                    ImageUrl = "https://i.etsystatic.com/12377040/r/il/3c045d/5577946433/il_fullxfull.5577946433_op9f.jpg",
                    Name = "Nature",
                    Description = "Water Color - Painting",
                    Rating = " 4.8",
                    RatingDetail = " (30 raitings)",
                    HomeSelected = "EmptyHeart",
                    Price = 145
                },
                new PhotoItem
                {
                    ProductID = 5,
                    CategoryID = 2,
                    ImageUrl = "https://images-cdn.ubuy.co.in/634e37b7d57ac13b6907b5b8-v-inspire-art-30x40-inch-abstract-art.jpg",
                    Name = "V Inspire",
                    Description = "Abstract Art - Painting",
                    Rating = " 4.4",
                    RatingDetail = " (120 raitings)",
                    HomeSelected = "CompleteHeart",
                    Price = 220
                },
                new PhotoItem
                {
                    ProductID = 6,
                    CategoryID = 2,
                    ImageUrl = "https://cdn11.bigcommerce.com/s-x49po/images/stencil/1500x1500/products/86342/252452/1655983273158_20220616-180740__46890.1687004840.jpg?c=2",
                    Name = "Musician",
                    Description = "Abstract Art - Painting",
                    Rating = " 4.8",
                    RatingDetail = " (156 raitings)",
                    HomeSelected = "EmptyHeart",
                    Price = 300
                },
                new PhotoItem
                {
                    ProductID = 7,
                    CategoryID = 3,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTpapt5r8-97C3JbqrZ3IZYV4Cs7ZVsxUBFdg&s",
                    Name = "Popart",
                    Description = "Pop Art - Paints",
                    Rating = " 4.4",
                    RatingDetail = " (120 raitings)",
                    HomeSelected = "CompleteHeart",
                    Price = 110
                },
                new PhotoItem
                {
                    ProductID = 8,
                    CategoryID = 6,
                    ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTCGk5HAssigDgEYBIffSl288UGy_-eh8F42w&s",
                    Name = "Village",
                    Description = "Painterly - Paints",
                    Rating = " 4.8",
                    RatingDetail = " (156 raitings)",
                    HomeSelected = "EmptyHeart",
                    Price = 150
                }
             };

            foreach (var item in PhotoItems)
            {
                int result = sqliteConnection.Insert(item);
            }
        }
    }
}
