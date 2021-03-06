﻿#region File Header & Copyright Notice
//Copyright 2016 Motorola Solutions, Inc.
//All Rights Reserved.
//Motorola Solutions Confidential Restricted
#endregion

using System.Data.Linq.Mapping;

namespace OpenDayApplication.Model
{
  [Table(Name = "Clients")]
  public class Client
  {
    [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
    public int ID { get; set; }
    [Column]
    public string Name { get; set; }
    [Column]
    public string Surname { get; set; }
    [Column]
    public string Address { get; set; }
    [Column]
    public bool VIP { get; set; }
  }
}
