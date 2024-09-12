using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App_de_Android.Views
{ public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            // Simple login logic
            if (username == "admin" && password == "password123")
            {
                await Navigation.PushAsync(new Views.Home());
            }
            else
            {
                ErrorMessage.Text = "Invalid username or password!";
                ErrorMessage.IsVisible = true;
            }
        }
    }
}
