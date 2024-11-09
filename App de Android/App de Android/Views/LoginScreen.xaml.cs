using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
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
            string apiUrl = "https://myowndomain.lol:5001/api/Auth/login";

            var httpClient = new HttpClient();
            var loginModel = new
            {
                email = EmailEntry.Text.ToString(),
                password = PasswordEntry.Text.ToString()
            };

            try
            {
                var json = JsonConvert.SerializeObject(loginModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await httpClient.PostAsync(apiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Deserialize JSON response to extract the token
                    var tokenObj = JsonConvert.DeserializeObject<Dictionary<string, string>>(responseContent);
                    var token = tokenObj["token"];

                    await SecureStorage.SetAsync("authToken", token);

                    await Navigation.PushAsync(new TabbedPage1());
                }
                else
                {
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
