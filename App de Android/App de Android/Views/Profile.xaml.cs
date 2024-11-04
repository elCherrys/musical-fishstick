using System;
using Xamarin.Forms;

namespace App_de_Android.Views
{
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
        }

        private void Frame1_Tapped(object sender, EventArgs e)
        {
            // Lógica para el marco 1
            DisplayAlert("Tapped", "Marco 1 fue tocado", "OK");
        }

        private void Frame2_Tapped(object sender, EventArgs e)
        {
            // Lógica para el marco 2
            DisplayAlert("Tapped", "Marco 2 fue tocado", "OK");
        }

        private void Frame3_Tapped(object sender, EventArgs e)
        {
            // Lógica para el marco 3
            DisplayAlert("Tapped", "Marco 3 fue tocado", "OK");
        }

        private void Frame4_Tapped(object sender, EventArgs e)
        {
            // Lógica para el marco 4
            DisplayAlert("Tapped", "Marco 4 fue tocado", "OK");
        }

        private void Frame5_Tapped(object sender, EventArgs e)
        {
            // Lógica para el marco 5
            DisplayAlert("Tapped", "Marco 5 fue tocado", "OK");
        }

        private async void Frame6_Tapped(object sender, EventArgs e)
        {
            // Lógica para el marco 6
            await Navigation.PushAsync(new LoginScreen());
        }







    }
}
