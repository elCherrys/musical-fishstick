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
	public partial class Carrito : ContentPage
	{
		public Carrito ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void OnNextTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeliveryOrPickupScreen());
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            // Regresar a la página anterior
            await Navigation.PushAsync(new Views.TabbedPage1());  // Usar PopAsync para regresar a la página anterior
        }
    }
}