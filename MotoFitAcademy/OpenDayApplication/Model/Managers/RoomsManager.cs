#region File Header & Copyright Notice
//Copyright 2016 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System.Collections.Generic;
using System.Linq;
using OpenDayApplication.Model.Database;
using System;
using System.Windows;

namespace OpenDayApplication.Model.Managers
{
  public class RoomsManager
  {
    public List<Room> GetRooms()
    {
      var _rooms = new List<Room>();
      try
      {
        using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
        {
          _rooms = dataContext.Rooms.ToList();
        }
      }
      catch (Exception e)
      {
                MessageBox.Show("Nie udało się pobrać zawartości bazy danych");
      }
            
      return _rooms;
      
      
    }
    public void AddRoom(Room room)
    {
      if (string.IsNullOrWhiteSpace(room.Name)==true)
        {MessageBox.Show("Nazwa nie może być pusta.");
        return;  
        }
      try
            
            { 
                using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
                {
                    dataContext.Rooms.InsertOnSubmit(room);
                    dataContext.SubmitChanges();
                }
       
            }
            catch (Exception e)
            {
                MessageBox.Show("Operacja Dodaj nie powiodła się");
            }
        }
    public void EditRoom(Room room)
    {
        try
        {
            using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
            {
                var roomToEdit = dataContext.Rooms.FirstOrDefault(r => r.ID == room.ID);
                roomToEdit.Name = room.Name;
                roomToEdit.Capacity = room.Capacity;
                dataContext.SubmitChanges();
            }
        }
        catch (Exception e)
        {
            MessageBox.Show("Operacja Edytuj nie powiodła się");
        }
    }

   internal void DeleteRoom(Room EditedRoom)
    {
        using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
        {
            dataContext.Rooms.Attach(EditedRoom);
            dataContext.Rooms.DeleteOnSubmit(EditedRoom);
            dataContext.SubmitChanges();
        }
    } 
   
  }
}
