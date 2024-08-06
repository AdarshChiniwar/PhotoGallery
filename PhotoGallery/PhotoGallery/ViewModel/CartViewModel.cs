using PhotoGallery.Interfaces;
using PhotoGallery.Model;
using PhotoGallery.View;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhotoGallery.ViewModel
{
    public class CartViewModel : BaseViewModel
    {
        #region Global Variables
        private SQLiteConnection sqliteConnection;
        #endregion

        #region Properties
        public string OrderId { get; set; }
        public ObservableCollection<UserCartItem> CartItems { get; set; }

        private decimal _TotalCost;
        public decimal TotalCost
        {
            set
            {
                _TotalCost = value;
                OnPropertyChanged();
            }

            get
            {
                return _TotalCost;
            }
        }

        public Command PlaceOrdersCommand { get; set; }
        #endregion

        #region Constructor
        public CartViewModel()
        {
            sqliteConnection = DependencyService.Get<Isqlite>().GetConnection();
            CartItems = new ObservableCollection<UserCartItem>();
            LoadItems();
            PlaceOrdersCommand = new Command( () =>  PlaceOrdersAsync());
        }
        #endregion

        #region Functions
       
        private async void PlaceOrdersAsync()
        {
            try
            {
                var uname = Preferences.Get("user", "Guest");
                OrderId = Guid.NewGuid().ToString();
                var data = sqliteConnection.Table<CartItem>().ToList();
                foreach (var item in data)
                {
                    OrderDetails od = new OrderDetails()
                    {
                        OrderId = OrderId,
                        OrderDetailId = Guid.NewGuid().ToString(),
                        ProductID = item.ProductId,
                        ProductName = item.ProductName,
                        Price = item.Price,
                        Quantity = item.Quantity
                    };
                    sqliteConnection.Insert(od);
                }

                Order order = new Order()
                {
                    OrderId = OrderId,
                    Username = uname,
                    TotalCost = TotalCost

                };
                sqliteConnection.Insert(order);
                await Application.Current.MainPage.DisplayAlert("Info", "Order placed successfully", "OK");
                DeleteCartItem();
                LoadItems();
                await Application.Current.MainPage.Navigation.PushModalAsync(new OrdersHistoryView());
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
         
          
        }
        private async void DeleteCartItem()
        {
            try
            {
                var data = sqliteConnection.Table<CartItem>().ToList();
                foreach (var item in data)
                {
                    sqliteConnection.Delete(item);
                }
            }

            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
           
        }

        private async void LoadItems()
        {
            try
            {
                var items = sqliteConnection.Table<CartItem>().ToList();
                CartItems.Clear();
                foreach (var item in items)
                {
                    CartItems.Add(new UserCartItem()
                    {
                        CartItemId = item.CartItemId,
                        ProductId = item.ProductId,
                        ProductName = item.ProductName,
                        Price = item.Price,
                        Quantity = item.Quantity,
                        Cost = item.Price * item.Quantity
                    });
                    TotalCost += (item.Price * item.Quantity);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
         
        }
        #endregion
    }
}
