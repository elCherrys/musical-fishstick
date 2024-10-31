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

public class HomeViewModel : INotifyPropertyChanged
{
    private ObservableCollection<HomeModel> _products;
    private ObservableCollection<CategoriesModel> _categories;
    public ICommand NavigateToCategoriesCommand { get; private set; }
    public ICommand NavigateToProductsCommand { get; private set; }
    private bool _isLoading;

    public ObservableCollection<HomeModel> Products
    {
        get { return _products; }
        set
        {
            _products = value;
            Console.WriteLine($"Products updated: {Products.Count} items."); // Debug output
            OnPropertyChanged(nameof(Products));
        }
    }

    public ObservableCollection<CategoriesModel> Categories
    {
        get { return _categories; }
        set
        {
            _categories = value;
            OnPropertyChanged(nameof(Categories));
        }
    }


    public bool IsLoading
    {
        get { return _isLoading; }
        set
        {
            _isLoading = value;
            OnPropertyChanged(nameof(IsLoading));
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;

    public HomeViewModel()
    {
        LoadProducts();
        LoadCategories();
        NavigateToCategoriesCommand = new Command(NavigateToCategories);
        NavigateToProductsCommand = new Command(NavigateToProducts);
    }

    private void NavigateToCategories()
    {
        // Navigate to Categories view
        Application.Current.MainPage.Navigation.PushAsync(new Categories());
    }

    private void NavigateToProducts()
    {
        // Navigate to Products view
        Application.Current.MainPage.Navigation.PushAsync(new PopularProducts());
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
                Products = JsonConvert.DeserializeObject<ObservableCollection<HomeModel>>(response);
            }
        }
        catch (Exception ex)
        {
            // Handle any exceptions (network errors, parsing errors, etc.)
            Console.WriteLine("Error fetching products: " + ex.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }

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
            Console.WriteLine("Error fetching categories: " + ex.Message);
        }
        finally
        {
            IsLoading = false;
        }
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
