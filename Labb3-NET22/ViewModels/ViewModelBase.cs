using Labb3_NET22.Managers;

namespace Labb3_NET22.ViewModels;
using Microsoft.Toolkit.Mvvm.ComponentModel;

class ViewModelBase:ObservableObject
{
    private readonly NavigationManager _navigationManager;
    private readonly QuizManager _quizManager;

    public ObservableObject CurrentViewModel => _navigationManager.CurrentViewModel;

    public ViewModelBase(NavigationManager navigationManager, QuizManager quizManager)
    {
        _navigationManager = navigationManager;
        _quizManager = quizManager;
        _navigationManager.CurrentViewModelChanged += CurrentViewModelChanged;
    }


    private void CurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }

}