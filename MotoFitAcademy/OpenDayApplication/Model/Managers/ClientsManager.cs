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
    public void EditClient(Client client)
    {
        try
        {
            using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
            {
                var clientToEdit = dataContext.Clients.FirstOrDefault(w => w.ID == client.ID);
                clientToEdit.Name = client.Name;
                clientToEdit.Surname = client.Surname;
                clientToEdit.Address = client.Address;
                clientToEdit.VIP = client.VIP;
                dataContext.SubmitChanges();
            }
        }
        catch (System.Data.SqlClient.SqlException)
        {
            System.Windows.MessageBox.Show("Nie udało się dokonać edycji klienta.", "Błąd połączenia z bazą danych");
        }
    }
    }
}
