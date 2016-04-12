using OpenDayApplication.Model;
using OpenDayApplication.Model.Managers;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using OpenDayApplication.Viewmodel.Validators;

namespace OpenDayApplication.Viewmodel
{
    public class WorkersViewModel : BaseViewModel
    {
        private readonly WorkersManager _workersManager;
        private List<Worker> _workers;
        private bool _isWorkerEditVisible;
        private Worker _editedWorker;
        private CrudOperation _selectedOperation;
        private int _workerCount;

        public ICommand AddWorkerCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand EditWorkerCommand { get; set; }
        public ICommand DeleteWorkerCommand { get; set; }
        public ICommand CancelCommand { get; set; }
       

  
        public Worker EditedWorker
        {
            get { return _editedWorker; }
            set
            {
                _editedWorker = value;
                OnPropertyChanged("EditedWorker");
            }
        }
        public List<Worker> Workers
        {
            get { return _workers; }
            set
            {
                _workers = value;
                OnPropertyChanged("Workers");
            }
        }
        public bool IsWorkerEditVisible
        {
            get { return _isWorkerEditVisible; }
            set
            {
                _isWorkerEditVisible = value;
                OnPropertyChanged("IsWorkerEditVisible");
            }
        }
        public int RefreshWorkersCount
        {
            get { return _workerCount; }
            set
            {
                _workerCount = value;
                OnPropertyChanged("RefreshWorkersCount");
            }
        }
        public WorkersViewModel()
        {
            _workersManager = GetWorkersManagerr();
            AddWorkerCommand = new BaseCommand(AddWorker);
            EditWorkerCommand = new BaseCommand(EditWorker);
            DeleteWorkerCommand = new BaseCommand(DeleteWorker);
            SaveCommand = new BaseCommand(SaveChanges);
            CancelCommand = new BaseCommand(Cancel);
            RefreshWorkers();
        }

        public void AddWorker()
        {
            IsWorkerEditVisible = true;
            _selectedOperation = CrudOperation.Create;
            EditedWorker = new Worker();
            RefreshWorkers();
            RefreshWorkers_number();
        }

        public void EditWorker()
        {
            if (EditedWorker != null && EditedWorker.ID != 0)
            {
                IsWorkerEditVisible = true;
                _selectedOperation = CrudOperation.Edit;
            }
            else
            {
                IsWorkerEditVisible = false;
            }
        }

        public void DeleteWorker()
        {
            IsWorkerEditVisible = false;
            if (EditedWorker != null && EditedWorker.ID != 0)
            {
                _workersManager.DeleteWorker(EditedWorker);
                RefreshWorkers();
            }
        }

      private void SaveChanges()
        {
            if (WorkerValidator.Perform(EditedWorker))
            {
                switch (_selectedOperation)
                {
                    case CrudOperation.Create:
                        _workersManager.AddWorker(EditedWorker);
                        break;
                    case CrudOperation.Edit:
                        _workersManager.EditWorker(EditedWorker);
                        break;
                }
                IsWorkerEditVisible = false;
                RefreshWorkers();
            }
            else
                MessageBox.Show("Yoe have entered incorrect data!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public void Cancel()
        {
            IsWorkerEditVisible = false;
        }

        private void RefreshWorkers()
        {
            Workers = new List<Worker>(_workersManager.GetWorkers());
           
        }

        private void RefreshWorkers_number()
        { 
            if(_workers != null)
            {
                RefreshWorkersCount = _workers.Count;
            }
            else
            {
                RefreshWorkersCount = 0;
            }
                  
        }
    }
}
