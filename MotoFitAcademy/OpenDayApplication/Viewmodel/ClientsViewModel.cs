using OpenDayApplication.Model;
using OpenDayApplication.Model.Managers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows;

namespace OpenDayApplication.Viewmodel
{
    public class ClientsViewModel : BaseViewModel
    {
        private readonly ClientsManager _clientsManager;
        private List<Client> _clients;
        private bool _isClientEditVisible;
        private Client _editedClient;

        public ICommand AddClientCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand DeleteClientCommand { get; set; }
        public ICommand CancelCommand { get; set; }


        public Client EditedClient
        {
            get { return _editedClient; }
            set
            {
                _editedClient = value;
                OnPropertyChanged("EditedClient");
            }
        }
        public List<Client> Clients
        {
            get { return _clients; }
            set
            {
                _clients = value;
                OnPropertyChanged("Clients");
            }
        }
        public bool IsClientEditVisible
        {
            get { return _isClientEditVisible; }
            set
            {
                _isClientEditVisible = value;
                OnPropertyChanged("IsClientEditVisible");
            }
        }

        public ClientsViewModel()
        {
        
            _clientsManager = GetClientsManager();
            AddClientCommand = new BaseCommand(AddClient);
            DeleteClientCommand = new BaseCommand(DeleteClient);
            SaveCommand = new BaseCommand(SaveChanges);
            CancelCommand = new BaseCommand(Cancel);
            RefreshClients();
       
        }

        public void AddClient()
        {
            IsClientEditVisible = true;
            EditedClient = new Client();
        }

        public void DeleteClient()
        {
        try
        {
            var buttons = MessageBoxButton.YesNo;
            var question = MessageBoxImage.Question;
            if (MessageBox.Show("Czy na pewno chcesz usunac klienta?", "Potwierdzenie usuniecia", buttons, question) == MessageBoxResult.Yes)
            {
            IsClientEditVisible = false;
            if (EditedClient != null && EditedClient.ID != 0)
            {
                _clientsManager.DeleteClient(EditedClient);
                RefreshClients();
            }
        }
        }
        catch
        {
            MessageBox.Show("Błąd przy usuwaniu klienta!!!");
        }

    }

        public void SaveChanges()
    
        {
            try { 
        Viewmodel.Validators.AddressValidator validator = new Validators.AddressValidator();
        if (!validator.IsValidEmail(EditedClient.Address))
        {
            MessageBox.Show("Wrong email !", "Email Error", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
        else
        {
            _clientsManager.AddClient(EditedClient);
            IsClientEditVisible = false;
            RefreshClients();
        }
            }
            catch
            {
                MessageBox.Show("Cos poszlo nie tak", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void Cancel()
        {
            IsClientEditVisible = false;
        }

        private void RefreshClients()
        {
        try
        {
            Clients = new List<Client>(_clientsManager.GetClients());
        }
        catch
        {
            MessageBox.Show("Błąd przy wyświetlaniu listy klientów!!!");
        }
        }
    }
}
