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
            string username = EmailEntry.Text;
            string password = PasswordEntry.Text;

            string WebApiKey = Key.GetApiKey();
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
           
            //login logic
            try
            {
                var auth = await authProvider.SignInWithEmailAndPasswordAsync(username.ToString(), password.ToString());
                var content = await auth.GetFreshAuthAsync();
                var serializedcontnet = JsonConvert.SerializeObject(content);
                Preferences.Set("MyFirebaseRefreshToken", serializedcontnet);
                await Navigation.PushAsync(new TabbedPage1());
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "Invalid username or password", "OK");
                return;
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
