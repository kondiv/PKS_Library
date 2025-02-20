using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using PKS_Library.Data;
using PKS_Library.Models;
using PKS_Library.Repositories.Interfaces;
using PKS_Library.Repositories.Realisations;
using PKS_Library.ViewModels;
using PKS_Library.Views;

namespace PKS_Library
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            var collection = new ServiceCollection();

            collection.AddDbContext<PksBooksContext>();

            collection.AddSingleton<MainWindowViewModel>()
                      .AddSingleton<IBookRepository, BookRepository>()
                      .AddTransient<AllBooksViewModel>()
                      .AddSingleton<Func<PageName, PageViewModel>>(x => name => name switch
                      {
                          PageName.Books => x.GetRequiredService<AllBooksViewModel>(),
                          _ => throw new NotImplementedException()
                      });

            var services = collection.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                DisableAvaloniaDataAnnotationValidation();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        private void DisableAvaloniaDataAnnotationValidation()
        {
            // Get an array of plugins to remove
            var dataValidationPluginsToRemove =
                BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

            // remove each entry found
            foreach (var plugin in dataValidationPluginsToRemove)
            {
                BindingPlugins.DataValidators.Remove(plugin);
            }
        }
    }
}