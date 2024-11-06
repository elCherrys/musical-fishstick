using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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

        private async void Frame3_Tapped(object sender, EventArgs e)
        {
            // Lógica para el marco 3
            await Navigation.PushAsync(new CouponScreen());
        }

        private async void Frame4_Tapped(object sender, EventArgs e)
        {
            // Lógica para el marco 4
            await Navigation.PushAsync(new Help());
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
