﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App_de_Android.Views
{ public partial class LoginScreen : ContentPage
    {
        public LoginScreen()
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
                await Navigation.PushAsync(new Views.TabbedPage1());
            }
            else
            {
                ErrorMessage.Text = "Invalid username or password!";
                ErrorMessage.IsVisible = true;
            }
        }

        private async void RegisterNewUser(object sender, EventArgs e)
        {
            //Handle register tap
            await Navigation.PushAsync(new RegisterScreen());
        }
    }
}