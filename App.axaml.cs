using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using PKS_Library.Data;
using PKS_Library.Factories;
using PKS_Library.Models;
using PKS_Library.Repositories.Interfaces;
using PKS_Library.Repositories.Realisations;
using PKS_Library.Services.Interfaces;
using PKS_Library.Services.Realisations;
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
                      .AddSingleton<NavigationService>()
                      .AddSingleton<IBookRepository, BookRepository>()
                      .AddSingleton<IAuthorRepository, AuthorRepository>()
                      .AddSingleton<IGenreRepository, GenreRepository>()
                      .AddSingleton<IBookService, BookService>()
                      .AddSingleton<IAuthorService, AuthorService>()
                      .AddSingleton<IGenreService, GenreService>()
                      .AddTransient<AllBooksViewModel>()
                      .AddTransient<BookEditViewModel>()
                      .AddTransient<AddBookViewModel>()
                      .AddTransient<AllAuthorsViewModel>()
                      .AddTransient<AuthorEditViewModel>()
                      .AddTransient<AllGenresViewModel>()
                      .AddTransient<GenreEditViewModel>()
                      .AddSingleton<Func<PageName, PageViewModel>>(x => name => name switch
                      {
                          PageName.Books      => x.GetRequiredService<AllBooksViewModel>(),
                          PageName.BookEdit   => x.GetRequiredService<BookEditViewModel>(),
                          PageName.BookAdd    => x.GetRequiredService<AddBookViewModel>(),
                          PageName.Authors    => x.GetRequiredService<AllAuthorsViewModel>(),
                          PageName.AuthorEdit => x.GetRequiredService<AuthorEditViewModel>(),
                          PageName.Genres     => x.GetRequiredService<AllGenresViewModel>(),
                          PageName.GenreEdit  => x.GetRequiredService<GenreEditViewModel>(),
                          _ => throw new NotImplementedException()
                      })
                      .AddSingleton<PageViewModelFactory>();

            var services = collection.BuildServiceProvider();

            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                DisableAvaloniaDataAnnotationValidation();
                desktop.MainWindow = new MainWindow
                {
                    DataContext = services.GetRequiredService<MainWindowViewModel>(),
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