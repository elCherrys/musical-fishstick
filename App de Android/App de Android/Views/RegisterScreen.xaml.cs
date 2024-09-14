using Firebase.Auth;
using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_de_Android.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterScreen : ContentPage
    {
        public RegisterScreen()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void EvaluateForm(object sender, TextChangedEventArgs e)
        {
            // Ensure that Text properties are not null (they can be empty but not null)
            string username = UsernameEntry.Text ?? string.Empty;
            string email = EmailEntry.Text ?? string.Empty;
            string password = PasswordEntry.Text ?? string.Empty;
            string confirmPassword = ConfirmPasswordEntry.Text ?? string.Empty;

            // Check if all fields are filled
            bool allFieldsFilled = !string.IsNullOrEmpty(username) &&
                                   !string.IsNullOrEmpty(email) &&
                                   !string.IsNullOrEmpty(password) &&
                                   !string.IsNullOrEmpty(confirmPassword);

            // Validate email only if it's not empty
            bool isEmailValid = !string.IsNullOrEmpty(email) &&
                                Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
            InvalidEmailLabel.IsVisible = !isEmailValid && !string.IsNullOrEmpty(email);

            // Password complexity checks
            bool isPasswordLongEnough = password.Length > 6;
            bool hasUppercase = Regex.IsMatch(password, @"[A-Z]");
            bool hasLowercase = Regex.IsMatch(password, @"[a-z]");
            bool hasDigit = Regex.IsMatch(password, @"\d");
            // Show or hide password complexity labels
            if (!string.IsNullOrEmpty(password))
            {
                PasswordLengthLabel.IsVisible = !isPasswordLongEnough;
                PasswordUppercaseLabel.IsVisible = !hasUppercase;
                PasswordLowercaseLabel.IsVisible = !hasLowercase;
                PasswordNumberLabel.IsVisible = !hasDigit;
            }
            else
            {
                // Hide the labels if no password is being entered
                PasswordLengthLabel.IsVisible = false;
                PasswordUppercaseLabel.IsVisible = false;
                PasswordLowercaseLabel.IsVisible = false;
                PasswordNumberLabel.IsVisible = false;
            }
            //Check if passwords match
            bool isPasswordValid = isPasswordLongEnough && hasUppercase && hasLowercase && hasDigit;
            bool passwordsMatch = password == confirmPassword;
            PasswordsMatchLabel.IsVisible = !passwordsMatch && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(confirmPassword);

            // Enable button only if all fields are filled, email is valid, and passwords match
            RegisterButton.IsEnabled = allFieldsFilled && isEmailValid && passwordsMatch;
        }

        private async void RegisterUser(object sender, EventArgs e)
        {
            string WebApiKey = Key.GetApiKey();

            try
            {
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(WebApiKey));
                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(EmailEntry.Text.ToString(), PasswordEntry.Text.ToString());
                string gettoken = auth.FirebaseToken;

                await Navigation.PopAsync();
            }
            catch
            {
                await DisplayAlert("Error", "Failed to register user. Please try again.", "OK");
            }
        }
    }
}