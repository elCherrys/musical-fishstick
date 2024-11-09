using Xamarin.Forms;
using System.Collections.ObjectModel;
using System;

namespace App_de_Android.Views
{
    public partial class AddressListView : ContentPage
    {
        public ObservableCollection<string> Addresses { get; set; }

        public AddressListView()
        {
            InitializeComponent();
            Addresses = new ObservableCollection<string>
            {
                "123 Main St",
                "456 Elm St",
                "789 Oak St"
            };

            AddressListViewControl.ItemsSource = Addresses; // Correct reference to the ListView control
        }

        private async void OnAddAddressClicked(object sender, EventArgs e)
        {
            var addAddressPage = new AddAddressView(Addresses);
            await Navigation.PushModalAsync(addAddressPage);
        }
    }
}
