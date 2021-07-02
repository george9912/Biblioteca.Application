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
    public class ViewDViewModel : BindableBase
    {
        private ObservableCollection<LoanModel> loansModel = new ObservableCollection<LoanModel>();

        public ViewDViewModel()
        {
            var loanRestService = new LoanRestService();
        }

        public DelegateCommand GetLoans
        {
            get
            {
                return new DelegateCommand(GetLoansAction);
            }
        }
        public ObservableCollection<LoanModel> LoansModel
        {
            get
            {
                return loansModel;
            }
            set
            {
                if (value != loansModel)
                {
                    loansModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public async void GetLoansAction()
        {
            try
            {
                LoansModel.Clear();
                var loanRestService = new LoanRestService();
                var loans = await loanRestService.GetLoans();
                foreach (var loan in loans)
                {
                    LoansModel.Add(loan);
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
