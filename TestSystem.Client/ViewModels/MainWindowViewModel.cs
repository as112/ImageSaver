using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using TestSystem.Client.Commands;
using TestSystem.Client.Services.Interfaces;
using TestSystem.Client.ViewModels.Base;
using TestSystem.Core.Models;

namespace TestSystem.Client.ViewModels
{
    public class MainWindowViewModel : ViewModel
    {
        private readonly IDataService<ImageWithText> _dataService;

        private string _title = "Client";
        public string Title { get => _title; set => Set(ref _title, value); }

        private string _status;
        public string Status { get => _status; set => Set(ref _status, value); }

        private ObservableCollection<ImageWithText> _imagesWithText;
        public ObservableCollection<ImageWithText> ImagesWithText { get => _imagesWithText; set => Set(ref _imagesWithText, value); }

        public MainWindowViewModel(IDataService<ImageWithText> dataService)
        {
            _dataService = dataService;
            ImagesWithText = new ObservableCollection<ImageWithText>();
            try
            {
                var images = _dataService.GetAllAsync().Result;
                foreach (var image in images)
                {
                    ImagesWithText.Add(image);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Статус", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
