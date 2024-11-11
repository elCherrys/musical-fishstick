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
    }
}