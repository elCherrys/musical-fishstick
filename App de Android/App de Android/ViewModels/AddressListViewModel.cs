using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;
using App_de_Android.Models;
using App_de_Android.Views;

namespace App_de_Android.ViewModels
{
    public class AddressListViewModel : BindableObject
    {
        private ObservableCollection<AddressModel> _addresses;
        private INavigation _navigation;

        public ObservableCollection<AddressModel> Addresses
        {
            get => _addresses;
            set
            {
                _addresses = value;
                OnPropertyChanged();
            }
        }

        public Command LoadAddressesCommand { get; }
        public Command AddAddressCommand { get; }

        public AddressListViewModel(INavigation navigation)
        {
            _navigation = navigation;
            Addresses = new ObservableCollection<AddressModel>();
            LoadAddressesCommand = new Command(async () => await LoadAddressesAsync());
            AddAddressCommand = new Command(async () => await NavigateToAddAddressPage());
        }

        private async Task LoadAddressesAsync()
        {
            try
            {
                var token = await SecureStorage.GetAsync("authToken");
                if (string.IsNullOrEmpty(token))
                {
                    throw new Exception("Authentication token not found.");
                }

                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await httpClient.GetAsync("https://myowndomain.lol:5001/api/product/addresses");

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API Response: {jsonResponse}");

                    var addressList = JsonConvert.DeserializeObject<List<AddressModel>>(jsonResponse);

                    Addresses.Clear();
                    foreach (var address in addressList)
                    {
                        Addresses.Add(address);
                    }

                    Console.WriteLine("Addresses loaded successfully");
                }
                else
                {
                    var errorResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Failed to fetch addresses from the API. Response: {errorResponse}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching addresses: {ex.Message}");
            }
        }

        private async Task NavigateToAddAddressPage()
        {
            var addAddressPage = new AddAddressView(Addresses);
            await _navigation.PushModalAsync(addAddressPage);
        }
    }
}
