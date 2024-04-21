using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TestSystem.Client.Commands;
using TestSystem.Client.Services.Interfaces;
using TestSystem.Client.ViewModels.Base;
using TestSystem.Core.Models;

namespace TestSystem.Client.ViewModels
{
    public class LoadImageViewModel : ViewModel
    {
        private readonly IDataService<ImageWithText> _dataService;
        private readonly MainWindowViewModel _mainWindowViewModel;

        private string _title = "Загрузка";
        public string Title { get => _title; set => Set(ref _title, value); }

        private string _imagePath;
        public string ImagePath { get => _imagePath; set => Set(ref _imagePath, value); }

        private string _associatedText;
        public string AssociatedText { get => _associatedText; set => Set(ref _associatedText, value); }

        #region Command ChooseImageCommand

        private ICommand _chooseImageCommand;
        public ICommand ChooseImageCommand => _chooseImageCommand
            ??= new LambdaCommand(OnChooseImageCommandExecuted, CanChooseImageCommandExecute);

        private bool CanChooseImageCommandExecute(object p) => true;
        private void OnChooseImageCommandExecuted(object p)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Image files (*.jpg)|*.jpg|All files (*.*)|*.*";
            dialog.RestoreDirectory = true;
            dialog.InitialDirectory = Environment.CurrentDirectory;
            dialog.ShowDialog();
            try
            {
                ImagePath = System.IO.Path.GetFullPath(dialog.FileName);
            }
            catch
            {
                ImagePath = string.Empty;
            }
        }
        #endregion

        #region Command SendImageCommandAsync

        private ICommand _sendImageCommandAsync;
        public ICommand SendImageCommandAsync => _sendImageCommandAsync
            ??= new LambdaCommandAsync(OnSendImageCommandExecutedAsync, SendImageCommandExecute);

        private bool SendImageCommandExecute(object p) => !string.IsNullOrEmpty(ImagePath);
        private async Task OnSendImageCommandExecutedAsync(object p)
        {
            var img = new ImageWithText { Id = Guid.NewGuid(), FilePath = ImagePath, AssociatedText = AssociatedText };
            try
            {
                var newImage = await _dataService.SendDataAsync(img);
                MessageBox.Show("Отправлено", "Статус", MessageBoxButton.OK, MessageBoxImage.Information);
                _mainWindowViewModel.ImagesWithText.Add(newImage);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Статус", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                App.ActivedWindow.Close();
            }
        }
        #endregion


        public LoadImageViewModel(IDataService<ImageWithText> dataService, MainWindowViewModel mainWindowViewModel)
        {
            _dataService = dataService;
            _mainWindowViewModel = mainWindowViewModel;
        }
    }
}
