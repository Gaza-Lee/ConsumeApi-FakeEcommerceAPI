using ConsumeApi.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConsumeApi.ViewModels
{
    public class ProductViewModel : INotifyPropertyChanged
    {
        private readonly Product _product;
        private int _productCount;
        public ICommand IncreaseProductCommand { get; }
        public ICommand DecreaseProductCommand { get; }

        public string Title => _product.Title;
        public decimal Price => _product.Price;
        public string Description => _product.Description;
        public string Category => _product.Category;
        public string Image => _product.Image;

        public int ProductCount
        {
            get => _productCount;
            set
            {
                if (_productCount != value)
                {
                    _productCount = value;
                    OnPropertyChanged();
                    ((Command)DecreaseProductCommand).ChangeCanExecute();
                }
            }
        }

        private void IncreaseProductCount() => ProductCount++;
        private void DecreaseProductCount() => ProductCount--;
        private bool CanDecreaseProductCount() => ProductCount > 0;


        public ProductViewModel(Product product)
        {
            _product = product;
            _productCount = 0;
            IncreaseProductCommand = new Command(IncreaseProductCount);
            DecreaseProductCommand = new Command(DecreaseProductCount, CanDecreaseProductCount);
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
