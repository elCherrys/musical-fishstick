using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App_de_Android.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CouponScreen : ContentPage
    {
        public CouponScreen()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void OnCupon1Tapped(object sender, EventArgs e)
        {
            // Mostrar un mensaje al tocar el Frame
            await DisplayAlert("¡Cupón → 2x1 en Repollo!", "Aplicado!", "OK");
        }

        private async void OnCupon2Tapped(object sender, EventArgs e)
        {
            // Mostrar un mensaje al tocar el Frame
            await DisplayAlert("¡Cupón → 15% OFF en Carnes!", "Aplicado!", "OK");
        }
        private async void OnCupon3Tapped(object sender, EventArgs e)
        {
            // Mostrar un mensaje al tocar el Frame
            await DisplayAlert("¡Cupón → El segundo a mitad de precio!", "Aplicado!", "OK");
        }

        private async void OnCupon4Tapped(object sender, EventArgs e)
        {
            // Mostrar un mensaje al tocar el Frame
            await DisplayAlert("¡Cupón → 10% OFF en Cítricos", "Aplicado!", "OK");
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            // Regresar a la página anterior
            await Navigation.PopAsync();  // Usar PopAsync para regresar a la página anterior
        }
    }
}