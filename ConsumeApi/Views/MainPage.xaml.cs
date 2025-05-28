using ConsumeApi.ViewModels;

namespace ConsumeApi.Views;

public partial class MainPage : ContentPage
{
	private readonly ProductViewModel _viewModel;
    public MainPage(ProductViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();
		await _viewModel.LoadProductsAsync();
    }
}