using Xunit;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using BestRestaurants;

namespace BestRestaurants
{
  public class Restaurants_tests : IDisposable
  {
    public Restaurants_Test()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=best_restaurants_tests;Integrated Security=SSPI;";
    }


    public void Dispose()
    {
      Restaurants.DeleteAll();
    }
  }
}
