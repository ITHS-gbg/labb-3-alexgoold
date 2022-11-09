using Labb3_NET22.Managers;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb3_NET22.ViewModels
{
     class EditViewModel: ObservableObject
    {
        private NavigationManager _navigationManager;
        private QuizManager _quizManager;

        public RelayCommand NavigateMenuCommand { get; }

        public EditViewModel(NavigationManager navigationManager, QuizManager quizManager)
        {

            _navigationManager = navigationManager;
            _quizManager = quizManager;
            NavigateMenuCommand = new RelayCommand(() => _navigationManager.CurrentViewModel = new MenuViewModel(_navigationManager, _quizManager));

        }

    }
}
