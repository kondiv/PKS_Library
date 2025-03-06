using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation.Results;
using PKS_Library.Builders.Realisations;
using PKS_Library.Factories;
using PKS_Library.Models;
using PKS_Library.Services.Interfaces;
using PKS_Library.Services.Realisations;
using PKS_Library.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PKS_Library.ViewModels;

public partial class AddAuthorViewModel : PageViewModel
{
    private readonly IAuthorService _authorService;
    
    private readonly NavigationService _navigationService;

    private readonly PageViewModelFactory _factory;

    public AddAuthorViewModel(IAuthorService authorService, NavigationService navigationService, PageViewModelFactory factory)
    {
        PageName = Data.PageName.AuthorAdd;

        _authorService = authorService;
        _navigationService = navigationService;
        _factory = factory;
    }

    //поля
    [ObservableProperty]
    private string _firstName = string.Empty;

    [ObservableProperty]
    private string _lastName = string.Empty;

    [ObservableProperty]
    private string _birthdate = string.Empty;

    [ObservableProperty]
    private string _country = string.Empty;

    //ошибки
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

    [RelayCommand]
    private void GoToAuthorsPage()
    {
        _navigationService.NavigateTo(_factory.GetPageViewModel(Data.PageName.Authors));
    }

    [ObservableProperty]
    private string _succededAction = string.Empty;

    [RelayCommand]
    private async Task CreateAuthor()
    {
        var authorValidator = new AuthorPageValidator();
        var validationResult = authorValidator.Validate(new Data.AuthorValidationRequest(FirstName, LastName, Birthdate, Country));

        if(!validationResult.IsValid)
        {
            SetErrors(validationResult);
            return;
        }

        if(_areErrorsExist)
        {
            ClearErrors();
        }

        var author = BuildAuthor();
        try
        {
            await _authorService.AddAuthorAsync(author);
            SuccededAction = "Автор добавлен";
        }
        catch(Exception ex)
        {
            FatalError = ex.Message;
        }
    }

    private void SetErrors(FluentValidation.Results.ValidationResult validationResult)
    {
        _areErrorsExist = true;

        foreach (var error in validationResult.Errors)
        {
            switch (error.PropertyName)
            {
                case nameof(FirstName):
                    FirstNameError = error.ErrorMessage;
                    break;
                case nameof(LastName):
                    LastNameError = error.ErrorMessage;
                    break;
                case nameof(Birthdate):
                    BirthdateError = error.ErrorMessage;
                    break;
                case nameof(Country):
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

    private Author BuildAuthor()
    {
        var authorBuilder = new AuthorBuilder();

        authorBuilder.SetFirstName(FirstName)
            .SetLastName(LastName)
            .SetBirthdate(DateOnly.Parse(Birthdate))
            .SetCountry(Country);

        return authorBuilder.Build();
    }
}
