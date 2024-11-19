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
	public partial class FilteredCategories : ContentPage
	{
		public FilteredCategories ()
		{
			InitializeComponent ();
            NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = new FilteredCategoriesViewModel();
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            // Regresar a la página anterior
            await Navigation.PopAsync();  // Usar PopAsync para regresar a la página anterior
        }
    }
}