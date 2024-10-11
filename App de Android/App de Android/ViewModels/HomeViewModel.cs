using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using App_de_Android.Models;

public class HomeViewModel : INotifyPropertyChanged
{
    private List<HomeModel> _products;
    private bool _isLoading;

    public List<HomeModel> Products
    {
        get { return _products; }
        set
        {
            _products = value;
            OnPropertyChanged(nameof(Products));
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
    }

    private async void LoadProducts()
    {
        try
        {
            IsLoading = true;
            using (HttpClient httpClient = new HttpClient())
            {
                string apiUrl = "https://myowndomain.lol:5001/api/product/get";
                var response = await httpClient.GetStringAsync(apiUrl);
                Products = JsonConvert.DeserializeObject<List<HomeModel>>(response);
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

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
