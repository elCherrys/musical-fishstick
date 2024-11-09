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
        }

        private async void OnCupon1Tapped(object sender, EventArgs e)
        {
            // Mostrar un mensaje al tocar el Frame
            await DisplayAlert("¡Cupón 1 50% DE DESCUENTO", "Ingresa cup50", "OK");
        }

        private async void OnCupon2Tapped(object sender, EventArgs e)
        {
            // Mostrar un mensaje al tocar el Frame
            await DisplayAlert("¡Cupón 2 20% DE DESCUENTO!", "Ingresa cup20", "OK");
        }
        private async void OnCupon3Tapped(object sender, EventArgs e)
        {
            // Mostrar un mensaje al tocar el Frame
            await DisplayAlert("¡Cupón 3 ENVIO GRATIS", "PRIMERPEDIDO.", "OK");
        }

    }
}