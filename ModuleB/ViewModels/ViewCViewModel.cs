using Biblioteca.WPF.API.Client;
using Biblioteca.WPF.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModuleB.ViewModels
{
    public class ViewCViewModel : BindableBase
    {
        private ObservableCollection<ClientModel> clientsModel = new ObservableCollection<ClientModel>();
        ClientModel newClientModel = new ClientModel();
        public ViewCViewModel()
        {
            var customerRestService = new CustomerRestService();
        }

        public DelegateCommand GetClients
        {
            get
            {
                return new DelegateCommand(GetClientsAction);
            }
        }

        public DelegateCommand AddClient
        {
            get
            {
                return new DelegateCommand(AddClientAction);
            }
        }

        public ObservableCollection<ClientModel> ClientsModel
        {
            get
            {
                return clientsModel;
            }
            set
            {
                if (value != clientsModel)
                {
                    clientsModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public async void GetClientsAction()
        {
            try
            {
                ClientsModel.Clear();
                var clientRestService = new CustomerRestService();
                var clients = await clientRestService.GetClients();
                foreach (var client in clients)
                {
                    ClientsModel.Add(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: '{ex}'");
            }
        }

        public async void AddClientAction()
        {
            try
            {
                var clientRestService = new CustomerRestService();
                await clientRestService.CreateClient(newClientModel);
                newClientModel = null;
                GetClientsAction();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: '{ex}'");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
