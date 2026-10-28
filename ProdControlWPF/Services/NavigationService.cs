using System;
using System.Windows.Controls;

namespace ProdControlWPF.Services
{
    public sealed class NavigationService : INavigationService
    {
        private ContentControl _host;
        private object _currentViewModel;

        public void SetHost(ContentControl host)
        {
            _host = host ?? throw new ArgumentNullException(nameof(host));

            if (_currentViewModel != null)
            {
                _host.Content = _currentViewModel;
            }
        }

        public void Navigate(object viewModel)
        {
            _currentViewModel = viewModel ?? throw new ArgumentNullException(nameof(viewModel));

            if (_host != null)
            {
                _host.Content = _currentViewModel;
            }
        }
    }
}
