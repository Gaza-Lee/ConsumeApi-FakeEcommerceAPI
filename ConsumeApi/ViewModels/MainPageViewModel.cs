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

        private string GetCategoryImage(string categoryName)
        {
            return categoryName.ToLower() switch
            {
                "electronics" => "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Ftse4.mm.bing.net%2Fth%3Fid%3DOIP.FIr8tkgvJyAjm-o8sEh0AwHaEC%26pid%3DApi&f=1&ipt=2efab630c86e68de759ab66ad887891a1cf6bb6b5dbff70447b143598ba8c9a7",
                "jewelery" => "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fstatic.vecteezy.com%2Fsystem%2Fresources%2Fpreviews%2F021%2F836%2F736%2Flarge_2x%2Fluxury-jewelry-abstract-background-ai-generated-photo.jpg&f=1&nofb=1&ipt=4e64eb2262e96ce693f9df910dd9fe911bc7495f16a13dbb6962f2c70c5d61e8",
                "men's clothing" => "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fmedia.istockphoto.com%2Fphotos%2Fsuitcase-with-mens-clothings-isolated-on-white-background-picture-id1012579136%3Fk%3D6%26m%3D1012579136%26s%3D612x612%26w%3D0%26h%3DomhsOXIz-pqmrx280-FjYGfN1FWTYWGj9Fk348lj1eA%3D&f=1&nofb=1&ipt=4db00de42ea0f34ce0cb5c24e7f7d57859582c5f5f033116e3575c109d188fe6",
                "women's clothing" => "https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fassets.simpleviewinc.com%2Fsimpleview%2Fimage%2Fupload%2Fc_limit%2Cq_75%2Cw_1200%2Fv1%2Fcrm%2Feurekaca%2FB63FE394-D2A0-4871-8AE3-53C3001AA809_FC1CA812-5056-A36A-096BEC39DD4EC30D-fc1ca4e25056a36_fc1cb488-5056-a36a-093829553cd2ed96.jpg&f=1&nofb=1&ipt=f614ebad555f8305246aa2958e11e8982d8bb7f3c2e77d28b813848a503a679e",
                
            };
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
                        Products.Select(p => p.Category).Distinct().Select(c => new ProductCategory 
                        { 
                            CategoryName = c,
                            Image = GetCategoryImage(c) 
                        });

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

                    Offers.Clear();
                    foreach (var offers in Offer.GetOffers())
                    {
                        Console.WriteLine($"Offer: {offers.Title}, Description: {offers.Description}, Coupon: {offers.CouponCode}");
                        Offers.Add(offers);

                    }
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
