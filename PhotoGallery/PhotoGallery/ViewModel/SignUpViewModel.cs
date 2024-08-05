using PhotoGallery.Interfaces;
using PhotoGallery.Model;
using PhotoGallery.View;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PhotoGallery.ViewModel
{
    public class SignUpViewModel : BaseViewModel
    {
        #region Global Variables
        private SQLiteConnection sqliteConnection;
        #endregion

        #region Properties
        private string userName;
        public string UserName
        {
            get { return userName; }
            set { userName = value; OnPropertyChanged(nameof(UserName)); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { password = value; OnPropertyChanged(nameof(Password)); }
        }

        public ICommand SignUpCmd { get; set; }
        public ICommand SignInCmd { get; set; }
        #endregion

        #region Constructor
        public SignUpViewModel()
        {
            SignUpCmd = new Command(SignUp);
            SignInCmd = new Command(SignIn);
            sqliteConnection = DependencyService.Get<Isqlite>().GetConnection();
        }
        #endregion

        #region Functions
        private async void SignIn(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        private async void SignUp()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Incomplete input", "Fields cannot be empty", "Ok");
            }
            else
            {
                User user = new User()
                {
                    Name = UserName,
                    Password = Password
                };
                int result = sqliteConnection.Insert(user);
                if (result == 1)
                {

                    await Application.Current.MainPage.DisplayAlert("Info", "Congratulations , Sign up Successfull", "OK");
                    await Application.Current.MainPage.Navigation.PopAsync();
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Registration Failed!!!", "Please try again", "ERROR");
                }
            }
        }
        #endregion
    }
}
