using PhotoGallery.Interfaces;
using PhotoGallery.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PhotoGallery.ViewModel
{
    public class CategoryViewModel : BaseViewModel
    {

        #region Global Variables
        private SQLiteConnection sqliteConnection;
        #endregion

        #region Properties
        private Category _SelectedCategory;
        public Category SelectedCategory
        {
            set
            {
                _SelectedCategory = value;
                OnPropertyChanged();
            }

            get
            {
                return _SelectedCategory;
            }
        }

        public ObservableCollection<PhotoItem> PhotoItemByCategory { get; set; }

        private int _TotalFoodItems;
        public int TotalFoodItems
        {
            set
            {
                _TotalFoodItems = value;
                OnPropertyChanged();
            }

            get
            {
                return _TotalFoodItems;
            }
        }
        #endregion


        #region Constructor
        public CategoryViewModel(Category category)
        {
            sqliteConnection = DependencyService.Get<Isqlite>().GetConnection();
            SelectedCategory = category;
            PhotoItemByCategory = new ObservableCollection<PhotoItem>();
            GetFoodItems(category.CategoryID);
        }
        #endregion

        #region Functions
        private async void GetFoodItems(int categoryID)
        {
            try
            {
                PhotoItemByCategory.Clear();
                var details = (from x in sqliteConnection.Table<PhotoItem>() select x).ToList().Take(3);
                foreach (var item in details)
                {
                    if(item.CategoryID == categoryID)
                    {
                        PhotoItemByCategory.Add(item);
                        TotalFoodItems = PhotoItemByCategory.Count;
                    }
                }
                if (PhotoItemByCategory.Count == 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Info", "No items available!", "OK");
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
