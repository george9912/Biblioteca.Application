using Biblioteca.WPF.API.Client;
using Biblioteca.WPF.Models;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace ModuleB.ViewModels
{
    public class ViewCViewModel : BindableBase
    {
        private ObservableCollection<ClientModel> clientsModel = new ObservableCollection<ClientModel>();
        //ClientModel newClientModel = new ClientModel();

        private ClientModel clientPropToAdd;
        public ClientModel ClientPropToAdd
        {
            get
            {
                return clientPropToAdd;
            }
            set
            {
                if (value != clientPropToAdd)
                {
                    clientPropToAdd = value;
                    RaisePropertyChanged();
                }
            }
        }

        private ClientModel clientPropToUpdate;
        public ClientModel ClientPropToUpdate
        {
            get
            {
                return clientPropToUpdate;
            }
            set
            {
                if (value != clientPropToUpdate)
                {
                    clientPropToUpdate = value;
                    RaisePropertyChanged();
                }
            }
        }


        public ViewCViewModel()
        {
            clientPropToAdd = new ClientModel();
            clientPropToUpdate = new ClientModel();
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

        private DelegateCommand<ClientModel> updateClientCommand;
        public DelegateCommand<ClientModel> UpdateClientCommand =>
            updateClientCommand ?? (updateClientCommand = new DelegateCommand<ClientModel>(UpdateClientAction));

        private DelegateCommand<ClientModel> editClientCommand;
        public DelegateCommand<ClientModel> EditClientCommand =>
            editClientCommand ?? (editClientCommand = new DelegateCommand<ClientModel>(EditClientAction));

        private DelegateCommand<ClientModel> deleteClientCommand;
        public DelegateCommand<ClientModel> DeleteClientCommand =>
            deleteClientCommand ?? (deleteClientCommand = new DelegateCommand<ClientModel>(DeleteClientAction));

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
        private ClientModel selectedClientGrid;
        public ClientModel SelectedClientGrid
        {
            get
            {
                return selectedClientGrid;
            }
            set
            {
                if (value != selectedClientGrid)
                {
                    selectedClientGrid = value;
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
                ClientModel clientToAdd = new ClientModel()
                {
                    FirstName = ClientPropToAdd.FirstName,
                    LastName = ClientPropToAdd.LastName,
                    Phone = ClientPropToAdd.Phone,
                    Adress = ClientPropToAdd.Adress
                };

                var clientRestService = new CustomerRestService();
                await clientRestService.CreateClient(clientToAdd);
                clientToAdd = null;
                GetClientsAction();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: '{ex}'");
            }
        }

        public async void UpdateClientAction(ClientModel SelectedClient)
        {
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var clientRestService = new CustomerRestService();
                    await clientRestService.UpdateClient(ClientPropToUpdate.Id, ClientPropToUpdate);

                    GetClientsAction();
                }
                else
                {
                    System.Windows.MessageBox.Show("Update operation terminated");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: '{ex}'");
            }
        }

        public void EditClientAction(ClientModel SelectedClientGrid)
        {
            try
            {
                clientPropToUpdate.Id = SelectedClientGrid.Id;
                clientPropToUpdate.FirstName = SelectedClientGrid.FirstName;
                clientPropToUpdate.LastName = SelectedClientGrid.LastName;
                clientPropToUpdate.Phone = SelectedClientGrid.Phone;
                clientPropToUpdate.Adress = SelectedClientGrid.Adress;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: '{ex}'");
            }
        }

        public async void DeleteClientAction(ClientModel clientToDelete)
        {
            try
            {
                MessageBoxResult messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var clientRestService = new CustomerRestService();
                    await clientRestService.DeleteClient(clientToDelete.Id);
                    GetClientsAction();
                }
                else
                {
                    System.Windows.MessageBox.Show("Delete operation terminated");
                }
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
