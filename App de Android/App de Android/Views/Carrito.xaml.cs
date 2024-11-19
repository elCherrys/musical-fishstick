using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace App_de_Android.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Carrito : ContentPage
    {
        public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

        public Carrito()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            CartListView.ItemsSource = Products;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadCartItems(); // Refresh cart items when the page appears
        }

        private async void LoadCartItems()
        {
            var token = await SecureStorage.GetAsync("authToken");
            if (token != null)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var response = await client.GetAsync("https://myowndomain.lol:5001/api/cart/cart-products");
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();
                        var products = JsonConvert.DeserializeObject<List<Product>>(json);
                        Products.Clear();
                        foreach (var product in products)
                        {
                            Products.Add(product); // Ensure observable collection is updated
                        }
                    }
                }
            }
            else
            {
                await DisplayAlert("Error", "Authentication token not found", "OK");
            }
        }

        private async void OnNextTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeliveryOrPickupScreen());
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Use PopAsync to go back to the previous page
        }

        private async void OnIncreaseClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var product = button?.BindingContext as Product;
            if (product != null)
            {
                Console.WriteLine($"OnIncreaseClicked: Increasing amount for product ID: {product.Id}");
                await AddOrUpdateCart(product.Id, 1); // Increase amount by 1
                LoadCartItems(); // Refresh cart items
            }
        }

        private async void OnDecreaseClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var product = button?.BindingContext as Product;
            if (product != null)
            {
                Console.WriteLine($"OnDecreaseClicked: Decreasing amount for product ID: {product.Id}");
                await RemoveOrUpdateCart(product.Id); // Decrease amount by 1
                LoadCartItems(); // Refresh cart items
            }
        }

        private async void OnRemoveClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var product = button?.BindingContext as Product;
            if (product != null)
            {
                Console.WriteLine($"OnRemoveClicked: Removing product with ID: {product.Id}");
                await RemoveWholeItemFromCart(product.Id); // Remove whole item
                LoadCartItems(); // Refresh cart items
            }
        }

        private async Task AddOrUpdateCart(string productId, int amount)
        {
            var token = await SecureStorage.GetAsync("authToken");
            if (token != null)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var url = $"https://myowndomain.lol:5001/api/cart/add?productId={productId}&amount={amount}";
                    var response = await client.PostAsync(url, null);
                    Console.WriteLine($"AddOrUpdateCart response: {response.StatusCode}");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"AddOrUpdateCart response content: {responseContent}");
                }
            }
            else
            {
                await DisplayAlert("Error", "Authentication token not found", "OK");
            }
        }

        private async Task RemoveOrUpdateCart(string productId)
        {
            var token = await SecureStorage.GetAsync("authToken");
            if (token != null)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var url = $"https://myowndomain.lol:5001/api/cart/remove?productId={productId}";
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(url)
                    };
                    var response = await client.SendAsync(request);
                    Console.WriteLine($"RemoveOrUpdateCart response: {response.StatusCode}");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"RemoveOrUpdateCart response content: {responseContent}");
                }
            }
            else
            {
                await DisplayAlert("Error", "Authentication token not found", "OK");
            }
        }

        private async Task RemoveWholeItemFromCart(string productId)
        {
            var token = await SecureStorage.GetAsync("authToken");
            if (token != null)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var url = $"https://myowndomain.lol:5001/api/cart/remove-whole?productId={productId}";
                    var request = new HttpRequestMessage
                    {
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(url)
                    };
                    var response = await client.SendAsync(request);
                    Console.WriteLine($"RemoveWholeItemFromCart response: {response.StatusCode}");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"RemoveWholeItemFromCart response content: {responseContent}");
                }
            }
            else
            {
                await DisplayAlert("Error", "Authentication token not found", "OK");
            }
        }
    }

    public class Product
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public string CategoryName { get; set; }
        public int Stock { get; set; }
        public int Amount { get; set; } // Added this field to hold the amount from the cart
    }
}
