#region File Header & Copyright Notice
//Copyright 2016 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System.Collections.Generic;
using System.Linq;
using OpenDayApplication.Model.Database;

namespace OpenDayApplication.Model.Managers
{
  public class ClientsManager
  {
    public List<Client> GetClients()
    {
      var _clients = new List<Client>();
      using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
      {
        _clients = dataContext.Clients.ToList();
      }
      return _clients;

    }
    public void AddClient(Client client)
    {
      using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
      {
        dataContext.Clients.InsertOnSubmit(client);
        dataContext.SubmitChanges();
      }
    }
    public void DeleteClient(Client client)
    {
      using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
      {
        dataContext.Clients.Attach(client);
        dataContext.Clients.DeleteOnSubmit(client);
        dataContext.SubmitChanges();
      }
    }
  }
}
