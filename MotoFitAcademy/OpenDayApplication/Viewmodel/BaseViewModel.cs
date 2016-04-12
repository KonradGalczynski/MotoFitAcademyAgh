using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenDayApplication.Model.Managers;

namespace OpenDayApplication.Viewmodel
{
  public class BaseViewModel : INotifyPropertyChanged
  {
    private readonly ClassesManager _classesManager;
    private readonly ClientsManager _clientsManager;
    private readonly RoomsManager _roomsManager;
    private readonly WorkersManager _workersManager;
    private readonly WorkPlanManager _workPlanManager;

    public BaseViewModel()
    {
      _classesManager = new ClassesManager();
      _clientsManager = new ClientsManager();
      _roomsManager = new RoomsManager();
      _workersManager = new WorkersManager();
      _workPlanManager = new WorkPlanManager();
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged(string propertyName)
    {
      if (PropertyChanged != null)
      {
        var e = new PropertyChangedEventArgs(propertyName);
        PropertyChanged(this, e);
      }
    }

    protected ClassesManager GetClassesManager()
    {
      return _classesManager;
    }

    protected ClientsManager GetClientsManager()
    {
      return _clientsManager;
    }

    protected RoomsManager GetRoomsManager()
    {
      return _roomsManager;
    }
    protected WorkersManager GetWorkersManagerr()
    {
      return _workersManager;
    }
    protected WorkPlanManager GetWorkPlanManager()
    {
      return _workPlanManager;
    }
  }
}
