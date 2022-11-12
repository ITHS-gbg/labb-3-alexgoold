using Labb3_NET22.Managers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Windows.Documents;
using Labb3_NET22.DataModels;

namespace Labb3_NET22.ViewModels;
class PlayViewModel: ObservableObject
{
    private readonly NavigationManager _navigationManager;
    private QuizManager _quizManager;

    public RelayCommand NavigateMenuCommand { get; }

    public PlayViewModel(NavigationManager navigationManager, QuizManager quizManager)
    {

        _navigationManager = navigationManager;
        _quizManager = quizManager;
        NavigateMenuCommand = new RelayCommand(() => _navigationManager.CurrentViewModel = new MenuViewModel(_navigationManager, _quizManager));

    }

  
}