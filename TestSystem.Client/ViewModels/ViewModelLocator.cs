using Microsoft.Extensions.DependencyInjection;

namespace TestSystem.Client.ViewModels
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowVM => App.Services.GetRequiredService<MainWindowViewModel>();
        public LoadImageViewModel LoadImageVM => App.Services.GetRequiredService<LoadImageViewModel>();
    }
}
