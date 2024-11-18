using System;
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
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            // Regresar a la página anterior
            await Navigation.PopAsync();  // Usar PopAsync para regresar a la página anterior
        }
    }
}
