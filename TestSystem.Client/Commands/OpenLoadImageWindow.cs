using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TestSystem.Client.Commands.Base;
using TestSystem.Client.Views;

namespace TestSystem.Client.Commands
{
    internal class OpenLoadImageWindow : Command
    {
        private LoadImageWindow _window;
        protected override bool CanExecute(object parameter) => _window == null;
        protected override void Execute(object p)
        {
            var window = new LoadImageWindow
            {
                Owner = Application.Current.MainWindow
            };
            _window = window;
            window.Closed += Window_Closed;

            window.ShowDialog();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ((Window)sender).Closed -= Window_Closed;
            _window = null;
        }
    }
}
