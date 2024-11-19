using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using App_de_Android.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using App_de_Android.Views;
using System.Windows.Input;
using System.Linq;
using System.Net.Http.Headers;
using Xamarin.Essentials;

public class HomeViewModel : INotifyPropertyChanged
{
    private ObservableCollection<ProductsModel> _products; // Observable collection of products
    private ObservableCollection<CategoriesModel> _categories; // Observable collection of categories

    // Commands for navigation
    public ICommand NavigateToCategoriesCommand { get; private set; }
    public ICommand NavigateToFilteredCategoriesCommand { get; private set; }
    public ICommand NavigateToProductsCommand { get; private set; }
    public ICommand NavigateToAddressCommand { get; private set; }
    public ICommand AddToCartCommand { get; private set; } // Command to add product to cart

    private bool _isLoading; // Loading state

    // Public property for products
    public ObservableCollection<ProductsModel> Products
    {
        get { return _products; }
        set
        {
            _products = value;
            Console.WriteLine($"Products updated: {Products.Count} items."); // Debug output
            OnPropertyChanged(nameof(Products));
        }
    }

    // Public property for categories
    public ObservableCollection<CategoriesModel> Categories
    {
        get { return _categories; }
        set
        {
            _categories = value;
            OnPropertyChanged(nameof(Categories));
        }
    }

    // Public property for loading state
    public bool IsLoading
    {
        get { return _isLoading; }
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged; // Event to handle property changes

    // Constructor
    public HomeViewModel()
    {
        LoadProducts(); // Load products from API
        LoadCategories(); // Load categories from API
        NavigateToCategoriesCommand = new Command(NavigateToCategories); // Command to navigate to categories view
        NavigateToFilteredCategoriesCommand = new Command<string>(NavigateToFilteredCategories); // Command to navigate to filtered categories view
        NavigateToProductsCommand = new Command(NavigateToProducts); // Command to navigate to products view
        NavigateToAddressCommand = new Command(NavigateToAdress); // Command to navigate to address view
        AddToCartCommand = new Command<string>(async (productId) => await AddToCart(productId, 1)); // Command to add product to cart
    }

    // Navigate to Categories view
    private void NavigateToCategories()
    {
        Application.Current.MainPage.Navigation.PushAsync(new Categories());
    }

    // Navigate to FilteredCategories view with selected category
    private async void NavigateToFilteredCategories(string categoryName)
    {
        var filteredCategoriesPage = new FilteredCategories();
        var viewModel = (FilteredCategoriesViewModel)filteredCategoriesPage.BindingContext;
        viewModel.Products = _products;

        // Ensure categories are loaded before setting the selected category
        await viewModel.LoadCategoriesAsync();
        viewModel.SelectedCategory = viewModel.Categories.FirstOrDefault(c => c.Name == categoryName);

        // Set IsSelected for the chosen category
        foreach (var cat in viewModel.Categories)
        {
            cat.IsSelected = cat.Name == categoryName;
        }

        Application.Current.MainPage.Navigation.PushAsync(filteredCategoriesPage);
    }

    private void NavigateToAdress()
    {
        Application.Current.MainPage.Navigation.PushAsync(new AddressListView());
    }

    // Navigate to Products view
    private void NavigateToProducts()
    {
        Application.Current.MainPage.Navigation.PushAsync(new PopularProducts());
    }

    // Load products from API
    private async void LoadProducts()
    {
        try
        {
            IsLoading = true;
            using (HttpClient httpClient = new HttpClient())
            {
                string apiUrl = "https://myowndomain.lol:5001/api/product/get"; // Your API URL
                var response = await httpClient.GetStringAsync(apiUrl);
                Products = JsonConvert.DeserializeObject<ObservableCollection<ProductsModel>>(response);
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

    // Load categories from API
    private async void LoadCategories()
    {
        try
        {
            IsLoading = true;
            using (HttpClient httpClient = new HttpClient())
            {
                string apiUrl = "https://myowndomain.lol:5001/api/categories/get"; // Your API URL
                var response = await httpClient.GetStringAsync(apiUrl);
                var allCategories = JsonConvert.DeserializeObject<ObservableCollection<CategoriesModel>>(response);
                Categories = new ObservableCollection<CategoriesModel>(allCategories.Take(5)); // Limit to 5 items
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error fetching categories: " + ex.Message); // Handle any exceptions
        }
        finally
        {
            IsLoading = false;
        }
    }

    // Add product to cart
    private async Task AddToCart(string productId, int amount)
    {
        if (string.IsNullOrEmpty(productId))
        {
            Console.WriteLine("Error: productId is null or empty"); // Log error if productId is null or empty
            return;
        }

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

    // Notify property changed
    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
