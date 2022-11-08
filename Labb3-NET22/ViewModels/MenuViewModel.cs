using Labb3_NET22.DataModels;
using Labb3_NET22.Managers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;

namespace Labb3_NET22.ViewModels;

class MenuViewModel:ObservableObject
{
        private NavigationManager _navigationManager;
        private readonly QuizManager _quizManager;



    public RelayCommand NavigatePlayCommand { get; }
    public RelayCommand NavigateCreateCommand { get; }

        public MenuViewModel(NavigationManager navigationManager, QuizManager quizManager)
        {
            _navigationManager = navigationManager;
            NavigatePlayCommand = new RelayCommand(() => _navigationManager.CurrentViewModel = new PlayViewModel(_navigationManager, _quizManager));
            NavigateCreateCommand = new RelayCommand(() => _navigationManager.CurrentViewModel = new CreateViewModel(_navigationManager, _quizManager));
        }

}