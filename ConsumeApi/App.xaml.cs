using ConsumeApi.ViewModels;
using ConsumeApi.Views;

namespace ConsumeApi
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage(new ProductViewModel());
        }
    }
}
