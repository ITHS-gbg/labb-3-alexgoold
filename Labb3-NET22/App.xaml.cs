using Labb3_NET22.Managers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Labb3_NET22.ViewModels;
using Labb3_NET22.Views;

namespace Labb3_NET22
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly NavigationManager _navigationManager;
        private QuizManager _quizManager;


        public App()
        {
            _navigationManager = new NavigationManager();

        }


        protected override void OnStartup(StartupEventArgs e)
        {
            _quizManager = new QuizManager();
            _navigationManager.CurrentViewModel = new MenuViewModel(_navigationManager, _quizManager);
            var rootWindow = new RootWindow { DataContext = new ViewModelBase(_navigationManager, _quizManager) };
            rootWindow.Show();
            base.OnStartup(e);
        }
    }
}
