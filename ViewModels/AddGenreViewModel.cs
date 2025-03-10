using Avalonia.Animation.Easings;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.Extensions.Options;
using PKS_Library.Builders.Realisations;
using PKS_Library.Data;
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

public partial class AddGenreViewModel : PageViewModel
{
    private readonly IGenreService _genreService;

    private readonly PageViewModelFactory _factory;

    private readonly NavigationService _navigationService;

    public AddGenreViewModel(IGenreService genreService, PageViewModelFactory factory, NavigationService navigationService)
    {
        PageName = Data.PageName.GenreAdd;
        
        _genreService = genreService;
        _factory = factory;
        _navigationService = navigationService;
    }

    [ObservableProperty]
    private string _successMessage = string.Empty;

    //поля
    [ObservableProperty]
    private string _name = string.Empty;

    [ObservableProperty]
    private string _description = string.Empty;

    //ошибки
    [ObservableProperty]
    private string _nameError = string.Empty;

    [ObservableProperty]
    private string _descriptionError = string.Empty;

    [ObservableProperty]
    private string _fatalError = string.Empty;

    private bool _areErrorsExist = false;

    [RelayCommand]
    private void OpenAllGenresPage()
    {
        var allGenresPage = _factory.GetPageViewModel(Data.PageName.Genres);

        _navigationService.NavigateTo(allGenresPage);
    }

    [RelayCommand]
    private async Task CreateGenre()
    {
        var genreValidator = new GenrePageValidator();

        var validationResult = genreValidator.Validate(new GenreValidationRequest(Name, Description));

        if (!validationResult.IsValid)
        {
            SetErrors(validationResult);
            return;
        }

        if(_areErrorsExist)
        {
            ClearErrors();
        }

        var genre = BuildGenre();

        try
        {
            await _genreService.CreateGenreAsync(genre);
            SuccessMessage = "Жанр успешно создан";
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
                case nameof(Name):
                    NameError = error.ErrorMessage;
                    break;
                case nameof(Description):
                    DescriptionError = error.ErrorMessage;
                    break;
            }
        }
    }

    private void ClearErrors()
    {
        NameError = string.Empty;
        DescriptionError = string.Empty;
        FatalError = string.Empty;
    }

    private Genre BuildGenre()
    {
        var genreBuilder = new GenreBuilder();

        genreBuilder.SetName(Name)
            .SetDescription(Description);

        return genreBuilder.Build();
    }
}
