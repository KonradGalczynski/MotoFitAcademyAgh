﻿#region File Header & Copyright Notice
//Copyright 2016 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System.Collections.Generic;
using System.Linq;
using OpenDayApplication.Model.Database;
using System.Text.RegularExpressions;
using System.Windows;

namespace OpenDayApplication.Model.Managers
{
  public class WorkersManager
  {
    public List<Worker> GetWorkers()
    {
      var _workers = new List<Worker>();
      try { 
          using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
          {
                  _workers = dataContext.Workers.ToList();
          }
      } catch (System.Data.SqlClient.SqlException)
      {
                MessageBox.Show("Nie można pobrać listy pracowników.", "Błąd połączenia z bazą danych.");
      }

      return _workers;
    }
    public void AddWorker(Worker worker)
    {
      /*  if (string.IsNullOrWhiteSpace(worker.Name) == true)
        {
            MessageBox.Show("Pole imię nie może być puste.");
            return;
        }

        if (string.IsNullOrWhiteSpace(worker.Surname) == true)
        {
            MessageBox.Show("Pole nazwisko nie może być puste.");
            return;
        } */

      using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
      {
                string pattern = @"^[0-9]{11}$";
                if (Regex.IsMatch(worker.Pesel, pattern) == false)
                {
                    MessageBox.Show("Niepoprawny pesel");
                    return;
                }
                dataContext.Workers.InsertOnSubmit(worker);
                dataContext.SubmitChanges();
      }
    }
    public void DeleteWorker(Worker worker)
    {
            try
            {
                using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
                {
                    dataContext.Workers.Attach(worker);
                    dataContext.Workers.DeleteOnSubmit(worker);
                    dataContext.SubmitChanges();
                }
            } catch (System.Data.SqlClient.SqlException)
            {
                System.Windows.MessageBox.Show("Nie udało się usunąć pracownika.", "Błąd połączenia z bazą danych");
            }
    }
    public void EditWorker(Worker worker)
    {
      using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
      {
        var workerToEdit = dataContext.Workers.FirstOrDefault(w => w.ID == worker.ID);
        workerToEdit.Name = worker.Name;
        workerToEdit.Surname = worker.Surname;
        dataContext.SubmitChanges();
      }
    }
  }
}
