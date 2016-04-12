#region File Header & Copyright Notice
//Copyright 2016 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System.Data.Linq.Mapping;

namespace OpenDayApplication.Model
{
  [Table(Name = "Classes")]
  public class Class
  {
    [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
    public int ID { get; set; }
    [Column]
    public string Name { get; set; }
    [Column(Name = "AddInfo")]
    public string Popularity { get; set; }
  }
}
