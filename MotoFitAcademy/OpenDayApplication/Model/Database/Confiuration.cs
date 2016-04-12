#region File Header & Copyright Notice
//Copyright 2016 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

namespace OpenDayApplication.Model.Database
{
  public class Confiuration
  {
    private const string ServerName = @"tcp:nlhxsich7b.database.windows.net";
    private const string DatabaseName = "MotoFitAcademy";
    private const string UserName = "MotoFitAcademyUser@nlhxsich7b";
    private const string Password = "MotoFitAcademy2015";

    public static string GetSqlConnectionString()
    {
      return string.Format("Server={0},1433;Database={1};User ID={2};Password={3};Trusted_Connection=False;Encrypt=True;Connection Timeout=30",
       ServerName, DatabaseName, UserName, Password);
    }
  }
}
