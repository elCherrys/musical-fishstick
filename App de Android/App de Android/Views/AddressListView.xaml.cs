using Xamarin.Forms;
using System;
using App_de_Android.ViewModels;

namespace App_de_Android.Views
{
    public partial class AddressListView : ContentPage
    {
        private AddressListViewModel ViewModel;

        public AddressListView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            ViewModel = new AddressListViewModel(Navigation);
            BindingContext = ViewModel;

            ViewModel.LoadAddressesCommand.Execute(null);
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            // Regresar a la página anterior
            await Navigation.PopAsync();  // Usar PopAsync para regresar a la página anterior
        }
    }
}
