using ConsumeApi.APIService;
using ConsumeApi.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsumeApi.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private readonly StoreApi _apiService;
        private List<Product> _products;
        private bool _isLoading;


        public List<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
        }

        public ProductViewModel()
        {
            _apiService = new StoreApi();
            _products = new List<Product>();
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public async Task LoadProductsAsync()
        {
            try
            {
                IsLoading = true;
                string apiUrl = "https://fakestoreapi.com/products"; // Use the API URL directly
                var products = await _apiService.GetStoreDataAsync(apiUrl);

                if (products != null)
                {
                    Products = products;
                }
            }
            catch(Exception ex)
            {
                // Handle exceptions (e.g., log them, show a message to the user, etc.)
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
