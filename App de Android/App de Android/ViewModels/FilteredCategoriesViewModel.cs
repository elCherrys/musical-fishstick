using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using App_de_Android.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Linq;
using System.Windows.Input;
using System.Net.Http.Headers;
using Xamarin.Essentials;

public class FilteredCategoriesViewModel : INotifyPropertyChanged
{
    private ObservableCollection<CategoriesModel> _categories; // Observable collection of categories
    private ObservableCollection<ProductsModel> _products; // Observable collection of products
    private ObservableCollection<ProductsModel> _selectedCategoryProducts; // Products for the selected category
    private CategoriesModel _selectedCategory; // Currently selected category

    // Public properties
    public ObservableCollection<CategoriesModel> Categories
    {
        get { return _categories; }
        set { _categories = value; OnPropertyChanged(nameof(Categories)); }
    }

    public ObservableCollection<ProductsModel> Products
    {
        get { return _products; }
        set { _products = value; OnPropertyChanged(nameof(Products)); }
    }

    public ObservableCollection<ProductsModel> SelectedCategoryProducts
    {
        get { return _selectedCategoryProducts; }
        set { _selectedCategoryProducts = value; OnPropertyChanged(nameof(SelectedCategoryProducts)); }
    }

    public CategoriesModel SelectedCategory
    {
        get { return _selectedCategory; }
        set
        {
            _selectedCategory = value;
            OnPropertyChanged(nameof(SelectedCategory));
            FilterProductsByCategory(); // Filter products when category changes
        }
    }

    // Command to handle category selection
    public ICommand SelectCategoryCommand { get; private set; }
    public ICommand AddToCartCommand { get; private set; } // Command to add product to cart

    // Constructor
    public FilteredCategoriesViewModel()
    {
        SelectCategoryCommand = new Command<CategoriesModel>(SelectCategory);
        AddToCartCommand = new Command<string>(async (productId) => await AddToCart(productId, 1)); // Command to add product to cart
        LoadProducts(); // Load products from API
    }

    // Async method to load categories from API
    public async Task LoadCategoriesAsync()
    {
        try
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string apiUrl = "https://myowndomain.lol:5001/api/categories/get"; // Your API URL
                var response = await httpClient.GetStringAsync(apiUrl);
                Categories = JsonConvert.DeserializeObject<ObservableCollection<CategoriesModel>>(response); // Load all categories
                // Ensure a default category is selected if none is passed
                if (SelectedCategory == null && Categories.Any())
                {
                    SelectedCategory = Categories.First();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error fetching categories: " + ex.Message); // Handle any exceptions
        }
    }

    // Load products from API
    private async void LoadProducts()
    {
        try
        {
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
                FilterProductsByCategory(); // Ensure the filtering happens
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error fetching products: " + ex.Message); // Handle any exceptions
        }
    }

    // Handle category selection
    private void SelectCategory(CategoriesModel category)
    {
        foreach (var cat in Categories)
        {
            cat.IsSelected = false; // Reset all categories to not selected
        }
        category.IsSelected = true; // Set the selected category
        SelectedCategory = category;
    }

    // Filter products based on selected category
    private void FilterProductsByCategory()
    {
        if (SelectedCategory != null)
        {
            SelectedCategoryProducts = new ObservableCollection<ProductsModel>(
                Products.Where(p => p.categoryName == SelectedCategory.Name)); // Ensure the field name matches
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

    public event PropertyChangedEventHandler PropertyChanged;
}
