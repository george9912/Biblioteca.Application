using Biblioteca.WPF.API.Client;
using Bibliotecs.WPF.ModuleA;
using GalaSoft.MvvmLight.Command;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace ModuleA.ViewModels
{
    public class ViewAViewModel : BindableBase, INotifyPropertyChanged
    {

        private readonly IRegionManager _regionManager;
        IEventAggregator ea;
        
        public DelegateCommand<string> NavigateCommand { get; private set; }
        public ViewAViewModel(IRegionManager regionManager, IEventAggregator ea)
        {
            _regionManager = regionManager;
            NavigateCommand = new DelegateCommand<string>(Navigate);
            this.ea = ea;
            EnabledCommand = new DelegateCommand(Login);
            EnabledCommandCustomer = new DelegateCommand(LoginCustomer);
        }
        public DelegateCommand EnabledCommand 
        {
            get;set;
        }
        public DelegateCommand EnabledCommandCustomer
        {
            get; set;
        }
        public void Login()
        {
            ea.GetEvent<MessageSentEvent>().Publish("Administrator");
        }
        public void LoginCustomer()
        {
            ea.GetEvent<MessageSentEvent>().Publish("Customer");
        }
        public DelegateCommand LoginCommand
        {
            get
            {
                return new DelegateCommand(Login);
            }
        }
        public DelegateCommand LoginCommandCustomer
        {
            get
            {
                return new DelegateCommand(LoginCustomer);
            }
        }
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
                _regionManager.RequestNavigate("ContentRegion", navigatePath);
        }
    }
}
