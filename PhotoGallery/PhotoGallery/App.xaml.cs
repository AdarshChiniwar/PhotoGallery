using PhotoGallery.Interfaces;
using PhotoGallery.Model;
using PhotoGallery.View;
using SQLite;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PhotoGallery.Helpers;

namespace PhotoGallery
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            string user = Preferences.Get("user", string.Empty);
            Application.Current.UserAppTheme = OSAppTheme.Light;
            CreateTables();
            //HelpersClass helpers = new HelpersClass();
            //helpers.AddCategories();
            //helpers.AddPhotoItems();
            if (string.IsNullOrEmpty(user))
            {
                MainPage = new NavigationPage(new SignIn());
            }
            else
            {
                MainPage = new NavigationPage(new MainPage(user));
            }


        }
        private void CreateTables()
        {
            SQLiteConnection sqliteConnection = DependencyService.Get<Isqlite>().GetConnection();
            sqliteConnection.CreateTable<Category>();
            sqliteConnection.CreateTable<PhotoItem>();
            sqliteConnection.CreateTable<CartItem>();
            sqliteConnection.CreateTable<User>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
