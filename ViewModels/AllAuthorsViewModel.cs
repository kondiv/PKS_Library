using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PKS_Library.CustomExceptions;
using PKS_Library.Factories;
using PKS_Library.Models;
using PKS_Library.Services.Interfaces;
using PKS_Library.Services.Realisations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.ViewModels
{
    public partial class AllAuthorsViewModel : PageViewModel
    {
        private readonly IAuthorService _authorService;

        private readonly NavigationService _navigationService;

        private readonly PageViewModelFactory _factory;

        [ObservableProperty]
        private ObservableCollection<Author> _authors = [];

        [ObservableProperty]
        private string _infoMessage = string.Empty;

        public AllAuthorsViewModel(IAuthorService authorService, NavigationService navigationService, PageViewModelFactory factory)
        {
            PageName = Data.PageName.Authors;

            _authorService = authorService;
            _navigationService = navigationService;
            _factory = factory;

            LoadAuthorsAsync().ConfigureAwait(false);
        }

        private async Task LoadAuthorsAsync()
        {
            var authors = await _authorService.GetAllAuthorsAsync();
            Authors = new ObservableCollection<Author>(authors);
        }

        [RelayCommand]
        public void OpenEditAuthorPage(Author author)
        {
            var authorEditPage = _factory.GetPageViewModel(Data.PageName.AuthorEdit) as EditAuthorViewModel ??
                throw new PageDoesNotExistException("Не удалось открыть страницу редактирования автора");

            authorEditPage.SetAuthor(author);

            _navigationService.NavigateTo(authorEditPage);
        }

        [RelayCommand]
        private void OpenAddAuthorPage()
        {
            _navigationService.NavigateTo(_factory.GetPageViewModel(Data.PageName.AuthorAdd));
        }

        [RelayCommand]
        private async Task DeleteAuthor(Author author)
        {
            if (author == null)
            {
                await ShowMessage("Автор не выбран");
                return;
            }

            try
            {
                await _authorService.DeleteAuthorAsync(author.AuthorId);
                Authors.Remove(author);
                await ShowMessage("Автор удален");
            }

            catch (Exception ex)
            {
                await ShowMessage(ex.Message);
            }
        }

        private async Task ShowMessage(string message)
        {
            InfoMessage = message;

            await Task.Delay(1500);

            InfoMessage = string.Empty;
        }
    }
}
