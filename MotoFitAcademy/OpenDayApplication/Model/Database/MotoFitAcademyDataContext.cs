#region File Header & Copyright Notice
//Copyright 2016 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System.Data.Linq;

namespace OpenDayApplication.Model.Database
{
  public class MotoFitAcademyDataContext : DataContext
  {
    public MotoFitAcademyDataContext(string connectionString)
        : base(connectionString)
    { }

    public Table<Worker> Workers;
    public Table<Class> Classes;
    public Table<Client> Clients;
    public Table<Room> Rooms;
    public Table<WorkPlanElement> WorkPlanElements;
  }
}
