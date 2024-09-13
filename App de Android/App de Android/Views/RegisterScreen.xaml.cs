using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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

        private void PasswordComparison(object sender, TextChangedEventArgs e)
        {
            bool passwordMatch = PasswordEntry.Text == ConfirmPasswordEntry.Text;

            if (passwordMatch || (string.IsNullOrEmpty(PasswordEntry.Text) || string.IsNullOrEmpty(ConfirmPasswordEntry.Text)))
            {
                PasswordsMatchLabel.IsVisible = false;
            }
            else
            {
                PasswordsMatchLabel.IsVisible = true;
            }
        }



        private async void RegisterUser(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void EmailValidation(object sender, TextChangedEventArgs e)
        {
            var email = e.NewTextValue;
            
            bool isValid = !string.IsNullOrWhiteSpace(email) && Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);

            InvalidEmailLabel.IsVisible = !isValid;

        }
    }
}