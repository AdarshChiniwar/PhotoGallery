using PhotoGallery.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhotoGallery.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class OrdersHistoryView : ContentPage
	{
		public OrdersHistoryView ()
		{
			InitializeComponent ();
            UserOrdersHistoryViewModel userOrdersHistoryViewModel = new UserOrdersHistoryViewModel();
			this.BindingContext = userOrdersHistoryViewModel;
            LabelName.Text = @"Order's History of " + Preferences.Get("Username", "Guest") + ",";

        }
        async void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}