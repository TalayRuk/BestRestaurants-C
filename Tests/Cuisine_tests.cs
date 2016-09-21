using System;
using Xunit;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BestRestaurants;
using BestRestaurants.Objects;

namespace BestRestaurants
{
  public class Cuisine_Tests : IDisposable
   {
     public Tests()
      {
        DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=!!!!!!!!!!!!!!;Integrated Security=SSPI;";
      }
      [Fact]
      public void Test1_IsDbEmpty()
      {
        //Act
        int AnyRows = Cuisine.GetAll().Count;
        //Assert
        Assert.Equal(0, AnyRows);
      }

    }
}
