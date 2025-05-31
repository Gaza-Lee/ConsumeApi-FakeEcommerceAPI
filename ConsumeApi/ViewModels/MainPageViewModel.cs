using ConsumeApi.APIService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsumeApi.Models;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

namespace ConsumeApi.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly StoreApi _apiService;
        private ProductCategory _selectedCategory;
        private List<Product> _products;
        private bool _isLoading;

        public ObservableCollection<ProductCategory> Categories { get; set; }
        public ObservableCollection<Product> FilteredProducts { get; set; }
        public ObservableCollection<Offer> Offers { get; set; }


        public MainPageViewModel()
        {
            _apiService = new StoreApi();
            _products = new List<Product>();
            Offers = new ObservableCollection<Offer>(Offer.GetOffers());
            Categories = new ObservableCollection<ProductCategory>();
            FilteredProducts = new ObservableCollection<Product>();

        }


        public List<Product> Products
        {
            get => _products;
            set
            {
                _products = value;
                OnPropertyChanged();
            }
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

        public ProductCategory SelectedCategory
        {
            get => _selectedCategory;
            set
            {                
                    _selectedCategory = value;
                    OnPropertyChanged();
                    ApplyCategoryFilter();
            }
        }

        private void ApplyCategoryFilter()
        {
            if (SelectedCategory == null || Products == null)
                return;
            var filtered = Products
                .Where(p => p.Category == SelectedCategory.CategoryName).ToList();

           FilteredProducts.Clear();
            foreach (var product in filtered)
            {
                FilteredProducts.Add(product);
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

                    //Extract Uniques Categories from Products
                    var uniqueCategories =
                        Products.Select(p => p.Category).Distinct().Select(c => new ProductCategory { CategoryName = c });

                    Categories.Clear();
                    foreach (var category in uniqueCategories)
                    {
                        Categories.Add(category);
                    }

                    if (Categories.Any())
                    {
                        // Set the first category as selected by default
                        SelectedCategory = Categories.First();
                    }
                    else
                    {
                        SelectedCategory = null;
                    }

                    //Filter Products by Category
                    FilteredProducts.Clear();
                    foreach (var product in Products)
                    {
                        if (product.Category == SelectedCategory?.CategoryName)
                        {
                            FilteredProducts.Add(product);
                        }
                    }
                }

                Offers.Clear();
                foreach (var offers in Offer.GetOffers())
                {
                    Console.WriteLine($"Offer: {offers.Title}, Description: {offers.Description}, Coupon: {offers.CouponCode}");
                    Offers.Add(offers);

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions (e.g., log them, show a message to the user, etc.)
                Console.WriteLine($"Error loading products: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
