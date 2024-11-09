using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace App_de_Android.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Help : ContentPage
    {
        public Help()
        {
            InitializeComponent();
        }

        private async void Onhelp1Tapped(object sender, EventArgs e)
        {
            // Mostrar un mensaje al tocar el Frame
            await DisplayAlert("¡Cupón 1 50% DE DESCUENTO", "Ingresa cup50", "OK");
        }

        private async void Onhelp2Tapped(object sender, EventArgs e)
        {
            // Mostrar un mensaje al tocar el Frame
            await DisplayAlert("¡Cupón 2 20% DE DESCUENTO!", "Ingresa cup20", "OK");
        }
        private async void Onhelp3Tapped(object sender, EventArgs e)
        {
            // Mostrar un mensaje al tocar el Frame
            await DisplayAlert("¡Cupón 3 ENVIO GRATIS", "PRIMERPEDIDO.", "OK");
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
    }
}