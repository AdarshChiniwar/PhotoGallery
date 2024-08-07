using PhotoGallery.Interfaces;
using PhotoGallery.Model;
using PhotoGallery.View;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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

        private bool isCartViewVisible;
        public bool IsCartViewVisible
        {
            get { return isCartViewVisible; }
            set { isCartViewVisible = value; OnPropertyChanged(nameof(IsCartViewVisible)); }
        }

        private bool isPaymentPageVisible;

        public bool IsPaymentPageVisible
        {
            get { return isPaymentPageVisible; }
            set { isPaymentPageVisible = value; OnPropertyChanged(nameof(IsPaymentPageVisible)); }
        }
        public ICommand BacktoCartCmd { get; set; }
        public ICommand SwithToPaymentCmd { get; set; }
        private string cardName;

        public string CardName
        {
            get { return cardName; }
            set { cardName = value; OnPropertyChanged(nameof(CardName)); }
        }
        private string cardNumber;

        public string CardNumber
        {
            get { return cardNumber; }
            set { cardNumber = value; OnPropertyChanged(nameof(CardNumber)); }
        }

        private string cvv;

        public string Cvv
        {
            get { return cvv; }
            set { cvv = value; OnPropertyChanged(nameof(Cvv)); }
        }
        private string addressLine1;

        public string AddressLine1
        {
            get { return addressLine1; }
            set { addressLine1 = value; OnPropertyChanged(nameof(AddressLine1)); }
        }

        private string addressLine2;

        public string AddressLine2
        {
            get { return addressLine2; }
            set { addressLine2 = value; OnPropertyChanged(nameof(AddressLine2)); }
        }
        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; OnPropertyChanged(nameof(PhoneNumber)); }
        }

        private string pinCode;

        public string PinCode
        {
            get { return pinCode; }
            set { pinCode = value; OnPropertyChanged(nameof(PinCode)); }
        }

        private string city;

        public string City
        {
            get { return city; }
            set { city = value; OnPropertyChanged(nameof(City)); }
        }

        #endregion

        #region Constructor
        public CartViewModel()
        {
            IsCartViewVisible = true;
            IsPaymentPageVisible = false;
            sqliteConnection = DependencyService.Get<Isqlite>().GetConnection();
            CartItems = new ObservableCollection<UserCartItem>();
            LoadItems();
            PlaceOrdersCommand = new Command(() => PlaceOrdersAsync());
            BacktoCartCmd = new Command(BackToCart);
            SwithToPaymentCmd = new Command(ShowPayment);
        }




        #endregion

        #region Functions
        private void ShowPayment()
        {
            IsCartViewVisible = false;
            IsPaymentPageVisible = true;
        }
        private void BackToCart()
        {
            IsCartViewVisible = true;
            IsPaymentPageVisible = false;
        }

        private async void PlaceOrdersAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(City) || string.IsNullOrEmpty(PinCode)
                     || string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(AddressLine2)
                     || string.IsNullOrEmpty(AddressLine1) || string.IsNullOrEmpty(Cvv) || string.IsNullOrEmpty(CardNumber)
                     || string.IsNullOrEmpty(CardName))
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "Some fields are empty", "OK");
                }
                else
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
                    BackToCart();
                }
               
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
