using PhotoGallery.Interfaces;
using PhotoGallery.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhotoGallery.ViewModel
{
    public class UserOrdersHistoryViewModel : BaseViewModel
    {
        #region Global Variables
        private SQLiteConnection sqliteConnection;
        #endregion

        #region Properties
        public ObservableCollection<UserOrdersHistory> OrderDetails { get; set; }

        private bool _IsBusy;
        public bool IsBusy
        {
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
            }

            get
            {
                return _IsBusy;
            }
        }
        #endregion

        #region Constructor
        public UserOrdersHistoryViewModel()
        {
            sqliteConnection = DependencyService.Get<Isqlite>().GetConnection();
            OrderDetails = new ObservableCollection<UserOrdersHistory>();
            LoadUserOrders();
        }
        #endregion

        #region Functions
        private async void LoadUserOrders()
        {
            try
            {
                IsBusy = true;
                OrderDetails.Clear();
                List<UserOrdersHistory> UserOrders = new List<UserOrdersHistory>();
                var uname = Preferences.Get("user", "Guest");
                var details = (from x in sqliteConnection.Table<Order>() select x).Where(x => x.Username == uname).ToList();
                //var service = new UserOrderHistoryService();
                //var details = await service.GetOrderDetailsAsync();
          
                foreach (var order in details)
                {
                    UserOrdersHistory uoh = new UserOrdersHistory();
                    uoh.OrderId = order.OrderId;
                    uoh.ReceiptId = order.ReceiptId;
                    uoh.TotalCost = order.TotalCost;
                    var orderDetails = (from x in sqliteConnection.Table<OrderDetails>() select x).Where(x => x.OrderId == order.OrderId).ToList();
                    uoh.AddRange(orderDetails);
                    UserOrders.Add(uoh);
                }
                foreach (var item in UserOrders)
                {
                    OrderDetails.Add(item);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        #endregion
    }
}
