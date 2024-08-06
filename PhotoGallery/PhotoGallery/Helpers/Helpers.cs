using PhotoGallery.Interfaces;
using PhotoGallery.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async void AddCategories()
        {
            try
            {
                var details = (from x in sqliteConnection.Table<Category>() select x).ToList();
                foreach (var item in details)
                {
                    sqliteConnection.Delete(item);
                }

                Categories = new List<Category>()
            {
                new Category(){

                    CategoryID=1,
                    CategoryName = "Impressionism",
                    CategoryPoster = "Impressionismposter.png",
                    ImageUrl = "Impressionismimage.png"
                },
                new Category(){
                 CategoryID=2,
                    CategoryName = "Abstract art",
                    CategoryPoster = "abstractposter.png",
                    ImageUrl = "abstractimage.png"
                },
                new Category(){
                   CategoryID=3,
                    CategoryName = "Pop art",
                    CategoryPoster = "popposter.png",
                    ImageUrl = "popimage.png"
                },
                new Category(){
                    CategoryID=4,
                    CategoryName = "Realism",
                    CategoryPoster = "realismposter.jpg",
                    ImageUrl = "realismimage.jpeg"
                },
                new Category(){
                CategoryID=5,
                    CategoryName = "Water Color",
                    CategoryPoster = "waterposter.png",
                    ImageUrl = "waterimage.png"
                },
                new Category(){
                   CategoryID=6,
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
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
          
        }

        public async void AddPhotoItems()
        {
            try
            {
                var details = (from x in sqliteConnection.Table<PhotoItem>() select x).ToList();
                foreach (var item in details)
                {
                    sqliteConnection.Delete(item);
                }
                PhotoItems = new List<PhotoItem>()
            {
                new PhotoItem
                {
                    ProductID = 1,
                    CategoryID = 1,
                    ImageUrl = "jacksonart.jpg",
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
                    ImageUrl = "women.jpg",
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
                    ImageUrl = "farm.jpg",
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
                    ImageUrl = "water.webp",
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
                    ImageUrl = "vinspire.jpg",
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
                    ImageUrl = "musician.jpg",
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
                    ImageUrl = "paints.jpeg",
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
                    ImageUrl = "village.jpeg",
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
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
          
        }
    }
}
