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
            ViewModel = new AddressListViewModel(Navigation);
            BindingContext = ViewModel;

            ViewModel.LoadAddressesCommand.Execute(null);
        }
    }
}
