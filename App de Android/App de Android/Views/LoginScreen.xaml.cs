using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Firebase.Auth;
using Newtonsoft.Json;
using Xamarin.Essentials;
using System.Net.Http;

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
            string apiUrl = "https://myowndomain.lol:5001/api/Auth/login";  // Replace with your actual API URL

            var httpClient = new HttpClient();
            var loginModel = new
            {
                email = EmailEntry.Text.ToString(),
                password = PasswordEntry.Text.ToString()
            };

            try
            {
                // Convert to JSON
                var json = JsonConvert.SerializeObject(loginModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send the POST request
                var response = await httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Handle successful login
                    await Navigation.PushAsync(new TabbedPage1());
                }
                else
                {
                    // Handle error responses
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", $"Failed to login: {errorMessage}", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
        }

        private async void RegisterNewUser(object sender, EventArgs e)
        {
            //Handle register tap for register text
            await Navigation.PushAsync(new RegisterScreen());
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // Check if both username and password fields are not empty
            bool isUsernameFilled = !string.IsNullOrEmpty(EmailEntry.Text);
            bool isPasswordFilled = !string.IsNullOrEmpty(PasswordEntry.Text);

            // Enable the login button only if both fields are filled
            LoginButton.IsEnabled = isUsernameFilled && isPasswordFilled;
        }
    }
}
