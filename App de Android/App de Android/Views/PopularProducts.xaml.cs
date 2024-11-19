using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Newtonsoft.Json;
using App_de_Android.Models;

namespace App_de_Android.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PopularProducts : ContentPage
    {
        public ObservableCollection<ProductsModel> Products { get; set; }

        private bool isLoading;
        public bool IsLoading
        {
            get => isLoading;
            set
            {
                isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }

        public PopularProducts()
        {
            InitializeComponent();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            try
            {
                IsLoading = true;
                using (HttpClient httpClient = new HttpClient())
                {
                    string apiUrl = "https://myowndomain.lol:5001/api/product/get"; // Your API URL
                    var response = await httpClient.GetStringAsync(apiUrl);
                    var products = JsonConvert.DeserializeObject<ObservableCollection<ProductsModel>>(response);

                    foreach (var product in products)
                    {
                        product.Id = product.Id; // Ensure the Id field is used from the document
                        Console.WriteLine($"Product loaded: {product.Id} - {product.name}");
                    }

                    Products = products;
                    BindingContext = this; // Set the BindingContext to the current instance
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching products: " + ex.Message); // Handle any exceptions
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async void OnAddToCartClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var productId = button?.CommandParameter as string;
            if (!string.IsNullOrEmpty(productId))
            {
                await AddToCart(productId, 1); // Add 1 item to the cart
            }
        }

        private async Task AddToCart(string productId, int amount)
        {
            var token = await SecureStorage.GetAsync("authToken");
            if (token != null)
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    var url = $"https://myowndomain.lol:5001/api/cart/add?productId={productId}&amount={amount}";
                    Console.WriteLine($"AddToCart URL: {url}"); // Log the URL for debugging
                    var response = await client.PostAsync(url, null);
                    Console.WriteLine($"AddToCart response: {response.StatusCode}");
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"AddToCart response content: {responseContent}");
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Authentication token not found", "OK");
            }
        }
    }
}
