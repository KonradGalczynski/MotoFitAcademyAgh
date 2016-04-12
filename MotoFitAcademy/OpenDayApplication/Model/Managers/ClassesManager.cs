#region File Header & Copyright Notice
//Copyright 2016 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System.Collections.Generic;
using System.Linq;
using OpenDayApplication.Model.Database;
using System.Data.Linq;
using System;
using System.Windows;

namespace OpenDayApplication.Model.Managers
{
    public class ClassesManager
    {
    
        public List<Class> GetClasses()
        {
            var _classes = new List<Class>();
            try
            {
                using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
                {
                    _classes = dataContext.Classes.ToList();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Brak połączenia z bazą danych!");
            }
            return _classes;
        }
    
        public void AddClass(Class _class)
        {
      try {
                using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
            {

                    dataContext.Classes.InsertOnSubmit(_class);
                    dataContext.SubmitChanges();

            }

          }
            catch (Exception e)
            {

                MessageBox.Show("Operacja dodaj nie powiodła się");

            }
        }

        public void DeleteClass(Class _class)
        {
            try
            {
                using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
                {
                    dataContext.Classes.Attach(_class);
                    dataContext.Classes.DeleteOnSubmit(_class);
                    dataContext.SubmitChanges();
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Brak połączenia z bazą danych!");
            }
        }
        public void EditClass(Class _class)
        {
            try {
                using (var dataContext = new MotoFitAcademyDataContext(Confiuration.GetSqlConnectionString()))
                {
                    var classToEdit = dataContext.Classes.FirstOrDefault(c => c.ID == _class.ID);
                    classToEdit.Name = _class.Name;
                    classToEdit.Popularity = _class.Popularity;
                    dataContext.SubmitChanges();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show("Operacja edycji nie powiodła się");
            }
            }
  }
    
}
