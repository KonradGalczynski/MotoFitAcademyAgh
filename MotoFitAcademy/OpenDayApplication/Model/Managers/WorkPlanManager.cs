#region File Header & Copyright Notice
//Copyright 2016 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using OpenDayApplication.Model.Database;

namespace OpenDayApplication.Model.Managers
{
  public class WorkPlanManager
  {
    private static List<WorkPlanElement> _workPlanElements;
    public List<WorkPlanElement> GetWorkPlanElements()
    {
      _workPlanElements = new List<WorkPlanElement>();
      using (var sqlConn = new SqlConnection(Confiuration.GetSqlConnectionString()))
      {
        var cmd = new SqlCommand(@"SELECT WorkPlan.Id AS WorkPlanId, DayIndex, StartTime, EndTime, Employees.Id AS EmployeeId, Employees.Name AS EmployeeName, Salary,
                                   Rooms.Id AS RoomId, Rooms.Name AS RoomName, Capacity, Classes.Id AS ClassId, Classes.Name AS ClassName, AddInfo FROM WorkPlan 
                                   LEFT JOIN Employees ON WorkPlan.WorkerId=Employees.Id
                                   LEFT JOIN Rooms ON Rooms.Id=WorkPlan.RoomId
                                   LEFT JOIN Classes ON Classes.Id=WorkPlan.ClassId")
        {
          CommandType = CommandType.Text,
          Connection = sqlConn
        };
        sqlConn.Open();
        using (var reader = cmd.ExecuteReader())
        {
          if (reader.HasRows)
          {
            while (reader.Read())
            {
              var workPlanElement = new WorkPlanElement();
              var worker = new Worker();
              var _class = new Class();
              var room = new Room();

              worker.ID = reader.GetInt32(reader.GetOrdinal("EmployeeId"));
              worker.Name = reader.GetString(reader.GetOrdinal("EmployeeName"));
              workPlanElement.Worker = worker;

              room.ID = reader.GetInt32(reader.GetOrdinal("RoomId"));
              room.Name = reader.GetString(reader.GetOrdinal("RoomName"));
              room.Capacity = reader.GetInt32(reader.GetOrdinal("Capacity"));
              workPlanElement.Room = room;

              workPlanElement.Worker = worker;

              _class.ID = reader.GetInt32(reader.GetOrdinal("ClassId"));
              _class.Name = reader.GetString(reader.GetOrdinal("ClassName"));
              _class.Popularity = reader.GetString(reader.GetOrdinal("AddInfo"));
              workPlanElement.Class = _class;

              workPlanElement.ID = reader.GetInt32(reader.GetOrdinal("WorkPlanId"));
              workPlanElement.DayOfWeek = (DayOfWeek)reader.GetInt32(reader.GetOrdinal("DayIndex"));
              workPlanElement.StartTime = reader.GetTimeSpan(reader.GetOrdinal("StartTime"));
              workPlanElement.EndTime = reader.GetTimeSpan(reader.GetOrdinal("EndTime"));
              _workPlanElements.Add(workPlanElement);
            }
          }
        }
      }
      return _workPlanElements;
    }
    public void AddWorkPlanElement(WorkPlanElement workPlanElement)
    {
      using (var sqlConn = new SqlConnection(Confiuration.GetSqlConnectionString()))
      {
        var cmd = new SqlCommand("INSERT INTO WorkPlan (WorkerId, RoomId, ClassId, DayIndex, StartTime, EndTime) VALUES (@WorkerId, @RoomId, @ClassId, @DayIndex, @StartTime, @EndTime)")
        {
          CommandType = CommandType.Text,
          Connection = sqlConn
        };
        cmd.Parameters.Add("@WorkerId", SqlDbType.Int, 250).Value = workPlanElement.Worker.ID;
        cmd.Parameters.Add("@RoomId", SqlDbType.Int, 250).Value = workPlanElement.Room.ID;
        cmd.Parameters.Add("@ClassId", SqlDbType.Int, 250).Value = workPlanElement.Class.ID;
        cmd.Parameters.Add("@DayIndex", SqlDbType.Int, 250).Value = (int)workPlanElement.DayOfWeek;
        cmd.Parameters.Add("@StartTime", SqlDbType.Time, 7).Value = workPlanElement.StartTime;
        cmd.Parameters.Add("@EndTime", SqlDbType.Time, 7).Value = workPlanElement.EndTime;
        sqlConn.Open();
        cmd.ExecuteNonQuery();
      }
    }
    public void DeleteWorkPlanElement(WorkPlanElement workPlanElement)
    {
      using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
      {
        dataContext.WorkPlanElements.Attach(workPlanElement);
        dataContext.WorkPlanElements.DeleteOnSubmit(workPlanElement);
        dataContext.SubmitChanges();
      }
    }
    public void EditWorkPlanElement(WorkPlanElement workPlanElement)
    {
      using (var sqlConn = new SqlConnection(Confiuration.GetSqlConnectionString()))
      {
        var cmd = new SqlCommand("UPDATE WorkPlan SET WorkerId=@WorkerId, RoomId=@RoomId, ClassId=@ClassId, DayIndex=@DayIndex, StartTime=@StartTime, EndTime=@EndTime WHERE Id=@Id")
        {
          CommandType = CommandType.Text,
          Connection = sqlConn
        };
        cmd.Parameters.Add("@WorkerId", SqlDbType.Int, 250).Value = workPlanElement.Worker.ID;
        cmd.Parameters.Add("@RoomId", SqlDbType.Int, 250).Value = workPlanElement.Room.ID;
        cmd.Parameters.Add("@ClassId", SqlDbType.Int, 250).Value = workPlanElement.Class.ID;
        cmd.Parameters.Add("@DayIndex", SqlDbType.Int, 250).Value = (int)workPlanElement.DayOfWeek;
        cmd.Parameters.Add("@StartTime", SqlDbType.Time, 250).Value = workPlanElement.StartTime;
        cmd.Parameters.Add("@EndTime", SqlDbType.Int, 250).Value = workPlanElement.EndTime;
        cmd.Parameters.Add("@Id", SqlDbType.Int, 250).Value = workPlanElement.ID;
        sqlConn.Open();
        cmd.ExecuteNonQuery();
      }
    }
  }
}
