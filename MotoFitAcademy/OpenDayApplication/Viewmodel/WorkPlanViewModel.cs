using System.Security.Cryptography.X509Certificates;
using OpenDayApplication.Model;
using OpenDayApplication.Model.Managers;
using OpenDayApplication.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace OpenDayApplication.Viewmodel
{
  public class WorkPlanViewModel : BaseViewModel
  {
    public List<WorkPlanElement> WorkPlanElements
    {
      get { return _workPlanElements; }
      set
      {
        _workPlanElements = value;
        OnPropertyChanged("WorkPlanElements");
      }
    }
    public List<TimeSpan> TimeList
    {
      get { return _timeList; }
      set
      {
        _timeList = value;
        OnPropertyChanged("TimeList");
      }
    }
    public List<Class> ClassList
    {
      get { return _classList; }
      set
      {
        _classList = value;
        OnPropertyChanged("ClassList");
      }
    }
    public List<Room> RoomList
    {
      get { return _roomList; }
      set
      {
        _roomList = value;
        OnPropertyChanged("RoomList");
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
    public WorkPlanElement EditedWorkPlanElement
    {
      get { return _editedWorkPlanElement; }
      set
      {
        _editedWorkPlanElement = value;
        OnPropertyChanged("EditedWorkPlanElement");
      }
    }
    public bool IsWorkPlanElementEditVisible
    {
      get { return _isWorkPlanElementEditVisible; }
      set
      {
        _isWorkPlanElementEditVisible = value;
        OnPropertyChanged("IsWorkPlanElementEditVisible");
      }
    }
    public List<DayOfWeek> Days
    {
      get { return _days; }
      set
      {
        _days = value;
        OnPropertyChanged("Days");
      }
    }

    public ICommand AddWorkPlanElementCommand { get; set; }
    public ICommand EditWorkPlanElementCommand { get; set; }
    public ICommand DeleteWorkPlanElementCommand { get; set; }
    public ICommand SaveCommand { get; set; }
    public ICommand CancelCommand { get; set; }
    public ICommand ShowTimetableCommand { get; set; }

    private List<WorkPlanElement> _workPlanElements;
    private List<TimeSpan> _timeList;
    private List<Class> _classList;
    private List<Room> _roomList;
    private WorkPlanElement _editedWorkPlanElement;
    private bool _isWorkPlanElementEditVisible = false;
    private readonly RoomsManager _roomsManager;
    private readonly ClassesManager _classesManager;
    private readonly WorkPlanManager _workPlanManager;
    private readonly WorkersManager _workersManager;
    private CrudOperation _selectedOperation;
    private List<Worker> _workers;
    private List<DayOfWeek> _days;

    public WorkPlanViewModel()
    {
      AddWorkPlanElementCommand = new BaseCommand(AddWorkPlanElement);
      EditWorkPlanElementCommand = new BaseCommand(EditWorkPlanElement);
      DeleteWorkPlanElementCommand = new BaseCommand(DeleteWorkPlanElement);
      SaveCommand = new BaseCommand(SaveChanges);
      CancelCommand = new BaseCommand(Cancel);
      ShowTimetableCommand = new BaseCommand(ShowTimeTable);

      _roomsManager = GetRoomsManager();
      _classesManager = GetClassesManager();
      _workPlanManager = GetWorkPlanManager();
      _workersManager = GetWorkersManagerr();

      RefreshWorkPlanElements();
    }

    public void AddWorkPlanElement()
    {
      IsWorkPlanElementEditVisible = true;
      EditedWorkPlanElement = new WorkPlanElement();
      PrepareLists();

      EditedWorkPlanElement.Room = RoomList.FirstOrDefault();
      EditedWorkPlanElement.Class = ClassList.FirstOrDefault();
      EditedWorkPlanElement.StartTime = TimeList.First();
      EditedWorkPlanElement.EndTime = TimeList.ElementAt(1);
      EditedWorkPlanElement.Worker = Workers.FirstOrDefault();
      _selectedOperation = CrudOperation.Create;

    }

    public void ShowTimeTable()
    {
      var timeTable = new TimeLineView();
      timeTable.SetTimeTable(WorkPlanElements);
      timeTable.ShowDialog();
    }
    private void PrepareTimeList()
    {
      if (TimeList == null)
      {
        TimeList = new List<TimeSpan>();
        var endTime = new TimeSpan(17, 0, 0);
        var time = new TimeSpan(9, 0, 0);
        while (time <= endTime)
        {
          TimeList.Add(time);
          time = time.Add(new TimeSpan(0, 30, 0));
        }
      }
    }
    private void PrepareDayList()
    {
      if (Days == null)
      {
        Days = new List<DayOfWeek>();
        for (int i = 1; i < 6; i++)
          Days.Add((DayOfWeek)i);
      }
    }
    public void EditWorkPlanElement()
    {
      if (EditedWorkPlanElement != null && EditedWorkPlanElement.ID != 0)
      {
        IsWorkPlanElementEditVisible = true;
        _selectedOperation = CrudOperation.Edit;
      }
      else
      {
        IsWorkPlanElementEditVisible = false;
      }
    }

    public void DeleteWorkPlanElement()
    {
      IsWorkPlanElementEditVisible = false;
      if (EditedWorkPlanElement != null && EditedWorkPlanElement.ID != 0)
      {
        _workPlanManager.DeleteWorkPlanElement(EditedWorkPlanElement);
        RefreshWorkPlanElements();
      }
    }

    public void SaveChanges()
    {
      //if (IsClientValid())
      //{
      switch (_selectedOperation)
      {
        case CrudOperation.Create:
          _workPlanManager.AddWorkPlanElement(EditedWorkPlanElement);
          break;
        case CrudOperation.Edit:
          _workPlanManager.EditWorkPlanElement(EditedWorkPlanElement);
          break;
      }
      RefreshWorkPlanElements();
      IsWorkPlanElementEditVisible = false;
      //}
      //else
      //  MessageBox.Show("Yoe have entered incorrect data!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    public void Cancel()
    {
      IsWorkPlanElementEditVisible = false;
    }

    private void PrepareLists()
    {
      PrepareTimeList();
      PrepareDayList();
      RoomList = _roomsManager.GetRooms();
      ClassList = _classesManager.GetClasses();
      Workers = _workersManager.GetWorkers();
    }

    private void RefreshWorkPlanElements()
    {
      WorkPlanElements = new List<WorkPlanElement>(_workPlanManager.GetWorkPlanElements());
    }
  }
}
