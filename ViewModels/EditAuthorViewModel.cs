using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PKS_Library.Factories;
using PKS_Library.Models;
using PKS_Library.Services.Interfaces;
using PKS_Library.Services.Realisations;
using PKS_Library.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.ViewModels;

public partial class EditAuthorViewModel : PageViewModel
{
    private readonly IAuthorService _authorService;

    private readonly NavigationService _navigationService;

    private readonly PageViewModelFactory _factory;

    private Author _selectedAuthor = new();

    #region Поля формы
    [ObservableProperty]
    private string _firstName = string.Empty;

    [ObservableProperty]
    private string _lastName = string.Empty;

    [ObservableProperty]
    private string _birthdate = string.Empty;

    [ObservableProperty]
    private string _country = string.Empty;
    #endregion

    #region Ошибки
    [ObservableProperty]
    private string _firstNameError = string.Empty;

    [ObservableProperty]
    private string _lastNameError = string.Empty;

    [ObservableProperty]
    private string _birthdateError = string.Empty;

    [ObservableProperty]
    private string _countryError = string.Empty;

    [ObservableProperty]
    private string _fatalError = string.Empty;

    private bool _areErrorsExist = false;
    #endregion

    public EditAuthorViewModel(IAuthorService authorService, NavigationService navigationService, PageViewModelFactory factory)
    {
        PageName = Data.PageName.AuthorEdit;

        _authorService = authorService;
        _navigationService = navigationService;
        _factory = factory;
    }

    public void SetAuthor(Author author)
    {
        _selectedAuthor = author;

        FirstName = author.FirstName;
        LastName  = author.LastName;
        Birthdate = author.Birthdate.ToString();
        Country   = author.Country;
    }

    [RelayCommand]
    public void GoToAuthorsPage()
    {
        var authorsPage = _factory.GetPageViewModel(Data.PageName.Authors);

        _navigationService.NavigateTo(authorsPage);
    }

    [ObservableProperty]
    private string _successMessage = string.Empty;

    [RelayCommand]
    private async Task UpdateAuthor()
    {
        var authorValidator = new AuthorPageValidator();
        var validationResult = authorValidator.Validate(new Data.AuthorValidationRequest(FirstName, LastName, Birthdate, Country));

        if (!validationResult.IsValid)
        {
            SetErrors(validationResult);
            return;
        }

        if(_areErrorsExist)
        {
            ClearErrors();
        }

        try
        {
            _selectedAuthor.FirstName = FirstName;
            _selectedAuthor.LastName  = LastName;
            _selectedAuthor.Birthdate = DateOnly.Parse(Birthdate);
            _selectedAuthor.Country   = Country;

            await _authorService.UpdateAuthorAsync(_selectedAuthor);

            SuccessMessage = "Данные успешно обновлены";
        }
        catch (Exception ex)
        {
            FatalError = ex.Message;
        }
    }

    private void SetErrors(FluentValidation.Results.ValidationResult validationResult)
    {
        _areErrorsExist = true;
        
        ClearErrors(); 

        foreach (var error in validationResult.Errors)
        {
            switch (error.PropertyName)
            {
                case nameof(Author.FirstName):
                    FirstNameError = error.ErrorMessage;
                    break;
                case nameof(Author.LastName):
                    LastNameError = error.ErrorMessage;
                    break;
                case nameof(Author.Birthdate):
                    BirthdateError = error.ErrorMessage;
                    break;
                case nameof(Author.Country):
                    CountryError = error.ErrorMessage;
                    break;
            }
        }
    }

    private void ClearErrors()
    {
        FirstNameError = string.Empty;
        LastNameError  = string.Empty;
        BirthdateError = string.Empty;
        CountryError   = string.Empty;
        FatalError     = string.Empty;
    }
}
