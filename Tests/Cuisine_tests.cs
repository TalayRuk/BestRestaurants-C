using System;
using Xunit;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BestRestaurants;
using BestRestaurants.Objects;

namespace Testing
{
  public class Tests : IDisposable
   {
     public Tests()
      {
        DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=!!!!!!!!!!!!!!;Integrated Security=SSPI;";
      }
    }
}
