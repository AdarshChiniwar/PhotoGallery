using PhotoGallery.Interfaces;
using PhotoGallery.Model;
using PhotoGallery.View;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhotoGallery.ViewModel
{
    public class ProductDetailsViewModel : BaseViewModel
    {
        #region Global Variables
        private SQLiteConnection sqliteConnection;
        #endregion

        #region Properties
        private PhotoItem _SelectedPhotoItem;
        public PhotoItem SelectedPhotoItem
        {
            set
            {
                _SelectedPhotoItem = value;
                OnPropertyChanged();
            }

            get
            {
                return _SelectedPhotoItem;
            }
        }

        private int _TotalQuantity;
        public int TotalQuantity
        {
            set
            {
                this._TotalQuantity = value;
                if (this._TotalQuantity < 0)
                    this._TotalQuantity = 0;
                if (this._TotalQuantity > 10)
                    this._TotalQuantity -= 1;
                OnPropertyChanged();
            }

            get
            {
                return _TotalQuantity;
            }
        }

        public Command IncrementOrderCommand { get; set; }
        public Command DecrementOrderCommand { get; set; }
        public Command AddToCartCommand { get; set; }
        public Command ViewCartCommand { get; set; }
        public Command HomeCommand { get; set; }
        #endregion

        #region Constructor
        public ProductDetailsViewModel(PhotoItem photoItem)
        {
            sqliteConnection = DependencyService.Get<Isqlite>().GetConnection();
            SelectedPhotoItem = photoItem;
            TotalQuantity = 1;

            IncrementOrderCommand = new Command(() => IncrementOrder());
            DecrementOrderCommand = new Command(() => DecrementOrder());
            AddToCartCommand = new Command(() => AddToCart());
            ViewCartCommand = new Command(async () => await ViewCartAsync());
            HomeCommand = new Command(async () => await GotoHomeAsync());
        }
        #endregion

        #region Functions
        private async Task GotoHomeAsync()
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(new MainPage(""));
        }

        private async Task ViewCartAsync()
        {
           
            var count = sqliteConnection.Table<CartItem>().Count();
            if (count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Info", "No cart items available!", "OK");
                return;
            }
            await Application.Current.MainPage.Navigation.PushModalAsync(new CartView());
        }

        private void AddToCart()
        {
           
            try
            {
                CartItem ci = new CartItem()
                {
                    ProductId = SelectedPhotoItem.ProductID,
                    ProductName = SelectedPhotoItem.Name,
                    Price = SelectedPhotoItem.Price,
                    Quantity = TotalQuantity
                };
                var item = sqliteConnection.Table<CartItem>().ToList()
                    .FirstOrDefault(c => c.ProductId == SelectedPhotoItem.ProductID);
                if (item == null)
                    sqliteConnection.Insert(ci);
                else
                {
                    item.Quantity += TotalQuantity;
                    sqliteConnection.Update(item);
                }
                //sqliteConnection.Commit();
                //sqliteConnection.Close();
                Application.Current.MainPage.DisplayAlert("Cart", "Selected Item Added to Cart",
                    "OK");
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                //sqliteConnection.Close();
            }
        }

        private void DecrementOrder()
        {
            TotalQuantity--;
        }

        private void IncrementOrder()
        {
            TotalQuantity++;
        }
        #endregion
    }
}
