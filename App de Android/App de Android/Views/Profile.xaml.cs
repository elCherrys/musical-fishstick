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

        private async void Frame1_Tapped(object sender, EventArgs e)
        {
            // Lógica para el marco 1
            await Navigation.PushAsync(new Cuenta());
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

        private async void Frame5_Tapped(object sender, EventArgs e)
        {
            // Lógica para el marco 5
            await Navigation.PushAsync(new Settings());
        }

        private async void Frame6_Tapped(object sender, EventArgs e)
        {
            // Lógica para el marco 6
            await Navigation.PushAsync(new LoginScreen());
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            // Regresar a la página anterior
            await Navigation.PushAsync(new Views.TabbedPage1());  // Usar PopAsync para regresar a la página anterior
        }

    }
}
