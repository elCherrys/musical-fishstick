using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using App_de_Android.Models;
using System;
using System.Net.Http.Headers;
using Xamarin.Essentials;

namespace App_de_Android.Views
{
    public partial class AddAddressView : ContentPage
    {
        private ObservableCollection<AddressModel> _addresses;

        public AddAddressView(ObservableCollection<AddressModel> addresses)
        {
            InitializeComponent();
            _addresses = addresses;
        }

        private async void OnAddAddressButtonClicked(object sender, EventArgs e)
        {
            // Create AddressModel instance
            var addressModel = new AddressModel
            {
                Street = StreetEntry.Text,
                Number = NumberEntry.Text,
                Neighborhood = NeighborhoodEntry.Text,
                City = CityEntry.Text,
                State = StateEntry.Text
            };

            // Send address to API
            var success = await SendAddressToApi(addressModel);

            // Display success or error message
            if (success)
            {
                await DisplayAlert("Exitoso", "Dirección agregada con éxito!", "OK");
                // Optionally add the new address to the collection
                _addresses.Add(addressModel);
            }
            else
            {
                await DisplayAlert("Error", "No se pudo agregar la dirección.", "OK");
            }

            await Navigation.PopModalAsync();
        }

        private async Task<bool> SendAddressToApi(AddressModel addressModel)
        {
            string apiUrl = "https://myowndomain.lol:5001/api/product/address";  // Corrected URL

            var json = JsonConvert.SerializeObject(addressModel);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var token = await SecureStorage.GetAsync("authToken");
            token = token.Trim(); // Ensure the token is trimmed

            // Log the extracted token for debugging
            Console.WriteLine($"Extracted Token: {token}");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                // Log the final request details
                Console.WriteLine($"Request: {apiUrl}, Authorization: Bearer {token}, Content: {json}");

                var response = await client.PostAsync(apiUrl, content);
                if (!response.IsSuccessStatusCode)
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    await DisplayAlert("Error", $"Failed to add address: {errorMessage}", "OK");
                    return false;
                }

                return true;
            }
        }
    }
}
