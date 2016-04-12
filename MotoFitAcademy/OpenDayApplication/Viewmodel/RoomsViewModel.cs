using OpenDayApplication.Model;
using OpenDayApplication.Model.Managers;
using System.Windows.Input;

namespace OpenDayApplication.Viewmodel
{
  public class RoomsViewModel : BaseViewModel
  {
    private readonly RoomsManager _roomsManager;
    private bool _isRoomEditVisible;
    private Room _editedRoom;
    private CrudOperation _selectedOperation;

    public ICommand AddRoomCommand { get; set; }
    public ICommand SaveCommand { get; set; }
    public ICommand EditRoomCommand { get; set; }
    public ICommand CancelCommand { get; set; }

    public Room EditedRoom
    {
      get { return _editedRoom; }
      set
      {
        _editedRoom = value;
        OnPropertyChanged("EditedRoom");
      }
    }
    public bool IsRoomEditVisible
    {
      get { return _isRoomEditVisible; }
      set
      {
        _isRoomEditVisible = value;
        OnPropertyChanged("IsRoomEditVisible");
      }
    }

    public RoomsViewModel()
    {
      _roomsManager = GetRoomsManager();
      AddRoomCommand = new BaseCommand(AddRoom);
      EditRoomCommand = new BaseCommand(EditRoom);
      SaveCommand = new BaseCommand(SaveChanges);
      CancelCommand = new BaseCommand(Cancel);
    }

    public void AddRoom()
    {
      IsRoomEditVisible = true;
      _selectedOperation = CrudOperation.Create;
      EditedRoom = new Room();
    }

    public void EditRoom()
    {
      if (EditedRoom != null && EditedRoom.ID != 0)
      {
        IsRoomEditVisible = true;
        _selectedOperation = CrudOperation.Edit;
      }
      else
      {
        IsRoomEditVisible = false;
      }
    }

    public void SaveChanges()
    {
      switch (_selectedOperation)
      {
        case CrudOperation.Create:
          _roomsManager.AddRoom(EditedRoom);
          break;
        case CrudOperation.Edit:
          _roomsManager.EditRoom(EditedRoom);
          break;
      }
      IsRoomEditVisible = false;
    }

    public void Cancel()
    {
      IsRoomEditVisible = false;
    }
  }
}
