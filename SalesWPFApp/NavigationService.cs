using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SalesWPFApp
{
    public interface IActivable
    {
        Task ActivateAsync(object parameter);
    }

    /// <summary>
    /// Navigation service. It is used to navigate between windows.
    /// </summary>
    public class NavigationService
    {
        private readonly IServiceProvider serviceProvider;

        public NavigationService(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Show a dialog window
        /// </summary>
        public async Task<bool?> ShowDialogAsync<T>(object parameter = null) where T : Window
        {
            var window = serviceProvider.GetRequiredService<T>();
            if (window is IActivable activableWindow)
            {
                await activableWindow.ActivateAsync(parameter);
            }
            return window.ShowDialog();
        }

        /// <summary>
        /// Show a window
        /// </summary>
        public async Task ShowAsync<T>(object parameter = null) where T : Window
        {
            var window = serviceProvider.GetRequiredService<T>();
            if (window is IActivable activableWindow)
            {
                await activableWindow.ActivateAsync(parameter);
            }

            window.Show();
        }
    }
}
