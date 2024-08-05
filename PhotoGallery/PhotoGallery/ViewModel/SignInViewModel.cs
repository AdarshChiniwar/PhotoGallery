using PhotoGallery.Interfaces;
using PhotoGallery.Model;
using PhotoGallery.View;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PhotoGallery.ViewModel
{
    public class SignInViewModel : BaseViewModel
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

        public ICommand SigninCmd { get; set; }
        public ICommand SignUpCmd { get; set; }
        #endregion

        #region Constructer
        public SignInViewModel()
        {
            SigninCmd = new Command(SignIn);
            SignUpCmd = new Command(SignUp);
            sqliteConnection = DependencyService.Get<Isqlite>().GetConnection();
        }

        #endregion

        #region Private Functions
        private async void SignUp()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SignUp());
        }
        private async void SignIn()
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Incomplete input", "Fields cannot be empty", "Ok");
            }
            else
            {
                bool val = ValidateTheCreds();
                if (val)
                {
                    Preferences.Set("user", UserName);
                    Application.Current.MainPage = new NavigationPage(new MainPage(UserName));
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Incorrect username or password", "Ok");
                }
            }
        }
        private bool ValidateTheCreds()
        {
            bool status = false;
            User user = sqliteConnection.Table<User>().FirstOrDefault(elm => elm.Name == UserName);
            if (user != null && user.Password == Password)
            {
                status = true;
            }
            return status;
        }
        #endregion


    }
}
