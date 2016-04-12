using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDayApplication.Model
{
    public class WorkersManager
    {
        private List<Worker> _workers;
        public List<Worker> GetWorkers()
        {
            if (_workers == null)
                _workers = new List<Worker>();
            return _workers;
        }
        public void AddWorker(Worker worker)
        {
          worker.ID = GetNewID();
            if (_workers == null)
                _workers = new List<Worker>();
            _workers.Add(worker);
        }
        public void DeleteWorker(int workerId)
        {
            if(_workers != null)
            {
                var workerToRemove = _workers.FirstOrDefault(w => w.ID == workerId);
                if(workerToRemove!=null)
                {
                    _workers.Remove(workerToRemove);
                }
            }
        }
        public void EditWorker(Worker worker)
        {
            var workerToEdit = _workers.FirstOrDefault(w => w.ID == worker.ID);
            if(workerToEdit!=null)
            {
                workerToEdit.Name = worker.Name;
                workerToEdit.Surname = worker.Surname;
                workerToEdit.Salary = worker.Salary;
            }
        }

      private int GetNewID()
      {
        if (_workers == null || _workers.Count == 0)
          return 1;
        return _workers.Max(w => w.ID) + 1;
      }
    }
}
