﻿using PhotoGallery.Model;
using PhotoGallery.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhotoGallery.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailsView : ContentPage
    {
        public ProductDetailsView(PhotoItem photoItem)
        {
            InitializeComponent();
            ProductDetailsViewModel productDetailsViewModel = new ProductDetailsViewModel(photoItem);   
            this.BindingContext = productDetailsViewModel;
        }
        async void ImageButton_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}