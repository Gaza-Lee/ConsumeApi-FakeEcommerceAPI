using ConsumeApi.ViewModels;

namespace ConsumeApi.Views;

public partial class MainPage : ContentPage
{
	private readonly MainPageViewModel _viewModel;
    public MainPage(MainPageViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		_viewModel = viewModel;
    }

	protected override async void OnAppearing()
	{
		base.OnAppearing();

		if (_viewModel != null && !_viewModel.IsLoading)
		{
            await _viewModel.LoadProductsAsync();
        }
    }
}