using PhotoGallery.Interfaces;
using PhotoGallery.Model;
using PhotoGallery.View;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhotoGallery.ViewModel
{
    public class ProductsViewModel : BaseViewModel
    {
        #region Global Variables
        private SQLiteConnection sqliteConnection;
        #endregion

        #region Properties
        private string _UserName;
        public string UserName
        {
            set
            {
                _UserName = value;
                OnPropertyChanged();
            }

            get
            {
                return _UserName;
            }
        }

        private int _UserCartItemsCount;
        public int UserCartItemsCount
        {
            set
            {
                _UserCartItemsCount = value;
                OnPropertyChanged();
            }

            get
            {
                return _UserCartItemsCount;
            }
        }

        private string _SearchText;
        public string SearchText
        {
            set
            {
                _SearchText = value;
                OnPropertyChanged();
            }

            get
            {
                return _SearchText;
            }
        }

        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<PhotoItem> LatestItems { get; set; }

        public Command ViewCartCommand { get; set; }
        public Command LogoutCommand { get; set; }
        public Command ViewOrdersHistoryCommand { get; set; }
        public Command SearchViewCommand { get; set; }
        #endregion

        #region Constructor
        public ProductsViewModel()
        {
            sqliteConnection = DependencyService.Get<Isqlite>().GetConnection();
            var uname = Preferences.Get("user", String.Empty);
            if (String.IsNullOrEmpty(uname))
                UserName = "Guest";
            else
                UserName = uname;

            UserCartItemsCount = GetCartCount();

            Categories = new ObservableCollection<Category>();
            LatestItems = new ObservableCollection<PhotoItem>();

            ViewCartCommand = new Command(async () => await ViewCartAsync());
            LogoutCommand = new Command(async () => await LogoutAsync());
            ViewOrdersHistoryCommand = new Command(async () => await ViewOrderHistoryAsync());
            SearchViewCommand = new Command(async () => await SearchViewAsync());

            GetCategories();
            GetLatestItems();
        }
        #endregion

        #region Private Functions
        private int GetCartCount()
        {
            var count = sqliteConnection.Table<CartItem>().Count();
            return count;
        }
        private async Task SearchViewAsync()
        {
            //await Application.Current.MainPage.Navigation.PushModalAsync(
            //    new SearchResultsView(SearchText));
        }

        private async Task ViewOrderHistoryAsync()
        {
            // await Application.Current.MainPage.Navigation.PushModalAsync(new OrdersHistoryView());
        }

        private async Task ViewCartAsync()
        {
            //await Application.Current.MainPage.Navigation.PushModalAsync(new CartView());
        }

        private async Task LogoutAsync()
        {
            Preferences.Set("user", string.Empty);
            ClearAllDataFromCart();
            Application.Current.MainPage = new NavigationPage(new SignIn());
            //await Application.Current.MainPage.Navigation.PushModalAsync(new LogoutView());
          

        }

        private async void GetCategories()
        {
           
            try
            {
                Categories.Clear();
                var details = (from x in sqliteConnection.Table<Category>() select x).ToList();
                foreach (var item in details)
                {
                    Categories.Add(item);
                }
                if (Categories.Count == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "No categories available!", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
        private void ClearAllDataFromCart()
        {

        }
        private async void GetLatestItems()
        {
            try
            {
                LatestItems.Clear();
                var details = (from x in sqliteConnection.Table<PhotoItem>() select x).ToList().Take(3);
                foreach (var item in details)
                {
                    LatestItems.Add(item);
                }
                if (LatestItems.Count == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "No Latest items available!", "OK");
                }
            }
            catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
   
            //var data = await new FoodItemService().GetLatestFoodItemsAsync();
            //
            //foreach (var item in data)
            //{
            //    LatestItems.Add(item);
            //}
        }
        #endregion

    }
}
