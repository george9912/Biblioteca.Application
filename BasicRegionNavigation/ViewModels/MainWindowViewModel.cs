using Biblioteca.WPF.API.Client;
using Bibliotecs.WPF.ModuleA;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace BasicRegionNavigation.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private readonly IRegionManager _regionManager;
        IEventAggregator ea;
        bool isEnabled;
        bool isEnabledBooks;

        private string _title = "Library application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public DelegateCommand<string> NavigateCommand { get; private set; }

        public MainWindowViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            IsEnabled = false;
            IsEnabledBooks = false;
            this.ea = ea;
            ea.GetEvent<MessageSentEvent>().Subscribe(MessageReceived);
        }

        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }
        private void MessageReceived(string message)
        {
            if (message == "Administrator") {
                IsEnabled = true;
                IsEnabledBooks = true;
            }
            else  
            {
                IsEnabled = false;
                IsEnabledBooks = true;
            }
        }

        public bool IsEnabled
        {
            get { return isEnabled; }
            set { SetProperty(ref isEnabled, value); }
        }
        public bool IsEnabledBooks
        {
            get { return isEnabledBooks; }
            set { SetProperty(ref isEnabledBooks, value); }
        }

    }
}
