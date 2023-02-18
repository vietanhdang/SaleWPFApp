using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            var navigationService = ServiceProvider.GetRequiredService<NavigationService>();
            var task = navigationService.ShowAsync<Login>();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<NavigationService>();
            services.AddTransient<Login>();
            services.AddTransient<MemberManagement>();
            services.AddTransient<OrderManagement>();
            services.AddTransient<ProfileManagement>();
            services.AddTransient<ProductManagement>();
            services.AddScoped<IMemberRepository, MemberRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }
    }
}
