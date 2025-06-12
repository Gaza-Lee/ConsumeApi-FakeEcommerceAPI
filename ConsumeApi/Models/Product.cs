using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ConsumeApi.Models
{
    public partial class Product :ObservableObject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }


        [ObservableProperty]
        private int _productCount;

        public IRelayCommand IncreaseProductCommand { get; }
        public IRelayCommand DecreaseProductCommand { get; }

        public Product()
        {
            IncreaseProductCommand = new RelayCommand(IncreaseProductCount);
            DecreaseProductCommand = new RelayCommand(DecreaseProductCount, CanDecreaseProductCount);
        }

        private void IncreaseProductCount() => ProductCount++;
        private void DecreaseProductCount() => ProductCount--;
        private bool CanDecreaseProductCount() => ProductCount > 0;
    }
}
